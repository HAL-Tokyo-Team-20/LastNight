using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 装备基类, 继承自物品基类
public class IEquipment : IItem
{
    // 生命值
    public int HP{get; set; }
    // 护甲
    public int Armor{get; set; }
}
