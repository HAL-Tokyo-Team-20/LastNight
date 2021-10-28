using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerBehavior : MonoBehaviour
{

    public Vector3 MoveDirection = new Vector3(-1, 0, 0);

    public float MoveSpeed = 3.0f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + (MoveDirection * MoveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
