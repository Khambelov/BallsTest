using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public static UI GetUI;

    public Slider slider;
    public Text value;

    int ballMoveCount;

    void Awake()
    {
        GetUI = this;
    }

    // Use this for initialization
    void Start()
    {
        //Установка стартовых значений для слайдера скорости и счетчика движущихся шаров
        slider.value = Global.GetGlobal.startBallSpeed;

        value.text = slider.value.ToString();

        ballMoveCount = 0;

        //Выключение слайдера на старте
        slider.gameObject.SetActive(false);
    }

    //Обновление значения скорости в зависимости от слайдера
    public void UpdateValue(float speed = -1)
    {
        //Если в метод не подается какое-то значение скорости, то оно автомитечски примет значение -1.
        
        //Если параметр speed равен -1, то произойдет обновление скорости текущего шара в зависимости от значения слайдера
        if(speed == -1)
        {
            MovingBall.GetMovingBall.UpdateBallSpeed(slider.value);
        }
        //Иначе значение слайдера будет равно значению входящего параметра speed
        else
        {
            slider.value = speed;
        }

        //Обновления текста UI
        value.text = slider.value.ToString();
    }

    //Включение слайдера при начале движения
    public void SliderOn()
    {
        //Увеличение счетчика движущихся шаров
        ballMoveCount++;

        //Включение слайдера при старте движения одного шара. При старте движения второго и далее шаров, повторного запуска не будет.
        if (ballMoveCount == 1)
        {
            slider.gameObject.SetActive(true);
        }

        //Установка стартовых значений при старте движения и обновления отображения UI.
        slider.value = Global.GetGlobal.startBallSpeed;

        value.text = slider.value.ToString();
    }

    //Выключение слайдера при начале движения
    public void SliderOff()
    {
        //Уменьшение счетчика движущихся шаров
        ballMoveCount--;

        //Если кол-во движущихся шаров равна 0, установятся обратно стартовые значения в UI и слайдер выключится
        if (ballMoveCount == 0)
        {
            slider.value = Global.GetGlobal.startBallSpeed;

            value.text = slider.value.ToString();

            slider.gameObject.SetActive(false);
        }
    }
}
