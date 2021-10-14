using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemGP : MonoBehaviour, ISelectItem
{
    GameObject player;
    Transform player_center;
    private UIManager uIManager;

    LineTest line;
    void Start()
    {
        player = GameObjectMgr.Instance.GetGameObject("Player");
        SelectItemMgr.Instance.AddToList(this);
        player_center = player.transform.Find("center_point");
        line = GetComponent<LineTest>();
        uIManager = UIManager.Instance;
    }

    // 继承 ISelectItem 接口的类, 必须实现以下两个东西
    public bool Selected { get; set; }
    public bool ConfirmSelected { get; set; }
    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player_center.transform.position);
    }

    void Update()
    {
        if (Selected)
        {
            // 选取状态代码, 比如高亮显示
            Debug.Log(transform.gameObject.name);
            uIManager.MoveSelectImageToTarget(gameObject.transform);
        }
        if (ConfirmSelected)
        {
            // 1. 从玩家向目标点发射钩索
            // 2. 当钩索到达目标点后, spring 组件开始生效
            // 3. 按下断开键后, spring 组件和 line 组件失效

            line.Reset();
            line.IsShoot = true;



            this.Selected = false;
            this.ConfirmSelected = false;
        }
    }
}
