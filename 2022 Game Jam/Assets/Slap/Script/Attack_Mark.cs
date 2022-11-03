using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Mark : MonoBehaviour
{
    public List<Sprite> sprite = new List<Sprite>();

    SpriteRenderer Ren;

    void Start()
    {
        Ren = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        Ren.sprite = sprite[0];
    }

    public void Defense()
    {
        Ren.sprite = sprite[1];
    }
}
