using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// EnemyFSM: Idle --> SeePlayer --> AttackPlayer --> Dead
public class EnemyFSM : MonoBehaviour
{
    [Tooltip("敌人可以发现玩家的距离")]
    public float SeePlayerDistance;
    [Tooltip("敌人可以攻击玩家的距离")]
    public float AttackPlayerDistance;
    public float MoveSpeed;
    public int HP = 3;
    public float AttackTime = 3f;
    private EnemyFSMState state;
    private GameObject player;
    private HumanEnemyBehavior humanEnemyBehavior; // 敌人特效脚本
    public GameObject EnemyBullet;// 敌人子弹

    private bool CanAttack = true;

    private bool firstTimeAttack = true;

    void Start()
    {
        state = EnemyFSMState.Idle;
        player = GameObjectMgr.Instance.GetGameObject("Player");
        humanEnemyBehavior = GetComponent<HumanEnemyBehavior>();
    }

    void Update()
    {
        switch (state)
        {
            case EnemyFSMState.Idle:
                //TODO: 在特效脚本种完成
                humanEnemyBehavior.BloodSpread();
                Idle();
                break;
            case EnemyFSMState.SeePlayer:
                humanEnemyBehavior.BloodSpread();
                SeePlayer();
                break;
            case EnemyFSMState.AttackPlayer:
                humanEnemyBehavior.BloodSpread();
                AttackPlayer();
                break;
            case EnemyFSMState.Dead:
                break;
        }
    }

    private void Idle()
    {
        // 播放待机动画
        humanEnemyBehavior.Idle();

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
        //TODO: 播放移动动画

        //向玩家移动
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
        // 如果hp<=0, 则切换到下一个模式
        if (HP <= 0)
        {
            this.state = EnemyFSMState.Dead;
        }

        // 仅第一次攻击时生效
        if (firstTimeAttack)
        {
            firstTimeAttack = false;
            // 播放攻击动画,并向玩家攻击
            humanEnemyBehavior.Attack();
            EnemyBullet.GetComponent<EnemyBullet>().Right = false;
            GameObject.Instantiate(EnemyBullet, transform.position + Vector3.up * 0.7f, Quaternion.Euler(0, 0, 90));
            return;
        }

        if (CanAttack)
        {
            CanAttack = false;
            StartCoroutine(MyTimer.Wait(() =>
                {
                    CanAttack = true;

                    if (HP > 0)
                    {
                        // 播放攻击动画,并向玩家攻击
                        humanEnemyBehavior.Attack();
                        EnemyBullet.GetComponent<EnemyBullet>().Right = false;
                        GameObject.Instantiate(EnemyBullet, transform.position + Vector3.up * 0.7f, Quaternion.Euler(0, 0, 90));
                    }

                }, AttackTime));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // 如果受到子弹攻击, hp--
        if (other.CompareTag("Bullet"))
        {
            if (HP > 1)
            {
                HP--;
                humanEnemyBehavior.BeAttack();
                StartCoroutine(HitStop(0.1f,0.075f));
            }
            else if (HP == 1)
            {
                HP--;
                humanEnemyBehavior.Dead();
                state = EnemyFSMState.Dead;
                StartCoroutine(HitStop(0.35f, 0.075f));
            }
        }
    }

    // 打擊感
    private IEnumerator HitStop(float firstwait,float secondwait)
    {
        SoundManager.Instance.Play("Punch_00", 0.3f);
        transform.DOPunchPosition(new Vector3(0.025f,0, 0), 0.35f);
        yield return new WaitForSeconds(firstwait);
        GetComponent<Animator>().speed = 0.0f;
        yield return new WaitForSeconds(secondwait);
        GetComponent<Animator>().speed = 1.0f;
    }
}
