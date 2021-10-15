using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring3D : MonoBehaviour
{
    SpringJoint spring;
    Rigidbody rb;

    public float MaxDist = 1.5f;
    public float MinDist = 1.5f;

    public float SpringValue = 20;
    public float Damper = 5;

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
        spring.spring = SpringValue;
        spring.damper = Damper;
        //float dist = Vector3.Distance(player.transform.Find("center_point").transform.position, transform.position);
        //spring.maxDistance = dist * MaxDist;
        //spring.minDistance = dist * MinDist;
        spring.maxDistance = MaxDist;
        spring.minDistance = MinDist;



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
