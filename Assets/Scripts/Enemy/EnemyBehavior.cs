using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class EnemyBehavior : MonoBehaviour
{

    protected int hp = 3;
    private SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void BeAttack()
    {
        // TODO: 弾丸連続?たるとカ?ー戻れないバグ?り
        spriteRenderer.material.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);
        hp--;
    }

    protected virtual void Dead()
    {
        //spriteRenderer.enabled = false;
        StartCoroutine(DelayDestroy(1.5f));
    }

    private IEnumerator DelayDestroy(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
