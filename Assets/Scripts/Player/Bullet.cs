using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [Range(1.0f, 20.0f)]
    public float MoveDistance = 6.0f;
    [Range(0.1f, 10.0f)]
    public float DestroyTime = 3.0f;

    public float Angle = 0.0f;

    private Tweener move_tweener;

    public void Start()
    {
        // 转成弧度
        Angle *= Mathf.Deg2Rad;

        float x = MoveDistance * Mathf.Cos(Angle);
        float y = MoveDistance * Mathf.Sin(Angle);
        Vector3 dest = transform.position + new Vector3(x, y, 0.0f);

        move_tweener = transform.DOMove(dest, DestroyTime).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            move_tweener.Kill();
            Destroy(gameObject);
        }
    }
}