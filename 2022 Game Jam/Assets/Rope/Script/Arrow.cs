using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    RopeManager gameManager;

    public int My_Number;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RopeManager.Instance.Hit_Arrow = this.gameObject;
    }
}