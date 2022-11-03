using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slap : MonoBehaviour
{
    public Slider Slider;
    public GameObject Handle;

    public float Handle_Speed;
    public bool isStop = false;
    public bool isEnd = false;
    public bool Set_One = true;
    public bool isAttack = false;

    private void Update()
    {
        if(Slider.value == -1)
            isEnd = false;
        else if(Slider.value == 1)
            isEnd = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStop = true;
        }

        if (isAttack != true)
            Handle_Move();
        else
            Handle_Move();
    }

    void Handle_Move()
    {
        if (isStop != true)
        {
            if (isEnd)
            Handle_Speed -= Time.deltaTime * 6;
        else if(isEnd != true)
            Handle_Speed += Time.deltaTime * 6;

            Slider.value = Handle_Speed;
        }

        if (isStop)
            Result();
    }

    void Result()
    {
        if(-0.54f <= Slider.value || Slider.value <= 0.54f)
        {
            Good();

            if(-0.15f <= Slider.value || Slider.value <= 0.15f)
            {
                Perfect();
            }
        }
        else
        {
            Falled();
        }
    }

    void Good()
    {
        if(isAttack != false)
        {
            //좋은 방어 모션
            isAttack = false;
        }
        else
        {
            //좋은 공격 모션
            //승리 함수
        }
    }

    void Perfect()
    {
        if (isAttack != false)
        {
            //개좋은 방어 모션
            isAttack = false;
        }
        else
        {
            //개좋은 공격 모션
            //승리 함수
        }
    }

    void Falled()
    {
        //실패
        //게임오버 함수
    }
}
