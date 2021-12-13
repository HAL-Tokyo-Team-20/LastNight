using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring3D : MonoBehaviour
{
    [Range(1.0f, 3.0f)]
    public float PushPlayerForce;
    SpringJoint spring;
    Rigidbody rb;

    GameObject player;

    public float MaxDist = 1.5f;
    public float MinDist = 1.5f;

    public float SpringValue = 20;
    public float Damper = 5;

    public bool IsActive { get; set; }

    void Start()
    {
        player = GameObjectMgr.Instance.GetGameObject("Player");
    }

    public void Reset()
    {
        player = GameObjectMgr.Instance.GetGameObject("Player");
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

        //if (Input.GetKeyDown(KeyCode.Space))
        if (Input.GetButtonDown("Confirm"))
        {
            IsActive = false;
            Destroy(spring);

            // 给player添加一个推力
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            playerRb.AddForce(Vector3.Normalize(playerRb.velocity) * PushPlayerForce, ForceMode.Impulse);
        }
    }
}
