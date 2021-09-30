using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint3DTest : MonoBehaviour
{
    public float Force = 1.0f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.right * Force, ForceMode.VelocityChange);
        }
    }
}
