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
        [HideInInspector] public PlayerState playerState = PlayerState.Stop;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(Dance(Color.yellow));
                playerState = PlayerState.Left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(Dance(Color.red));
                playerState = PlayerState.Right;
            }
        }

        private IEnumerator Dance(Color dirColor)
        {
            spriteRenderer.color = dirColor;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = Color.white;
            playerState = PlayerState.Stop;
        }
    }
}
