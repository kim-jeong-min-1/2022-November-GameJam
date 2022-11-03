using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slap : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private Ai ai;

    public Slider Slider;

    float Speed; // 핸들 속도
    public bool isStop = false; // 스페이스바 = 멈춤
    public bool isEnd = false; // 핸들 방향 바꾸기
    public bool isAttack = false; // 내 공격 = true
    bool isWait; // 공격 끝나고 대기
    bool Ending;

    private void Start()
    {
        ai.Attack_Wait();
    }

    private void Update()
    {
        if (Ending)
        {
            //넘어가기
        }

        if (!isWait)
        {
            #region Handle
            if (!isStop)
            {
                if (Slider.value == -1)
                    isEnd = false;
                else if (Slider.value == 1)
                    isEnd = true;

                if (isEnd)
                    Speed -= Time.deltaTime * 6;
                else
                    Speed += Time.deltaTime * 6;

                Slider.value = Speed;
            }
            #endregion

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStop = true;

                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        if (isAttack)
        {
            player.Attack();
        }
        else
        {
            ai.Attack();
        }

        yield return new WaitForSeconds(1);

        //판정
        if (-0.15f <= Slider.value && Slider.value <= 0.15f && isAttack)
        {
            player.Win();
            ai.Lose();
        }
        else if (-0.54f <= Slider.value && Slider.value <= 0.54f)
        {
            player.Win();
            ai.Lose();
        }
        else
        {
            ai.Win();
            player.Lose();

            // 게임오버
        }
        if(isAttack)
            Ending = true;

        yield return new WaitForSeconds(1);

        isAttack = true;
        isStop = false;
        isWait = false;

        Speed = 0;

        ai.Idle();
        player.Attack_Wait();
    }
}
