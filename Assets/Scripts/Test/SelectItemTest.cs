using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemTest : MonoBehaviour, ISelectItem
{

    Vector3 playerPos;
    void Start()
    {
        playerPos = GameObjectMgr.Instance.GetGameObject("Player").transform.position;
    }

    // 继承 ISelectItem 接口的类, 必须实现以下两个东西
    public bool Selected { get; set; }
    public bool ConfirmSelected { get; set; }
    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, playerPos);
    }



    void Update()
    {
        if (Selected)
        {
            // 选取状态代码, 比如高亮显示
        }
        if (ConfirmSelected)
        {
            // 实现具体行为, 比如被拉向玩家


            this.ConfirmSelected = false;
        }
    }



}
