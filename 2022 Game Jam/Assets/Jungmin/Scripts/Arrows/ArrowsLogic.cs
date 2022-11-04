using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowsLogic : MonoBehaviour
{
    private readonly float MaxXAbs = 6f;

    [SerializeField] private List<GameObject> targets = new List<GameObject>();
    [SerializeField] private GameObject Tiger;
    [HideInInspector] public int hitTarget = 0;
    private bool GameClear = false;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUP(TargetMoveToRand);
    }

    private void Update()
    {
        if(hitTarget == 5)
        {
            print("!");
            GameClear = true;
            GameManager.Instance.GameClear(GameClear);
            hitTarget = 0;
        }
    }

    private void SetUP(System.Action callBack)
    {
        transform.DOMoveY(0, 1f);
        callBack?.Invoke();
    }
    
    private void TargetMoveToRand()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            var rand = Random.Range(0, 2);
            var dir = (rand > 0) ? 1 : -1;
            var speed = Random.Range(8f, 10f);

            StartCoroutine(TargetMoving(targets[i], dir, speed));
        }
    }

    private IEnumerator TargetMoving(GameObject target, int direction, float speed)
    {
        yield return new WaitUntil(() => !GameManager.Instance.isWait);
        while(hitTarget != 4)
        {
            if (Mathf.Abs(target.transform.position.x) >= MaxXAbs) direction = -direction;
            
            target.transform.Translate((Vector3.right * direction) * speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(1f);
        Tiger.SetActive(true);
        Tiger.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear);
    }

}
