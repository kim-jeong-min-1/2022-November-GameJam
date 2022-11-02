using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kwrestling
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Kwrestling kwrestling;
        private float EnemyCombineValue;
        private void Start()
        {
            StartCoroutine(AISystem());
        }

        private float RandDelay()
        {
            return Random.Range(0.08f, 0.1f);
        }

        private IEnumerator AISystem()
        {
            while (!kwrestling.GameOver)
            {
                yield return new WaitForSeconds(RandDelay());
                kwrestling.SetGaugeValue(-1);
            }
        }
    }
}
