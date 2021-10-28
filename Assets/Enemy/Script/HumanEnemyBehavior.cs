using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class HumanEnemyBehavior : EnemyBehavior
{

    private bool isHit = false;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private VisualEffect enemyHitVFX;

    [SerializeField]
    private List<Material> enemyMaterial = new List<Material>();

    [SerializeField]
    private float dissolveAmount = 0.0f;

    // 敌人子弹
    private int cnt = 0;
    public GameObject EnemyBullet;

    //[SerializeField]
    //private BoxCollider enemyCollider;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        VisualEffect[] tempVFX = GetComponentsInChildren<VisualEffect>();
        enemyHitVFX = tempVFX[0];
        enemyHitVFX.Stop();

        //enemyDeadVFX = tempVFX[1];
        //enemyDeadVFX.Stop();

        animator = GetComponent<Animator>();

        SpriteRenderer[] childObject = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < childObject.Length; i++)
        {
            enemyMaterial.Add(childObject[i].material);

            enemyMaterial[i].SetFloat("_DissolveAmount", 0.0f);
            enemyMaterial[i].SetFloat("_ElectricityAmount", 0.0f);
        }

        dissolveAmount = 0;

        //enemyCollider = GetComponent<BoxCollider>();
        //enemyCollider.enabled = true;
        isHit = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Dead();
        }

        if (isHit)
        {
            StartCoroutine(BloodSpread(0.05f));
        }
        else if (!isHit && animator.GetBool("BeAttack"))
        {
            enemyHitVFX.Stop();
            animator.SetBool("BeAttack", false);
        }

        // 暂时测试: 定时发射子弹
        cnt++;
        if (cnt == 60)
        {
            cnt = 0;
            EnemyBullet.GetComponent<EnemyBullet>().Right = false;
            GameObject.Instantiate(EnemyBullet, transform.position + Vector3.up * 0.7f, Quaternion.Euler(0, 0, 90));
        }
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


    private IEnumerator BloodSpread(float duration)
    {


        yield return new WaitForSeconds(duration);

        isHit = false;

    }

    private IEnumerator Dissolve(float duration)
    {
        for (int i = 0; i < enemyMaterial.Count; i++)
        {
            enemyMaterial[i].SetFloat("_ElectricityAmount", 1.0f);
        }

        yield return new WaitForSeconds(duration);

        dissolveAmount += Time.deltaTime;

        for (int i = 0; i < enemyMaterial.Count; i++)
        {
            enemyMaterial[i].SetFloat("_ElectricityAmount", 1.0f);
            enemyMaterial[i].SetFloat("_DissolveAmount", dissolveAmount);

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


    protected override void Dead()
    {
        //enemyCollider.enabled = false;
        animator.SetBool("Dead", true);
        StartCoroutine(Dissolve(2.3f));
        StartCoroutine(DelayDead());
    }

    protected override void BeAttack()
    {

        enemyHitVFX.Play();
        animator.SetBool("BeAttack", true);
        hp--;

    }

}
