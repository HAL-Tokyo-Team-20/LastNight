using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 物品基类
public class IItem : MonoBehaviour
{
    // 物品名字
    public string Name {get; set; }
    // 物品Id
    public int Id {get; set; }
    // 物品描述
    public string Description {get; set; }
    // 等级
    public int Lv{ get; set; }
}
