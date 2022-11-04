using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public List<Sprite> sprite = new List<Sprite>();

    SpriteRenderer Ren;

    void Start()
    {
        Ren = GetComponent<SpriteRenderer>();
    }

    public void Idle()
    {
        Ren.sprite = sprite[0];
    }

    public void Attack_Wait()
    {
        Ren.sprite = sprite[1];
    }

    public void Attack()
    {
        Ren.sprite = sprite[2];
    }

    public void Win()
    {
        Ren.sprite = sprite[3];
    }

    public void Lose()
    {
        Ren.sprite = sprite[4];
    }
}
