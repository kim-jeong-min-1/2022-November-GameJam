using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    GameManager gameManager;

    public int My_Number;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.Hit_Arrow = this.gameObject;
    }
}