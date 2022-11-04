using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private TextMeshProUGUI ScoreText;

    [HideInInspector] public int GameScore = 0;
    [HideInInspector] public bool isWait = true;

    private List<int> stageRecord = new List<int>();
    private Dictionary<int, int> stageCool = new Dictionary<int, int>();

    [HideInInspector] public bool isGameClear = false;
    [HideInInspector] public bool isGameOver = false;
    private IEnumerator timerCoroutine;

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
        isGameClear = false;
        isGameOver = false;
        StartCoroutine(LoadStage());
    }

    private void StageCoolCheck()
    {
        foreach (int num in stageRecord)
        {
            stageCool[num]++;
            if (stageCool[num] == 3)
            {
                stageRecord.Remove(num);
                stageCool.Remove(num);
            }
        }
    }

    private IEnumerator LoadStage()
    {
        int rand;
        while (true)
        {
            rand = Random.Range(0, 6);
            if (!stageRecord.Contains(rand)) break;
            yield return null;
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
        yield return new WaitForSeconds(3f);

        if (isTimeLimitStage[rand]) StartTimer(rand);

    }

    private void StartTimer(int stage)
    {
        TimerBar.gameObject.SetActive(true);
        TimerBar.value = 0;

        StartCoroutine(TimerLogic(stage));
    }

    private IEnumerator TimerLogic(int stage)
    {
        while (Timer < 1)
        {
            if (isGameClear || isGameOver)
            {
                break;
            }
            Timer += Time.deltaTime * 0.2f;
            yield return new WaitForFixedUpdate();
        }

        if(!isGameClear && !isGameOver)
        {
            if (clearConditions[stage] == ClearConditions.TimeLimit)
            {
                GameOver(true);
            }
            else if (clearConditions[stage] == ClearConditions.HoldOn)
            {
                GameClear(true);
            }
            TimerBar.gameObject.SetActive(false);
        }
    }

    private IEnumerator ScoreSum()
    {
        GameOverUI.SetActive(true);
        int score = 0;
        ScoreText.text = $"{score}";
        while (score != GameScore)
        {
            score += 100;
            ScoreText.text = $"{score}";
            yield return new WaitForSeconds(0.1f);
        }

        while (true)
        {
            if (Input.anyKey)
            {
                GameOverUI.SetActive(false);
                SceneManager.LoadScene("Title");
                break;
            }
            yield return null;
        }
    }

    public void GameClear(bool boolean)
    {
        isGameClear = boolean;

        if (isGameClear)
        {
            if (TimerBar.gameObject.activeSelf) TimerBar.gameObject.SetActive(false);
            GameScore += 100;
            StageCoolCheck();
            RandomStage();
        }
    }

    public void GameOver(bool boolean)
    {
        isGameOver = boolean;

        if (isGameOver)
        {
            if (TimerBar.gameObject.activeSelf) TimerBar.gameObject.SetActive(false);
            stageCool.Clear();
            stageRecord.Clear();
            StartCoroutine(ScoreSum());
        }
    }
}
