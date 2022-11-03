using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dak_ge : MonoBehaviour
{
    public List<Sprite> sprite = new List<Sprite>();

    SpriteRenderer Ren;

    void Start()
    {
        Ren = GetComponent<SpriteRenderer>();
    }

    void Ai_Idle()
    {
        Ren.sprite = sprite[0];
    }

    void Ai_Win()
    {
        Ren.sprite = sprite[1];
    }

    void Player_Idle()
    {
        Ren.sprite = sprite[2];
    }

    void Player_Win()
    {
        Ren.sprite = sprite[3];
    }
}
