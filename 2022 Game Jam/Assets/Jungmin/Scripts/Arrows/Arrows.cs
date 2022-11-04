using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    [SerializeField] private ArrowsLogic arrowsLogic;
    [SerializeField] private SpriteRenderer arrowPosition;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject missEffect;

    [SerializeField] private Sprite NormalArrow;
    [SerializeField] private Sprite HeartArrow;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isWait) return;
        ArrowToMousePoint();

        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.isGameOver)
        {
            ShootAnArrow();
        }
    }

    private IEnumerator ReLoadArrow(Sprite ArrowSprite)
    {
        yield return new WaitForSeconds(0.5f);
        arrowPosition.sprite = ArrowSprite;
    }

    private void ArrowToMousePoint()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Clamp(pos.x, -7, 7), Mathf.Clamp(pos.y, -2.9f, 2.9f));
    }

    private void ShootAnArrow()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;

        hit = Physics2D.Raycast(ray.origin, Vector2.zero, LayerMask.GetMask("Target"));

        if (hit.collider != null)
        {
            var target = hit.collider.GetComponent<Target>();
            arrowPosition.sprite = null;

            var obj = Instantiate(hitEffect, hit.transform.position, Quaternion.identity);
            Destroy(obj, 0.2f);

            if(!target.isHit)arrowsLogic.hitTarget++;
            target.isHit = true;
            StartCoroutine(target.HitArrow());

            if (arrowsLogic.hitTarget != 4) StartCoroutine(ReLoadArrow(NormalArrow));
            else StartCoroutine(ReLoadArrow(HeartArrow));
        }
        else
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            var obj = Instantiate(missEffect, pos, Quaternion.identity);
            Destroy(obj, 0.2f);
        }
    }
}
