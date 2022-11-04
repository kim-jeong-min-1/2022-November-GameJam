using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RopeManager : MonoBehaviour
{
    public static RopeManager Instance { get; private set; }
    private void Awake() => Instance = this;

    public int Player_Click;
    public int Arrow_Number;
    float Timer;
    public bool stopTimer = false;
    bool isClick = true;

    public List<GameObject> enemyObjs = new List<GameObject>();
    public List<Transform> Point = new List<Transform>();
    public List<GameObject> ArrowList = new List<GameObject>();

    public GameObject Hit_Arrow;
    public Slider timerSlider;
    public Image fall;

    [SerializeField] private Tug_of_War war;
    [SerializeField] private Check Check;
    [SerializeField] private GameObject ClickEffect;
    Arrow Arrow;

    private void Start()
    {
        Spawn_Arrow();
    }

    private void Update()
    {
        if (ArrowList.Count == 0)
        {
            war.Win();
            GameManager.Instance.GameClear(true);
            ArrowList.Add(new GameObject("!!"));
        }
        else
        {
           _Timer();
        }
        if (isClick) Check_Click();
    }

    void Spawn_Arrow() //화살표 생성 1~2번 사용
    {
        transform.position = new Vector2(-7, 3);

        for (int i = 0; i < Point.Count; i++) //생성
        {
            int ranEnemy = Random.Range(0, enemyObjs.Count);

            GameObject arrow = Instantiate(enemyObjs[ranEnemy], Point[i].position, Point[i].rotation);
            ArrowList.Add(arrow);
            arrow.GetComponent<Arrow>().My_Number = ranEnemy += 1;
        }
    }

    void Check_Click() //클릭체크
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Player_Click = 1;
            if (ArrowList.Count > 0)
            {
                if (ArrowList[0].GetComponent<Arrow>()?.My_Number == Player_Click)
                {
                    var temp = ArrowList[0];
                    var effect = Instantiate(ClickEffect, ArrowList[0].transform.position, Quaternion.identity);
                    ArrowList.RemoveAt(0);

                    Destroy(effect, 0.1f);
                    Destroy(temp);
                }
                else
                {
                    while (ArrowList.Count > 0)
                    {
                        var temp = ArrowList[0];
                        ArrowList.RemoveAt(0);
                        Destroy(temp);
                    }

                    if (isClick)
                        StartCoroutine("Miss");

                    Spawn_Arrow();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Player_Click = 2;
            if (ArrowList.Count > 0)
            {
                if (ArrowList[0].GetComponent<Arrow>()?.My_Number == Player_Click)
                {
                    var temp = ArrowList[0];
                    var effect = Instantiate(ClickEffect, ArrowList[0].transform.position, Quaternion.identity);
                    ArrowList.RemoveAt(0);

                    Destroy(effect, 0.1f);
                    Destroy(temp);
                }
                else
                {
                    while (ArrowList.Count > 0)
                    {
                        var temp = ArrowList[0];
                        ArrowList.RemoveAt(0);
                        Destroy(temp);
                    }

                    if (isClick)
                        StartCoroutine("Miss");

                    Spawn_Arrow();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Player_Click = 3;
            if (ArrowList.Count > 0)
            {
                if (ArrowList[0].GetComponent<Arrow>()?.My_Number == Player_Click)
                {
                    var temp = ArrowList[0];
                    var effect = Instantiate(ClickEffect, ArrowList[0].transform.position, Quaternion.identity);
                    ArrowList.RemoveAt(0);

                    Destroy(effect, 0.1f);
                    Destroy(temp);
                }
                else
                {
                    while (ArrowList.Count > 0)
                    {
                        var temp = ArrowList[0];
                        ArrowList.RemoveAt(0);
                        Destroy(temp);
                    }

                    if (isClick)
                        StartCoroutine("Miss");

                    Spawn_Arrow();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Player_Click = 4;
            if (ArrowList.Count > 0)
            {
                if (ArrowList[0].GetComponent<Arrow>()?.My_Number == Player_Click)
                {
                    var temp = ArrowList[0];
                    var effect = Instantiate(ClickEffect, ArrowList[0].transform.position, Quaternion.identity);
                    ArrowList.RemoveAt(0);

                    Destroy(effect, 0.1f);
                    Destroy(temp);
                }
                else
                {
                    while (ArrowList.Count > 0)
                    {
                        var temp = ArrowList[0];
                        ArrowList.RemoveAt(0);
                        Destroy(temp);
                    }
                    
                    if(isClick)
                        StartCoroutine("Miss");

                    Spawn_Arrow();
                }
            }
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

        if (GameManager.Instance.Timer == 1) war.Lose();
        //fall.color = Color.red;
        //Timer += Time.deltaTime * 0.2f;

        //if (timerSlider.value == 1)
        //{
        //    stopTimer = true;
        //}

        //if (stopTimer == false)
        //{
        //    timerSlider.value = Timer;
        //}
        //else
        //{
        //    war.Lose();
        //}
    }
}