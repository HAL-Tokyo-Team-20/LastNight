using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敌人基类
public class IEnemy : MonoBehaviour
{
    // 行走方法
    public virtual void Walk(){}
    // 攻击方法
    public virtual void Attack(){}
    // 死亡方法
    public virtual void OnDeath(){}
    // 受到攻击
    public virtual void OnAttack(){}

    // 生命值
    public int HP { get; set; }
    // 移动速度
    public float MoveSpeed { get; set; }
    // 敌人类型
    public EnemyType EnemyType { get; set; }
    // 等级
    public int Lv{ get; set; }
}
