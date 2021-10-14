using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapPoint : MonoBehaviour, ISelectItem
{
    GameObject player;
    Spring3D spring;
    LineGP line;

    private UIManager uIManager;

    void Start()
    {
        player = GameObjectMgr.Instance.GetGameObject("Player");
        SelectItemMgr.Instance.AddToList(this);

        spring = GetComponent<Spring3D>();
        line = GetComponent<LineGP>();
        spring.enabled = false;
        line.enabled = false;

        uIManager = UIManager.Instance;
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

            uIManager.SetSelectImageActive(true);
            uIManager.MoveSelectImageToTarget(gameObject.transform);
        }
        if (ConfirmSelected)
        {

            this.Selected = false;
            this.ConfirmSelected = false;

            Debug.Log("---------------------------");
            uIManager.SetSelectImageActive(false);
            player.GetComponent<SimplePlayerController>().SelectedMode = false;

            // 实现具体行为
            // 使脚本生效
            spring.enabled = true;
            line.enabled = true;
        }
    }
}
