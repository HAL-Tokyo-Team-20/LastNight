using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 可被选取的物体的接口
// 继承此接口, 需要实现对玩家的距离
public interface ISelectItem
{
    public bool Selected { get; set; }
    public bool ConfirmSelected { get; set; }
    public bool PlayerIsEnter { get; set; }
    public float DistanceToPlayer();
}
