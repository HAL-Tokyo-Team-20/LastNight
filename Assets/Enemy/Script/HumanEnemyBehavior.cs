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

        vfxProperties = GetComponent<VFXExposedProperty>();

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
            Debug.Log("Hit");
            vfx.Play();
        }
        else
        {
            Debug.Log("Hit End");
            vfx.Stop();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            //base.BeAttack();

            vfx.gameObject.transform.position = other.gameObject.transform.position +  new Vector3(this.gameObject.transform.localScale.x/5, 0.0f, 0.0f);
            
            

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
