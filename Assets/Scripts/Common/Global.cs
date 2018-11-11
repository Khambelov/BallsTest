using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Global : MonoBehaviour
{
    public static Global GetGlobal;

    //Интервал для двойного клика
    public float doubleClickDelay;

    //Стартовая скорость шара при движении
    public float startBallSpeed;

    //Список координат
    Dictionary<int, JSONNode> coordinates;

    public Dictionary<int, JSONNode> GetCoordinates
    {
        get { return coordinates; }
    }

    private void Awake()
    {
        GetGlobal = this;

        //Инициализация списка координат и чтение их с файлов
        coordinates = new Dictionary<int, JSONNode>();

        ReadData();
    }

    void ReadData()
    {
        //Загрузка json файла в формате TextAsset и запись текста в строку, затем парсинг строги в формат JSON с помощью SimpleJSON и добавление в словарь
        string json = Resources.Load<TextAsset>(@"Coordinates\ball_path").text;
        coordinates.Add(1, JSON.Parse(json));

        //Повторение для остальных файлов
        json = Resources.Load<TextAsset>(@"Coordinates\ball_path2").text;
        coordinates.Add(2, JSON.Parse(json));

        json = Resources.Load<TextAsset>(@"Coordinates\ball_path3").text;
        coordinates.Add(3, JSON.Parse(json));

        json = Resources.Load<TextAsset>(@"Coordinates\ball_path4").text;
        coordinates.Add(4, JSON.Parse(json));
    }
}
