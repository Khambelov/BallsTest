using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    //Массив точек шаров для следования и поворота камеры
    public GameObject[] ballPoints;

    //Чувствительность мыши при вовороте
    public float sensivity;

    //Расстояние камеры от шара
    public Vector3 offset;

    int index;

    void Start()
    {
        //Установка стартового индекса и стартового шара для камеры
        index = 0;

        MovingBall.GetMovingBall.InitStartBall(ballPoints[index].transform.parent.gameObject);
    }

    //Смена шара влево от текущего
    public void LeftSwap()
    {
        index++;

        //Если индекс равен размеру массива (т.е. вышел за его пределы), то берется стартовый индекс, т.е. первый шар.
        if (index == ballPoints.Length)
        {
            index = 0;
        }

        //Изменение текущего шара, за которым следует камера
        MovingBall.GetMovingBall.ChangeBall(ballPoints[index].transform.parent.gameObject);
    }

    //Смена шара вправо от текущего
    public void RightSwap()
    {
        index--;

        //Если индекс меньше нуля (т.е. вышел за его пределы), то берется последний индекс, т.е. последний шар.
        if (index < 0)
        {
            index = ballPoints.Length - 1;
        }

        //Изменение текущего шара, за которым следует камера
        MovingBall.GetMovingBall.ChangeBall(ballPoints[index].transform.parent.gameObject);
    }

    private void Update()
    {
        //Проверка на зажатие кнопки мыши(тача)
        if (Input.GetMouseButton(0))
        {
            //Поворт камеры в зависимости от того, в какую сторону двигают мышь (тач) по оси X, учитывая чувствительность
            ballPoints[index].transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * sensivity);
        }
    }

    void LateUpdate()
    {
        //Обновление позиции камеры по отношению к шару
        transform.position = ballPoints[index].transform.TransformPoint(offset);

        //Обновление фокуса камеры на шар
        transform.LookAt(ballPoints[index].transform.position);
    }
}
