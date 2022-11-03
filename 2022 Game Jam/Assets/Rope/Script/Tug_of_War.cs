using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tug_of_War : MonoBehaviour
{

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator> ();
    }

    public void Win()
    {
        anim.Play("Player_Win");
        // �Ѿ��
    }

    public void Lose()
    {
        anim.Play("AI_Win");
        // ���ӿ���
    }
}
