using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_02 : MonoBehaviour
{
    [SerializeField] List<Transform> snowmen; //все снеговики
    [SerializeField] List<Transform> points; //все точки
    [SerializeField] int speed; //скорость

    int point = 0;
    int snowmanID = 0;

    Transform currentSnowman; //снеговик, который сейчас бежит

    private void Start()
    {
        StartCoroutine(MoveTo()); //запускаем движение
    }

    IEnumerator MoveTo()
    {
        Transform target = points[point]; //выбираем цель
        currentSnowman = snowmen[snowmanID]; //выбираем снеговика
        GameObject baton = currentSnowman.transform.GetChild(1).gameObject;
        baton.SetActive(true);
        currentSnowman.LookAt(target); //поворачиваем снеговика к цели

        //пока не приблизились к цели, движемся к ней
        while (Vector3.Distance(currentSnowman.position, target.position) > 0.1f)
        {
            currentSnowman.position = Vector3.MoveTowards(currentSnowman.position, target.position, speed * Time.deltaTime);
            yield return null;
        }

        point++; //меняем точку

        if (point % 2 == 0) //на нечетной точке меняем снеговика
        {
            snowmanID++;
            baton.SetActive(false);
        }

        Debug.Log($"point {point}");

        if (point < points.Count) //если это была не последняя точка
        {
            StartCoroutine(MoveTo()); //запускаем движение
        }
    }
}
