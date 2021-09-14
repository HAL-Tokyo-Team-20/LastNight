using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehavior : MonoBehaviour
{

    private GameObject bullet_object;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bullet_object = Resources.Load("Bullet") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bullet_object.GetComponent<Bullet>().Right = !spriteRenderer.flipX;
            Instantiate(bullet_object, transform.position + new Vector3(0,0.5f,0), Quaternion.Euler(0,0,90));
        }        
    }
}
