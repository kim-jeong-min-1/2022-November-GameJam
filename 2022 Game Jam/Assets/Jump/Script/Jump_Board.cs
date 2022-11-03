using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jump_Board : MonoBehaviour
{

    Player Player;

    public bool isOver = false;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if(isOver == true) transform.DORotate(new Vector3(0, 0, 15), 2f, RotateMode.LocalAxisAdd);
        else transform.DORotate(new Vector3(0, 0, -15), 2f, RotateMode.LocalAxisAdd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Over_Check")
        {
            if (isOver == true) isOver = false;
            else isOver = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "Click_Check")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.Jump();
            }
        }
    }
}
