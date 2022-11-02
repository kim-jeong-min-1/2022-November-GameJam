using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake() => Instance = this;

    public int Player_Click;
    public int Arrow_Number;
    float Timer;
    bool stopTimer = false  ;

    bool isClick = true;

    public List<GameObject> enemyObjs = new List<GameObject>();
    public List<Transform> Point = new List<Transform>();
    public Queue<GameObject> ArrowQueue = new Queue<GameObject> ();

    public GameObject Hit_Arrow;
    public Slider timerSlider;
    public Image fall;

    private void Start()
    {
        Spawn_Arrow();
    }

    private void Update()
    {
        if(ArrowQueue.Count == 0)
        {
            //끝내기
        }
        _Timer();
        if(isClick) Check_Click();
    }

    void Spawn_Arrow() //화살표 생성 1~2번 사용
    {
        transform.position = new Vector2(-7, 3);

        for(int i = 0; i < Point.Count; i++)
        {
            int ranEnemy = Random.Range(0, enemyObjs.Count);

            GameObject arrow = Instantiate(enemyObjs[ranEnemy], Point[i].position, Point[i].rotation);
            ArrowQueue.Enqueue(arrow);
            arrow.GetComponent<Arrow>().My_Number = ranEnemy += 1;
        }
    }

    void Check_Click()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Player_Click = 1;
            Collder_Transform();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Player_Click = 2;
            Collder_Transform();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Player_Click = 3;
            Collder_Transform();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Player_Click = 4;
            Collder_Transform();
        }
    }

    void Collder_Transform()
    {
        transform.position = new Vector2(transform.position.x + 2, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Hit_Arrow.GetComponent<Arrow>().My_Number == Player_Click)
        {
            Destroy(collision.gameObject);
            ArrowQueue.Dequeue();
        }
        else if (Hit_Arrow.GetComponent<Arrow>().My_Number != Player_Click)
        {
            StartCoroutine(Miss());
        }
    }

    IEnumerator Miss()
    {
        isClick = false;
        yield return new WaitForSeconds(1.0f);
        isClick = true;
    }

    void _Timer()
    {
        fall.color = Color.red;
        Timer += Time.deltaTime*0.2f;

        if (Timer <= 0)
        {
            stopTimer = true;
        }

        if (stopTimer == false)
        {
            timerSlider.value = Timer;
        }
        else
        {
            //GameOver 호출
        }
    }
}