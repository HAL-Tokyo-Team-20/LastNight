using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring3D : MonoBehaviour
{
    SpringJoint spring;
    Rigidbody rb;
    public GameObject player;

    public float MaxDist = 0.6f;
    public float MinDist = 0.2f;

    void Start()
    {
        spring = GetComponent<SpringJoint>();
        spring.connectedBody = player.GetComponent<Rigidbody>();
        float dist = Vector3.Distance(player.transform.position, transform.position);
        spring.maxDistance = dist * MaxDist;
        spring.minDistance = dist * MinDist;
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
