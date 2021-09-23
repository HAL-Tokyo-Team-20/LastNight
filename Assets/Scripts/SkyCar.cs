using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 可能出现的问题: 1. 用时间控制销毁可能不准确, 可改用位置控制
public class SkyCar : MonoBehaviour
{
    public MoveDirEnum MoveDir { get; set; }
    public float MoveSpeed;
    [Tooltip("SkyCar销毁时间")]
    public float DeadTime;

    void Start()
    {
        MoveDir = MoveDirEnum.Left;
        // 指定时间后销毁自身
        StartCoroutine(MyTimer.Wait(() =>
        {
            GameObject.Destroy(this.gameObject);
        }, DeadTime));
    }

    void Update()
    {
        //TODO 更顺滑的移动
        if (MoveDir == MoveDirEnum.Left)
        {
            transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime, Space.World);
        }
        else if (MoveDir == MoveDirEnum.Right)
        {
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime, Space.World);
        }
        else if (MoveDir == MoveDirEnum.Forward)
        {
            transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime, Space.World);
        }
    }
}
