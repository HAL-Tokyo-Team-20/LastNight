using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 可能出现的问题: 1. 用时间控制销毁可能不准确, 可改用位置控制
public class SkyCar : MonoBehaviour
{
    public MoveDirEnum MoveDir { get; set; }
    public float MoveSpeed;
    public float DeadEnd;

    void Start()
    {
        // 指定时间后销毁自身 
        if (MoveDir == MoveDirEnum.Left)
        {
            transform.DOMoveX(transform.position.x - DeadEnd, DeadEnd / MoveSpeed).SetEase(Ease.OutQuad).SetAutoKill().OnComplete(() =>
               {
                   GameObject.Destroy(this.gameObject);
               });
        }
        else if (MoveDir == MoveDirEnum.Right)
        {
            transform.DOMoveX(transform.position.x + DeadEnd, DeadEnd / MoveSpeed).SetEase(Ease.OutQuad).SetAutoKill().OnComplete(() =>
               {
                   GameObject.Destroy(this.gameObject);
               });
        }
        else if (MoveDir == MoveDirEnum.Forward)
        {
            transform.DOMoveZ(transform.position.z - DeadEnd, DeadEnd / MoveSpeed).SetEase(Ease.OutQuad).SetAutoKill().OnComplete(() =>
               {
                   GameObject.Destroy(this.gameObject);
               });
        }

    }
}
