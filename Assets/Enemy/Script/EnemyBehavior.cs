using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class EnemyBehavior : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private VisualEffect vfx;
    private int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        vfx = transform.GetChild(0).GetComponent<VisualEffect>();
        vfx.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            spriteRenderer.enabled = false;
            vfx.Play();
            StartCoroutine(DelayDestroy());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // TODO: 弾丸連続当たるとカラー戻れないバグあり
            spriteRenderer.material.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);
            hp--;
        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
