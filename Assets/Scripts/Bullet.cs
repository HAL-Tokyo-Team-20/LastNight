using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10f;

    public void Start()
    {
        StartCoroutine(MyTimer.Wait(() =>
        {
            if (this.gameObject != null)
            {
                GameObject.Destroy(this.gameObject);
            }
        }, 0.5f));
    }

    private void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}