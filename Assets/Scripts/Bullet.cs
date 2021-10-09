using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    //[Range(1.0f,10.0f)]
    //public float moveSpeed = 10f;
    [Range(1.0f, 20.0f)]
    public float MoveDistance = 6.0f;
    [Range(0.1f, 10.0f)]
    public float DestroyTime = 3.0f;

    public bool Right = true;

    public void Start()
    {
        if (!Right) MoveDistance *= -1;
        transform.DOMoveX(transform.position.x + MoveDistance, DestroyTime).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
    }

    private void Update()
    {

    }
}