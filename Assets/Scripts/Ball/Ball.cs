using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [Header("Ball Properties")]
    public int id;
    public Renderer renderer;
    public TrailRenderer trail;
    public float ballSpeed;

    List<Vector3> routes;

    Vector3 startPosition;
    bool oneClick;
    float clickTime;
    bool isMoving;

    void Awake()
    {
        //Инициализация списка координат
        routes = new List<Vector3>();

        //Установка имени объекта и загрузка материала из ресурсов
        gameObject.name = "Ball_" + id;
        renderer.material = trail.material = Resources.Load<Material>(string.Concat("Materials\\Ball", id.ToString()));
    }

    void Start()
    {
        //Получение координат из Global в формате JSON
        JSONNode coordinates = Global.GetGlobal.GetCoordinates[id];

        //Заполнение списка координат из JSON 
        for (int i = 0; i < coordinates["x"].Count; i++)
        {
            routes.Add(new Vector3(coordinates["x"][i].AsFloat, 
                                   coordinates["y"][i].AsFloat, 
                                   coordinates["z"][i].AsFloat));
        }

        //Установка стартовых значений и стартовой позиции
        oneClick = false;
        isMoving = false;
        startPosition = transform.position = routes[0];
    }

    //Обработка события при нажатии на шар
    public void OnMouseDown()
    {
        //Проверка, если поверх шара был нажат UI элемент
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        

        //Если один клик был произведен, то проверяется интервал между первым и вторым кликом и если он больше заданного интервала, то клик обнуляется
        if (oneClick)
        {
            if ((Time.time - clickTime) > Global.GetGlobal.doubleClickDelay)
            {
                oneClick = false;
            }
        }

        //Если клика не было или он обнулился, то начинается движение шара, если он находился в покое
        if (!oneClick)
        {
            //Фиксируем, что один клик был
            oneClick = true;

            //Засекаем время нажатия для дальнейшего подсчета интервала между кликами
            clickTime = Time.time;

            //Проверка на движение шара. Если шар в движении, преждевременно выходим из метода
            if (isMoving)
            {
                return;
            }

            //Запускаем движение и устанавливаем маркер
            isMoving = true;

            StartCoroutine(MoveBall());
        }
        //Если же двойной клик прошел, то шар возвращается в стартовую позицию, прекращая свое движение, если он в нем был. Так же выключается слайдер, очищается линия пути и убираются все маркеры.
        else
        {
            oneClick = false;
            StopAllCoroutines();
            UI.GetUI.SliderOff();
            trail.Clear();
            transform.position = startPosition;
            isMoving = false;
        }
    }

    //Корутина движения шара. Замена Update, т.к. нет смысла работать update постоянно, когда шар должен двигаться только по клику
    IEnumerator MoveBall()
    {
        //Очищаем линию с предыдущего пути
        trail.Clear();

        //Запускаем слайдер скорости
        UI.GetUI.SliderOn();

        //Задаем стартовую скорость шара
        ballSpeed = Global.GetGlobal.startBallSpeed;

        //Инициализируем индекс для прохода по списку координат
        int index = 0;

        //Выполнять процесс движения, пока индекс не выйдет за пределы кол-ва элементов в списке координат. Иными словами, пока не доберется до последней точки.
        while (index < routes.Count)
        {
            //Если координаты шара и текущих координат совпадают, индекс увеличивается, т.е. теперь будут браться следующие координаты
            if (transform.position == routes[index])
            {
                index++;
            }
            //Иначе шар будет двигаться к текущим координатам
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, routes[index], ballSpeed * Time.deltaTime);
            }

            yield return null;
        }

        //Выключается слайдер и снимается маркер движения, т.е. шар остановился.
        UI.GetUI.SliderOff();
        isMoving = false;

        yield return null;
    }
}
