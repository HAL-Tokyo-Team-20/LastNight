using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBullet : MonoBehaviour
{
    [Range(1.0f, 20.0f)]
    public float MoveDistance = 6.0f;
    [Range(0.1f, 10.0f)]
    public float DestroyTime = 3.0f;

    public bool Right = false;

    private Tweener move_tweener;

    public void Start()
    {
        if (!Right) MoveDistance *= -1;
        move_tweener = transform.DOMoveX(transform.position.x + MoveDistance, DestroyTime).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            move_tweener.Kill();
            Destroy(gameObject);
        }
    }
}
