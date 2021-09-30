using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{

    [Range(0.1f, 10.0f)]
    public float MoveSpeed = 1.0f;

    public bool LockMove = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move 
        Move();

    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (!(moveDirection.x == 0 && moveDirection.y == 0))
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (!LockMove)
        {
            SetFlip(moveDirection.x);
            transform.position += moveDirection * MoveSpeed * Time.deltaTime;
        }

    }

    private void SetFlip(float x)
    {
        if (x > 0) spriteRenderer.flipX = false;
        else if (x < 0) spriteRenderer.flipX = true;
        else return;
    }
}
