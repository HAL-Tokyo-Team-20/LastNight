using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{

    [Range(0.1f, 10.0f)]
    public float MoveSpeed = 1.0f;

    public bool LockMove = false;

    public bool SelectedMode { get; set; }// 是否在物体选取模式中
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SelectedMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Move 
        Move();

        // 选取模式
        Select();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (!(moveDirection.x == 0 && moveDirection.y == 0))
        {
            animator.SetBool("Running", true);
            SoundManager.Instance.Play("FootStep_00");
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

    private void Select()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SelectedMode = !SelectedMode;
        }
        if (SelectedMode)
        {
            // 选取模式下
            if (Input.GetKeyDown(KeyCode.K))
            {
                SelectItemMgr.Instance.SelectNextItem();
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                SelectItemMgr.Instance.SelectPreItem();
            }
            else
            {
                SelectItemMgr.Instance.SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SelectItemMgr.Instance.Confirm();
            }
        }
    }
}
