using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemTest : MonoBehaviour, ISelectItem
{

    GameObject player;
    void Start()
    {
        player = GameObjectMgr.Instance.GetGameObject("Player");
        SelectItemMgr.Instance.AddToList(this);
    }

    // 继承 ISelectItem 接口的类, 必须实现以下两个东西
    public bool Selected { get; set; }
    public bool ConfirmSelected { get; set; }
    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }



    void Update()
    {
        if (Selected)
        {
            // 选取状态代码, 比如高亮显示
            Debug.Log(transform.gameObject.name);
        }
        if (ConfirmSelected)
        {
            // 实现具体行为, 比如被拉向玩家
            // transform.localPosition = Vector3.MoveTowards(transform.localPosition, player.transform.position + Vector3.right, Time.deltaTime);
            transform.position = player.transform.position + Vector3.right;


            this.Selected = false;
            this.ConfirmSelected = false;
        }
    }



}
