using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 武器基类, 继承自物品基类
public class IWeapon : IItem
{
    // 攻击力
    public float ATK{ get; set;}
    // 攻击距离
    public float ATKDistance { get; set; }
    // 拥有的最大弹药数
    public int BulletCount { get; set; }
}
