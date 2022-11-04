using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        private readonly float ExactBeat = 1f;
        private readonly float OffBeat = 0.6f;
        private readonly float FasetBeat = 0.4f;

        [SerializeField] private List<SpriteRenderer> aiSprites = new List<SpriteRenderer>();
        [SerializeField] private List<Animator> aiAnimators = new List<Animator>();
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
            bool flip = state switch
            {
                GangGangSulaeState.Left => false,
                GangGangSulaeState.Right => true,
            };

            int dir = state switch
            {
                GangGangSulaeState.Left => -1,
                GangGangSulaeState.Right => 1,
            };

            for (int i = 0; i < aiSprites.Count; i++)
            {
                if (state == GangGangSulaeState.Stop) continue;
                aiSprites[i].flipX = flip;

                var ai = aiSprites[i].gameObject.transform;
                ai.DOMoveX(ai.position.x + (1f * dir), 0.2f).SetEase(Ease.Linear);
                aiSprites[i].GetComponent<Animator>().SetTrigger("isMove");
            }
            player.gameObject.transform.
                DOMoveX(player.gameObject.transform.position.x + (1f * dir), 0.2f).SetEase(Ease.Linear);

            StartCoroutine(CameraMove(dir));
        }

        private IEnumerator CameraMove(int dir)
        {
            yield return new WaitForSeconds(0.1f);
            Camera.main.transform.DOMoveX(Camera.main.transform.position.x + (1.2f * dir), 0.2f).SetEase(Ease.Linear);
        }

        private void GameOverCheck()
        {
            if ((int)player.playerState != (int)state)
            {
                GameOver = true;
                GameManager.Instance.GameOver(GameOver);
            }
        }


        private IEnumerator GangGangSulaeLogic()
        {
            yield return new WaitUntil(() => !GameManager.Instance.isWait);
            while (!GameOver && !GameManager.Instance.isGameClear)
            {
                state = GangGangSulaeState.Stop;
                yield return new WaitForSeconds(RandWaitTime());

                state = (GangGangSulaeState)Random.Range(1, 3);
                ChangeSprite(state);

                yield return new WaitForSeconds(0.5f);
                if(!GameManager.Instance.isGameClear) GameOverCheck();
            }
        }
    }
}
