using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kwrestling
{
    public class Kwrestling : MonoBehaviour
    {
        private readonly int CombineValue = 1;
        private readonly Color PlayerColor = new Color(255 / 255f, 55/ 255f, 150 / 255f, 1);
        private readonly Color EnemyColor = new Color(65 / 255f, 60 / 255f, 215 / 255f, 1);

        [SerializeField] private List<SpriteRenderer> gauges = new List<SpriteRenderer>();
        [SerializeField] private GameObject ClickUI => transform.GetChild(0).gameObject;
        [SerializeField] private GameObject KO => transform.GetChild(1).gameObject;

        public bool GameOver;
        private string Winner;

        private int gaugeCenter = 12;
        public int Gauge
        {
            get => gaugeCenter;
            set
            {
                gaugeCenter = value;
            }
        }

        public void ChangeGaugeValue(int abs)
        {
            if (GameOver) return;

            Gauge = Gauge + (CombineValue * abs);
            print(Gauge);
            ChangeGaugeColor();

            transform.DOShakePosition(0.1f, 0.1f, 10, 10);

            ClickUI.SetActive(true);
            ClickUI.transform.DOScale(Vector3.one, 0.15f);

            Invoke("ClickUISetUp", 0.15f);
        }

        private void ChangeGaugeColor()
        {
            for (int i = 0; i < gauges.Count; i++)
            {
                Color color = (i + 1 <= gaugeCenter) ? PlayerColor : EnemyColor;
                gauges[i].color = color;
            }
        }

        private void ClickUISetUp()
        {
            ClickUI.transform.localScale = Vector3.one * 0.8f;
            ClickUI.SetActive(false);
        }

        private void GameOverUI()
        {
            KO.SetActive(true);
            KO.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutQuad);
        }

        // Update is called once per frame
        void Update()
        {
            if (Gauge >= 24)
            {
                GameOver = true;
                Winner = "Player";
            }
            else if (Gauge <= 0)
            {
                GameOver = true;
                Winner = "Enemy";
            }

            if (GameOver) GameOverUI();
        }
    }
}
