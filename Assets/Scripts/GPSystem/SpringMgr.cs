using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 使用:
/// 将原来 GPPoint 里的 Spring3D 脚本, 替换为此脚本
/// </summary>
public class SpringMgr : MonoBehaviour
{

    /*
     1. 将玩家拉向发射点一段距离
     2. 然后摆动开始生效
     3. 按下空格, 放开
     */
    public Transform Anchor;         //圆点
    public float Gravity = 9.8f;     //重力加速度
    [Tooltip("玩家拉向GP点的距离比")]
    [Range(0f,1f)]
    public float f1 = 0.6f;
    [Tooltip("玩家拉向GP点的时间")]
    public float f2 = 2f;
    [Tooltip("施加的推力")]
    public float PushPlayerForce = 2f;

    private Vector3 m_rotateAxis;    //旋转轴
    private float w = 0;             //角速度
    GameObject player;
    Transform center_pos;
    SimplePlayerController playerController;

    Vector3 targetPos;
    float preX = 0f;

    public bool IsActive { get; set; }
    bool startPhysics;

    private float player_z;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        player = GameObjectMgr.Instance.GetGameObject("Player");
        playerController = player.GetComponent<SimplePlayerController>();
        //center_pos = player.transform.Find("center_point");
        center_pos = player.transform;
        targetPos = transform.position - f1 * (transform.position - center_pos.position);

        m_rotateAxis = Vector3.Cross(center_pos.position - Anchor.position, Vector3.down);//求出旋转轴
        startPhysics = false;
    }

    private void Update()
    {
        preX = player.transform.position.x;
        if(IsActive)
        {
            IsActive = false;

            player_z = player.transform.position.z;

            // 取消重力
            player.GetComponent<Rigidbody>().useGravity = false;

            player.transform.DOMove(targetPos, f2)
                .OnComplete(() =>
                {
                    startPhysics = true;
                });
        }
        if(startPhysics)
        {
            DoPhysics();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                startPhysics = false;
                IsActive = false;
                //恢复重力
                player.GetComponent<Rigidbody>().useGravity = true;

                //施加一个推力
                if (player.transform.position.x < preX)
                {
                    Rigidbody playerRb = player.GetComponent<Rigidbody>();
                    playerRb.AddForce(Vector3.left * PushPlayerForce, ForceMode.Impulse);
                }
                else
                {
                    Rigidbody playerRb = player.GetComponent<Rigidbody>();
                    playerRb.AddForce(Vector3.right * PushPlayerForce, ForceMode.Impulse);
                }
                player.transform.DOLocalMoveZ(player_z, 0.1f).SetAutoKill();
            }
        }



    }

    void DoPhysics()
    {
        float r = Vector3.Distance(Anchor.position, center_pos.position);
        float l = Vector3.Distance(new Vector3(Anchor.position.x, center_pos.position.y, Anchor.position.z), center_pos.position);
        //当钟摆摆动到另外一侧时，l为负，则角加速度alpha为负。
        Vector3 axis = Vector3.Cross(center_pos.position - Anchor.position, Vector3.down);
        if (Vector3.Dot(axis, m_rotateAxis) < 0)
        {
            l = -l;
        }
        float cosalpha = l / r;
        //求角加速度
        float alpha = (cosalpha * Gravity) / r;
        //累计角速度
        w += alpha * Time.deltaTime;
        //求角位移(乘以180/PI 是为了将弧度转换为角度)
        float thelta = w * Time.deltaTime * 180.0f / Mathf.PI;
        //绕圆点m_ahchor的旋转轴m_rotateAxis旋转thelta角度
        //transform.RotateAround(Anchor.position, m_rotateAxis, thelta);
        player.transform.RotateAround(Anchor.position, m_rotateAxis, thelta);

        // 使玩家一直朝上
        if(playerController.FaceRight)
        {
            player.transform.up = Vector3.up;
            player.transform.right = Vector3.right;
        }
        else
        {
            player.transform.up = Vector3.up;
            player.transform.right = Vector3.left;
        }
        
    }
}
