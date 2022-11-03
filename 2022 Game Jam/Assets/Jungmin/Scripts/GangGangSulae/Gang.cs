using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaitTime
{
    ExactBeat = 1,
    OffBeat = 2,
    FastBeat = 3
}

public enum GangGangSulaeState
{
    Stop = 0,
    Left = 1,
    Right = 2
}

namespace GangGangSulae
{
    public class Gang : MonoBehaviour
    {
        private readonly float ExactBeat = 2f;
        private readonly float OffBeat = 1.2f;
        private readonly float FasetBeat = 0.8f;
      
        [SerializeField] private List<SpriteRenderer> aiSprites = new List<SpriteRenderer>();
        [SerializeField] private Player player;
        private bool GameOver = false;

        private WaitTime waitTime;
        private GangGangSulaeState state = GangGangSulaeState.Stop;

        private void Start()
        {
            StartCoroutine(GangGangSulaeLogic());
        }

        private float RandWaitTime()
        {
            var num = Random.Range(1, 4);
            waitTime = (WaitTime)num;

            float wtime = waitTime switch
            {
                WaitTime.ExactBeat => ExactBeat,
                WaitTime.OffBeat => OffBeat,
                WaitTime.FastBeat => FasetBeat
            };

            return wtime;
        }

        private void ChangeSprite(GangGangSulaeState state)
        {
            Color color = state switch
            {
                GangGangSulaeState.Left => Color.yellow,
                GangGangSulaeState.Right => Color.red,
                GangGangSulaeState.Stop => Color.white
            };

            for (int i = 0; i < aiSprites.Count; i++)
            {
                aiSprites[i].color = color;
            }
        }

        private void GameOverCheck()
        {
            if((int)player.playerState != (int)state)
            {
                GameOver = true;
            }
        }

        private IEnumerator GangGangSulaeLogic()
        {
            while (!GameOver)
            {
                state = GangGangSulaeState.Stop;
                ChangeSprite(state);
                yield return new WaitForSeconds(RandWaitTime());

                state = (GangGangSulaeState)Random.Range(1, 3);
                ChangeSprite(state);
                yield return new WaitForSeconds(0.5f);
                GameOverCheck();
            }
        }
    }
}
