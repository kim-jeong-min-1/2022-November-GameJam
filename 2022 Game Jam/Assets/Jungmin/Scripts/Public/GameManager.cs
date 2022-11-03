using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ClearConditions
{
    None,
    HoldOn,
    TimeLimit
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool[] isTimeLimitStage = new bool[6];
    [SerializeField] private string[] StageName = new string[6];
    [SerializeField] private ClearConditions[] clearConditions = new ClearConditions[6];

    [SerializeField] private Slider TimerBar;

    [HideInInspector] public int GameScore = 0;
    [HideInInspector] public bool isWait = true;

    private List<int> stageRecord = new List<int>();
    private Dictionary<int, int> stageCool = new Dictionary<int, int>();

    private bool isGameClear = false;
    private bool isGameOver = false;
    private IEnumerator timerCoroutine;
    private IEnumerator loadStageCoroutine;

    public float Timer
    {
        get => TimerBar.value;
        set
        {
            TimerBar.value = value;
        }
    }

    public void RandomStage()
    {
        loadStageCoroutine = LoadStage();
        isGameClear = false;
        isGameOver = false;
        StartCoroutine(loadStageCoroutine);
    }

    private void StageCoolCheck()
    {
        foreach (int num in stageRecord)
        {
            stageCool[num]++;
            if(stageCool[num] == 3)
            {
                stageRecord.Remove(num);
            }
        }
    }

    private IEnumerator LoadStage()
    {
        var rand = Random.Range(0, 6);

        //3번 이내에 플레이한 이력이 있는 스테이지라면 패스
        if(stageRecord.Contains(rand))
        {
            StopCoroutine(loadStageCoroutine);
            RandomStage();
        }
        stageRecord.Add(rand);
        stageCool.Add(rand, 0);
        isWait = true;

        EventManager.Instance.FadeIn();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(StageName[rand]);

        EventManager.Instance.FadeOut();
        yield return new WaitForSeconds(0.8f);

        EventManager.Instance.StageStart(rand);
        yield return new WaitForSeconds(3.15f);

        if(isTimeLimitStage[rand]) StartTimer(rand);
    }

    private void StartTimer(int stage)
    {
        TimerBar.gameObject.SetActive(true);
        TimerBar.value = 0;

        timerCoroutine = TimerLogic(stage);
        StartCoroutine(timerCoroutine);
    }

    private IEnumerator TimerLogic(int stage)
    {
        while (Timer < 1)
        {
            if(isGameClear || isGameOver)
            {
                TimerBar.gameObject.SetActive(false);
                StopCoroutine(timerCoroutine);
            }
            Timer += Time.deltaTime * 0.2f;
            yield return new WaitForFixedUpdate();
        }

        if (clearConditions[stage] == ClearConditions.TimeLimit)
        {
            GameOver(true);
        }
        else if(clearConditions[stage] == ClearConditions.HoldOn)
        {
            GameClear(true);
        }
        TimerBar.gameObject.SetActive(false);
    }

    private IEnumerator ScoreSum()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
        SceneManager.LoadScene("Title");
    }

    public void GameClear(bool boolean)
    {
        isGameClear = boolean;

        if (isGameClear)
        {
            GameScore += 100;
            StageCoolCheck();
            Invoke("RandomStage", 1f);
        }
    }

    public void GameOver(bool boolean)
    {
        isGameOver = boolean;

        if (isGameOver)
        {
            StartCoroutine(ScoreSum());
        }
    }
}
