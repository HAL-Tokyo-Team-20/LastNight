using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class HumanEnemyBehavior : EnemyBehavior
{

    private bool isHit = false;

    [SerializeField]
    private VisualEffect vfx;

    [SerializeField]
    private VFXExposedProperty vfxProperties;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        vfx = GetComponentInChildren<VisualEffect>();
        vfx.Stop();
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            base.Dead();
        }

        if (isHit)
        {
            vfx.Play();
        }
        else
        {
            vfx.Stop();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            base.BeAttack();

            RaycastHit hit;
            Physics.SphereCast(other.gameObject.transform.position, 1.0f,
                new Vector3(other.gameObject.transform.position.x, -other.gameObject.transform.position.y, other.gameObject.transform.position.z),
                out hit, 2.0f);

            var dir = (other.gameObject.transform.position - this.gameObject.transform.position).normalized;
            Vector3 bloodVelocity = vfx.GetVector3("BloodVelocity");

            if (dir.x < 0.0f)
            {
                if (bloodVelocity.x < 0.0f)
                {
                    bloodVelocity.x *= -1;
                }

                vfx.SetVector3("BloodVelocity", new Vector3(bloodVelocity.x, bloodVelocity.y, bloodVelocity.z));
                vfx.gameObject.transform.position = other.gameObject.transform.position + new Vector3(this.gameObject.transform.localScale.x / 5, 0.0f, 0.0f);
            }
            else if (dir.x > 0.0f && bloodVelocity.x > 0.0f)
            {
                vfx.SetVector3("BloodVelocity", new Vector3(-bloodVelocity.x, bloodVelocity.y, bloodVelocity.z));
                vfx.gameObject.transform.position = other.gameObject.transform.position + new Vector3(this.gameObject.transform.localScale.x / 5 * -1, 0.0f, 0.0f);
            }

            vfx.SetVector3("GroundVector", new Vector3(0.0f, -hit.transform.position.y*4.5f, 0.0f));

            Debug.Log(hit.transform.position.y);

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

}
