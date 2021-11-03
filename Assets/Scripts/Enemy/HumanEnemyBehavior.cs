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

    // 流血动画
    public void BloodSpread()
    {
        if (isHit)
        {
            StartCoroutine(DelayBloodSpread(0.05f));
        }
        else if (!isHit && animator.GetBool("BeAttack"))
        {
            enemyHitVFX.Stop();
            animator.SetBool("BeAttack", false);
        }
    }

    // 死亡动画
    public override void Dead()
    {
        animator.SetBool("Dead", true);
        StartCoroutine(Dissolve(2.3f));
        StartCoroutine(DelayDead());
    }

    // 走路动画
    public void Walk()
    {
        //animator.SetBool("Attack", false);
        //animator.SetBool("Walk", true);
    }

    // 攻击动画
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    protected override void BeAttack()
    {
        enemyHitVFX.Play();
        animator.SetBool("BeAttack", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            BeAttack();

            RaycastHit hit;
            Physics.SphereCast(other.gameObject.transform.position, 4.2f,
                new Vector3(other.gameObject.transform.position.x, -other.gameObject.transform.position.y, other.gameObject.transform.position.z),
                out hit, 3.0f);

            var dir = (other.gameObject.transform.position - this.gameObject.transform.position).normalized;
            Vector3 bloodVelocity = enemyHitVFX.GetVector3("BloodVelocity");

            if (dir.x < 0.0f)
            {
                if (bloodVelocity.x < 0.0f)
                {
                    bloodVelocity.x *= -1;
                }

                enemyHitVFX.SetVector3("BloodVelocity", new Vector3(bloodVelocity.x, bloodVelocity.y, bloodVelocity.z));
                enemyHitVFX.gameObject.transform.position = other.gameObject.transform.position + new Vector3(this.gameObject.transform.localScale.x/* / 6*/, 0.0f, 0.0f);
            }
            else if (dir.x > 0.0f && bloodVelocity.x > 0.0f)
            {
                enemyHitVFX.SetVector3("BloodVelocity", new Vector3(-bloodVelocity.x, bloodVelocity.y, bloodVelocity.z));
                enemyHitVFX.gameObject.transform.position = other.gameObject.transform.position + new Vector3(this.gameObject.transform.localScale.x/* / 6 */* -1, 0.0f, 0.0f);
            }
            enemyHitVFX.SetVector3("GroundVector", new Vector3(0.0f, /*-*/hit.transform.position.y * 1.5f, 0.0f));
            isHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            isHit = false;
        }
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
        yield return new WaitUntil(() => dissolveAmount >= 1.0f);
        Destroy(gameObject);
    }
}
