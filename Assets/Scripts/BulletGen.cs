using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGen : UnitySingleton<BulletGen>
{
    public GameObject BulletObj;

    public void Shoot(Vector3 pos)
    {
        GameObject.Instantiate(BulletObj, pos, new Quaternion());
    }
}