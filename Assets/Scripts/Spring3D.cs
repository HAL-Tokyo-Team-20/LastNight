using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring3D : MonoBehaviour
{
    SpringJoint spring;
    Rigidbody rb;
    public GameObject player;

    void Start()
    {
        spring = GetComponent<SpringJoint>();
        spring.connectedBody = player.GetComponent<Rigidbody>();
        float dist = Vector3.Distance(player.transform.position, transform.position);
        spring.maxDistance = dist * 0.6f;
        spring.minDistance = dist * 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(spring);
        }
    }
}
