using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        //if (GameObjectMgr.Instance.GetGameObject("Player") != null)
        //{
        //    print("get player");
        //}
        //else
        //{
        //    print("not found player");
        //}
    }

    // Update is called once per frame
    private void Update()
    {
        float axisVal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(axisVal * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetButtonDown("Fire1"))
        {
            BulletGen.Instance.Shoot(transform.position);
        }
    }
}