using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kwrestling
{
    public class Kwrestling : MonoBehaviour
    {
        private readonly float CombineValue = 0.1f;
        [SerializeField] private Slider gaugeSlider;

        public bool GameOver;
        private string Winner;

        public float Gauge
        {
            get => gaugeSlider.value;
            set
            {
                gaugeSlider.value = value;
            }
        }

        public void SetGaugeValue(int abs)
        {
            if (GameOver) return;
            Gauge = Gauge + (CombineValue * abs);
        }

        // Update is called once per frame
        void Update()
        {
            if (Gauge >= 1)
            {
                GameOver = true;
                Winner = "Player";
            }
            else if (Gauge <= 0)
            {
                GameOver = true;
                Winner = "Enemy";
            }
        }
    }
}
