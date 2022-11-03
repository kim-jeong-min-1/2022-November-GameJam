using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
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

    public void Win()
    {
        Ren.sprite = sprite[1];
    }
}
