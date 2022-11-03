using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public float Jump_Power;

    Rigidbody2D rigid;
    Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    public void Jump()
    {
        
    }
}
