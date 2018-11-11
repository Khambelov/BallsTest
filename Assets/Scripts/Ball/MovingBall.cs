using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBall : MonoBehaviour
{
    public static MovingBall GetMovingBall;

    GameObject currentBall;

    void Awake()
    {
        GetMovingBall = this;
    }

    public void InitStartBall(GameObject ball)
    {
        //Записсь стартового шара и установка его скорости. На старте скорость равна 0, т.к. шар находится в покое
        currentBall = ball;
        currentBall.GetComponent<Ball>().ballSpeed = 0;
    }

    public void ChangeBall(GameObject ball)
    {
        //Предылущему шару задается скорость, равная 0, т.о. его движение останавливается. Стоит обратить внимание, что сам шар не прекращает движения! Он просто двигается со скоростью 0;
        currentBall.GetComponent<Ball>().ballSpeed = 0;

        //Текущий шар заменяется на новый, который подается в метод
        currentBall = ball;

        //Обновление значений скорости в UI
        UI.GetUI.UpdateValue(currentBall.GetComponent<Ball>().ballSpeed);
    }

    //Обновление значения скорости шара на подающееся в метод значение
    public void UpdateBallSpeed(float speed)
    {
        currentBall.GetComponent<Ball>().ballSpeed = speed;
    }
}
