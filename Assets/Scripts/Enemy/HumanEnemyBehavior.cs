using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HumanEnemyBehavior : EnemyBehavior
{

    private bool isHit = false;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    public VisualEffect enemyHitVFX;

    [SerializeField]
    public List<Material> enemyMaterial = new List<Material>();

    [SerializeField]
    private float dissolveAmount = 0.0f;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        VisualEffect[] tempVFX = GetComponentsInChildren<VisualEffect>();
        enemyHitVFX = tempVFX[0];
        enemyHitVFX.Stop();


        animator = GetComponent<Animator>();

        SpriteRenderer[] childObject = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < childObject.Length; i++)
        {
            enemyMaterial.Add(childObject[i].material);

            enemyMaterial[i].SetFloat("_DissolveAmount", 0.0f);
            enemyMaterial[i].SetFloat("_ElectricityAmount", 0.0f);
        }


        dissolveAmount = 0;

        isHit = false;

    }

    public void Idle()
    {
        animator.SetBool("Idle", true);
    }

    // 流血动画
    public void BloodSpread()
    {
        if (isHit)
        {
            StartCoroutine(DelayBloodSpread(0.05f));
        }
        else if (!isHit && !animator.GetBool("BeAttack"))
        {
            enemyHitVFX.Stop();
        }
    }

    // 死亡动画
    public override void Dead()
    {
        animator.SetTrigger("Dead");
        enemyHitVFX.Play();
        StartCoroutine(StopEffect());
    }

    // 走路动画
    public void Walk()
    {

    }

    // 攻击动画
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public override void BeAttack()
    {
        animator.SetTrigger("BeAttack");
        enemyHitVFX.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            EnemyHit(other.gameObject.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            isHit = false;
        }
    }

    private IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(0.35f);
        enemyHitVFX.Stop();
    }

    //
    private void EnemyHit(Vector3 otherPosition)
    {
        var dir = (otherPosition - this.gameObject.transform.position).normalized;
        Vector3 bloodVelocity = enemyHitVFX.GetVector3("BloodVelocity");

        if (dir.x < 0.0f)
        {
            if (bloodVelocity.x < 0.0f)
            {
                bloodVelocity.x *= -1;
            }

            enemyHitVFX.SetVector3("BloodVelocity", new Vector3(bloodVelocity.x, bloodVelocity.y, bloodVelocity.z));
            enemyHitVFX.gameObject.transform.position = otherPosition + new Vector3(this.gameObject.transform.localScale.x / 6, 0.0f, 0.0f);
        }
        else if (dir.x > 0.0f && bloodVelocity.x > 0.0f)
        {
            enemyHitVFX.SetVector3("BloodVelocity", new Vector3(-bloodVelocity.x, bloodVelocity.y, bloodVelocity.z));
            enemyHitVFX.gameObject.transform.position = otherPosition + new Vector3(this.gameObject.transform.localScale.x / 6 * -1, 0.0f, 0.0f);
        }

        enemyHitVFX.SetVector3("GroundVector", new Vector3(0.0f, -1.7f, 0.0f));
        isHit = true;

    }

    //VFX Effects handling
    private IEnumerator DelayBloodSpread(float duration)
    {
        yield return new WaitForSeconds(duration);
        isHit = false;
    }

    private IEnumerator Dissolve(float duration = 0.0f)
    {
        foreach (var material in enemyMaterial)
        {
            material.SetFloat("_ElectricityAmount", 1.0f);
        }

        yield return new WaitForSeconds(duration);

        dissolveAmount += Time.deltaTime;

        foreach (var material in enemyMaterial)
        {
            material.SetFloat("_ElectricityAmount", 1.0f);
            material.SetFloat("_DissolveAmount", dissolveAmount);

            if (dissolveAmount >= 1.0f)
            {
                dissolveAmount = 1.0f;
            }
        }
    }

    private IEnumerator DelayDead()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        Destroy(gameObject);
    }
}
