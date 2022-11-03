using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector2(0, 0);
    }

    public void Check_Arrow()
    {
        transform.position = new Vector2(0, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    public void End_Check()
    {
        transform.position = new Vector2(0, 0);
    }
}
