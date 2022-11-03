using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : Singleton<EventManager>
{
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private Image Fade;
    [SerializeField] private Image Count;
    [SerializeField] private Text Explain;

    [SerializeField] private Sprite[] countSprites = new Sprite[3];
    [SerializeField] private string[] stageExplain = new string[6];
    
    public void StageStart(int stage)
    {
        StartCoroutine(CountDown(stage));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeAtValue(1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeAtValue(-1));
    }

    private IEnumerator CountDown(int stage)
    {
        Explain.text = stageExplain[stage];
        StartMenu.SetActive(true);

        for (int i = 2; i >= 0; i--)
        {
            Count.sprite = countSprites[i];
            yield return new WaitForSeconds(1f);
        }

        StartMenu.SetActive(false);
        GameManager.Instance.isWait = false;
    }

    private IEnumerator FadeAtValue(int value)
    {
        while (true)
        {
            Color color = Fade.color;
            color.a += 0.01f * value;

            Fade.color = color;
            if (Fade.color.a >= 1 || Fade.color.a <= 0) break;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
