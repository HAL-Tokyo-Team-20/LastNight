using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 可能的问题: 1.状态机切换相关

// EnemyFSM: Idle --> SeePlayer --> AttackPlayer --> Dead
public class EnemyFSM : MonoBehaviour
{
    [Tooltip("敌人可以发现玩家的距离")]
    public float SeePlayerDistance;
    [Tooltip("敌人可以攻击玩家的距离")]
    public float AttackPlayerDistance;
    public float MoveSpeed;
    public int HP = 3;
    private EnemyFSMState state;
    private GameObject player;
    private int deathTime;// 死亡动画的总帧数

    private HumanEnemyBehavior humanEnemyBehavior; // 敌人特效脚本

    // 敌人子弹
    private int cnt = 0;
    public GameObject EnemyBullet;

    void Start()
    {
        state = EnemyFSMState.Idle;
        player = GameObjectMgr.Instance.GetGameObject("Player");
        humanEnemyBehavior = GetComponent<HumanEnemyBehavior>();
    }

    void Update()
    {
        humanEnemyBehavior.BloodSpread();
        switch (state)
        {
            case EnemyFSMState.Idle:
                Idle();
                break;
            case EnemyFSMState.SeePlayer:
                SeePlayer();
                break;
            case EnemyFSMState.AttackPlayer:
                AttackPlayer();
                break;
            case EnemyFSMState.Dead:
                Dead();
                break;
        }
    }

    private void Idle()
    {
        // 播放待机动画


        // 检测与玩家的距离
        float dist = Vector3.Distance(transform.position, player.transform.position);
        // 如果距离小于设定的可发现的距离, 则切换到下一个模式
        if (dist <= SeePlayerDistance)
        {
            this.state = EnemyFSMState.SeePlayer;
        }
    }

    private void SeePlayer()
    {
        // 播放移动动画,并向玩家移动

        //TODO 更顺滑的移动
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime, Space.World);
        // 检测与玩家的距离
        float dist = Vector3.Distance(transform.position, player.transform.position);
        // 如果距离小于设定的可攻击距离, 则切换到下一个模式
        if (dist <= AttackPlayerDistance)
        {
            this.state = EnemyFSMState.AttackPlayer;
        }
    }

    private void AttackPlayer()
    {
        // 播放攻击动画,并向玩家攻击
        humanEnemyBehavior.Attack();

        cnt++;
        if (cnt == 360)
        {
            cnt = 0;
            EnemyBullet.GetComponent<EnemyBullet>().Right = false;
            GameObject.Instantiate(EnemyBullet, transform.position + Vector3.up * 0.7f, Quaternion.Euler(0, 0, 90));
        }

        // 如果hp<=0, 则切换到下一个模式
        if (HP <= 0)
        {
            this.state = EnemyFSMState.Dead;
        }
    }

    private void Dead()
    {
        humanEnemyBehavior.Dead();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果受到子弹攻击, hp--
        if (other.CompareTag("Bullet"))
        {
            HP--;
        }
    }
}
