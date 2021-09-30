using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class HumanEnemyBehavior : EnemyBehavior
{
    private VisualEffect vfx;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            base.Dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            base.BeAttack();
        }
    }

}
