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
            return Random.Range(0.15f, 0.2f);
        }

        private IEnumerator AISystem()
        {
            yield return new WaitUntil(() => !GameManager.Instance.isWait);
            while (!kwrestling.GameOver)
            {
                yield return new WaitForSeconds(RandDelay());
                kwrestling.ChangeGaugeValue(-1);
            }
        }
    }
}
