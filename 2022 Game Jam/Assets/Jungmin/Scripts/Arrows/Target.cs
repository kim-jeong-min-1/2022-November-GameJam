using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour
{
    [SerializeField] private Sprite hitSprite;
    public bool isHit;
    public IEnumerator HitArrow()
    {
        SoundManager.Instance.PlaySFX(SoundEffect.Hit);
        var sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = hitSprite;
        yield return new WaitForSeconds(0.5f);

        while (sprite.color.a > 0)
        {
            Color color = sprite.color;
            color.a -= 0.1f;

            sprite.color = color;
            yield return new WaitForSeconds(0.1f);
        }

        this.gameObject.SetActive(false);
        yield return null;
    }
}
