using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jump_Board : MonoBehaviour
{

    Player Player;

    

    public bool isOver = false;
    public bool ClickTime = false; 
    public bool ClickCheck = false;
    public bool isClick = true;
    public bool isSucces = true;

    private void Update()
    {
        if (ClickTime == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClickCheck = true;
                isClick = true;
                //Player.Jump();
            }
        }
        Move();
        
    }

    void Move()
    {
        if(isOver == true) transform.DORotate(new Vector3(0, 0, 15), 2f, RotateMode.LocalAxisAdd);
        else transform.DORotate(new Vector3(0, 0, -15), 2f, RotateMode.LocalAxisAdd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Click_Check")
        {
            ClickTime = true;
            ClickCheck = false;
        }
        if(collision.name == "Over_Check")
        {
            if (isOver == true) isOver = false;
            else isOver = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Click_Check")
        {
            ClickTime = false;
            if (ClickCheck == false)
            {
                isClick = false;
            }

            if (isClick) isSucces = true;
            else isSucces = false;

            if (isSucces != true)
            {
                Debug.Log("1111");
                //게임 오버
            }
        }
    }
}
