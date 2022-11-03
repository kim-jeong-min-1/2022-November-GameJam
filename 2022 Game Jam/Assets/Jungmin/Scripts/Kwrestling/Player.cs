using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kwrestling
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Kwrestling kwrestling;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isWait)
            {
                kwrestling.ChangeGaugeValue(1);
            }
        }
    }
}