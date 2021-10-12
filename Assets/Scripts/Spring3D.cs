using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring3D : MonoBehaviour
{
    SpringJoint spring;
    Rigidbody rb;

    public float MaxDist = 0.6f;
    public float MinDist = 0.2f;

    public bool IsActive { get; set; }

    void Start()
    {

    }

    public void Reset()
    {
        GameObject player = GameObjectMgr.Instance.GetGameObject("Player");
        spring = gameObject.AddComponent<SpringJoint>();
        spring.connectedBody = player.GetComponent<Rigidbody>();
        spring.anchor = Vector3.zero;
        spring.connectedAnchor = Vector3.zero;
        spring.autoConfigureConnectedAnchor = false;
        spring.spring = 20;
        spring.damper = 5;
        float dist = Vector3.Distance(player.transform.Find("center_point").transform.position, transform.position);
        spring.maxDistance = dist * MaxDist;
        spring.minDistance = dist * MinDist;



        IsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsActive = false;
            Destroy(spring);
        }
    }
}
