using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerBehavior : MonoBehaviour
{
    public MoveDirEnum MoveDir { get; set; }

    public float MoveSpeed = 3.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // TODO:随机生成服饰等

    }

    void Update()
    {
        if (MoveDir == MoveDirEnum.Left)
        {
            rb.MovePosition(transform.position + (Vector3.left * MoveSpeed * Time.deltaTime));
        }
        else if (MoveDir == MoveDirEnum.Right)
        {
            rb.MovePosition(transform.position + (Vector3.right * MoveSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
