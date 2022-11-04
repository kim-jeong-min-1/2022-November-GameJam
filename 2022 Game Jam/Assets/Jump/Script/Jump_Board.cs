using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jump_Board : MonoBehaviour
{
    Player Player;

    public bool isOver; // 판 회전 방향 바꾸기
    public bool isClick;
    public bool NoSucces;
    bool NotClick;
    bool OneTime = true;
    bool One = true;
    bool Start_Game;
    public float timer;

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOver)
        {
            SoundManager.Instance.PlaySFX(SoundEffect.Touch);
            isClick = true;

            if (0.7f <= timer)
            {
                Debug.Log("11");
            }
            else
            {
                NoSucces = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) != true && !isClick && !isOver && 0.2f <= timer && One && Start_Game)
        {
            SoundManager.Instance.PlaySFX(SoundEffect.Touch);
            Debug.Log("11111111");
            NotClick = true;
            One = false;
        }

        if (NoSucces && OneTime || NotClick && OneTime)
        {
            Debug.Log("GameOver");
            GameManager.Instance.GameOver(true);
            OneTime = false;
        }

        Move();
        Timer();
    }

    void Move()
    {
        if (GameManager.Instance.isWait) return;
        if (isOver == true) transform.DORotate(new Vector3(0, 0, 15), 2f, RotateMode.LocalAxisAdd);
        else transform.DORotate(new Vector3(0, 0, -15), 2f, RotateMode.LocalAxisAdd);
    }

    void Timer()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOver)
            isOver = false;
        else
            isOver = true;

        timer = 0;

        if (isOver)
            isClick = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        Start_Game = true;
    }
}
