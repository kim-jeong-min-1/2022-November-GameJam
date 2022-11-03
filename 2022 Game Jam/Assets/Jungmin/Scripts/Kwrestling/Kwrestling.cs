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
        private readonly Color PlayerColor = new Color(65 / 255f, 60 / 255f, 215 / 255f, 1);
        private readonly Color EnemyColor = new Color(255 / 255f, 55 / 255f, 150 / 255f, 1);

        [SerializeField] private List<SpriteRenderer> gauges = new List<SpriteRenderer>();
        [SerializeField] private GameObject ClickUI => transform.GetChild(0).gameObject;
        [SerializeField] private GameObject KoUI => transform.GetChild(1).gameObject;
        [SerializeField] private Sprite koSprite;

        public bool GameOver;

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

            float PosY = Random.Range(-3, 4);
            float PosX = Random.Range(-1.5f, 3.5f);
            ClickUI.transform.position = new Vector2(PosX, PosY);

            ClickUI.SetActive(false);
        }

        private void GameOverUI()
        {
            KoUI.SetActive(true);
            KoUI.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutQuad);
        }

        // Update is called once per frame
        void Update()
        {
            if (Gauge >= 24)
            {
                GameOver = true;
                GameOverUI();
                GetComponent<SpriteRenderer>().sprite = koSprite;
                GameManager.Instance.GameClear(GameOver);

            }
            else if (Gauge <= 0)
            {
                GameOver = true;
                GameOverUI();
                GameManager.Instance.GameOver(GameOver);
            }
        }
    }
}
