using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GangGangSulae
{
    public enum PlayerState
    {
        Stop,
        Left,
        Right,
    }

    public class Player : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
        private Animator animator => GetComponent<Animator>();
        [HideInInspector] public PlayerState playerState = PlayerState.Stop;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(Dance(false));
                playerState = PlayerState.Left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(Dance(true));
                playerState = PlayerState.Right;
            }
        }

        private IEnumerator Dance(bool flip)
        {
            SoundManager.Instance.PlaySFX(SoundEffect.Touch);
            spriteRenderer.flipX = flip;
            animator.SetTrigger("isMove");

            yield return new WaitForSeconds(0.3f);
            playerState = PlayerState.Stop;
        }
    }
}
