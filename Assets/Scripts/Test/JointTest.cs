using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointTest : MonoBehaviour
{
    //HingeJoint2D hingeJoint2D;
    SpringJoint2D springJoint2D;

    bool isStart = false;
    bool shortDist = false;

    int cnt = 0;

    void Start()
    {
        springJoint2D = GetComponent<SpringJoint2D>();
        springJoint2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            cnt++;
            // 10 帧内, 每帧缩小弹簧距离
            if (cnt >= 120)
            {
                shortDist = true;
            }
            else
            {
                springJoint2D.distance -= (2.0f / 120f);
            }

            // 已经缩短了距离 且 按下了空格
            if (shortDist && Input.GetKeyDown(KeyCode.Space))
            {
                springJoint2D.enabled = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStart = true;
                springJoint2D.enabled = true;
            }
        }
    }
}
