using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slap : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private Ai ai;
    [SerializeField] private Dak_ge dak_ge;
    [SerializeField] private Attack_Mark mark;
    [SerializeField] private BackGround backGround;

    public Slider Slider;
    public GameObject Effect;

    float Speed; // 핸들 속도
    public bool isStop = false; // 스페이스바 = 멈춤
    public bool isEnd = false; // 핸들 방향 바꾸기
    public bool isAttack = false; // 내 공격 = true
    bool isWait; // 공격 끝나고 대기
    bool Ending;
    bool win;

    private void Start()
    {
        ai.Attack_Wait();
        dak_ge.Ai_Idle();
        mark.Defense();
        backGround.Idle();
    }

    private void Update()
    {
        if (Ending)
        {
            GameManager.Instance.GameClear(true);
            Ending = false;
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

            if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isWait)
            {
                isStop = true;
                SoundManager.Instance.PlaySFX(SoundEffect.Dakzi);
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
            dak_ge.Player_Win();
            Effect.transform.position = new Vector2(1.89f, 4.93f);
        }
        else if (-0.54f <= Slider.value && Slider.value <= 0.54f)
        {
            player.Win();
            ai.Lose();
            dak_ge.Player_Win();
            win = true;
        }
        else
        {
            ai.Win();
            player.Lose();
            dak_ge.Ai_Win();
            win = true;

            yield return new WaitForSeconds(1f);
            GameManager.Instance.GameOver(win);
        }

        if (isAttack && win)
        {
            backGround.Win();
            yield return new WaitForSeconds(1.2f);
            Ending = true;
        }

        yield return new WaitForSeconds(1);

        isAttack = true;
        isStop = false;
        isWait = false;

        Speed = 0;

        ai.Idle();
        player.Attack_Wait();
        dak_ge.Player_Idle();
        mark.Attack();
    }
}
