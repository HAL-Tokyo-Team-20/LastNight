using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemGP : MonoBehaviour, ISelectItem
{
    private GameObject player;
    private Transform player_center;
    private UIManager uIManager;

    private LineGP line;

    public bool PlayerIsEnter { get; set; }

    private void Start()
    {
        player = GameObjectMgr.Instance.GetGameObject("Player");
        SelectItemMgr.Instance.AddToList(this);
        player_center = player.transform.Find("center_point");
        line = GetComponent<LineGP>();
        uIManager = UIManager.Instance;

        PlayerIsEnter = false;
    }

    // 继承 ISelectItem 接口的类, 必须实现以下两个东西
    public bool Selected { get; set; }

    public bool ConfirmSelected { get; set; }

    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player_center.transform.position);
    }

    private void Update()
    {
        if (Selected)
        {
            // 选取状态代码, 比如高亮显示
            DebugManager.Instance.UpdateData("Selected Item", transform.gameObject.name);

            uIManager.SetSelectImageActive(true);
            uIManager.MoveSelectImageToTarget(gameObject.transform);
        }
        if (ConfirmSelected)
        {
            // 1. 从玩家向目标点发射钩索
            // 2. 当钩索到达目标点后, spring 组件开始生效
            // 3. 按下断开键后, spring 组件和 line 组件失效

            uIManager.SetSelectImageActive(false);
            StartCoroutine(Grap());
        }
    }

    private IEnumerator Grap()
    {
        Animator animator = player.GetComponent<Animator>();
        animator.SetBool("Graped",true);
        animator.SetTrigger("Grap");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.75);

        line.Reset();
        line.IsShoot = true;

        this.Selected = false;
        this.ConfirmSelected = false;

        player.GetComponent<SimplePlayerController>().SelectedMode = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsEnter = false;
        }
    }
}