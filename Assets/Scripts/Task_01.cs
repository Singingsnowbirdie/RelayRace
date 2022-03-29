using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//сымитировано движение ловушки с шипами в каком-нибудь данже

public class Task_01 : MonoBehaviour
{
    //массив точек
    Vector3[] points = new Vector3[4]
    {
        new Vector3(-4,0,4),
        new Vector3(4,0,4),
        new Vector3(4,0,-4),
        new Vector3(-4,0,-4)
    };

    [SerializeField] int movingSpeed; //скорость движения
    [SerializeField] int rotatingSpeed; //скорость вращения
    Vector3 target; //текущая цель
    int index = 1; //индекс текущей цели в коллекции
    bool isMoving = true; //переключатель "движется/не движется"
    bool isForward = true; //переключатель "вперед/назад"

    void Start()
    {
        StartCoroutine(Move());
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Rotate(0, rotatingSpeed, 0, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //если нажат пробел
        {
            isMoving = !isMoving; //включаем и выключаем движение

            if (!isMoving)
            {
                StopAllCoroutines();
            }
            else
            {
                StartCoroutine(Move());
            }
        }
        else if (Input.GetKeyDown(KeyCode.E)) //если нажата кнопка "Е"
        {
            isForward = !isForward; //переключаем направление движения
            StopAllCoroutines();
            SelectTarget();
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        target = points[index]; //устанавливаем цель

        //пока не приблизились к цели, движемся к ней
        while (Vector3.Distance(transform.position, target) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, movingSpeed * Time.deltaTime);
            yield return null;
        }

        //потом выбираем следующую цель
        SelectTarget();

        //движемся к следующей цели
        StartCoroutine(Move());
    }

    void SelectTarget()
    {
        if (isForward)
        {
            if (index != 3) index++;
            else index = 0;
        }
        else
        {
            if (index != 0) index--;
            else index = 3;
        }
    }
}
