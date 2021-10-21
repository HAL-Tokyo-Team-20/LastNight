using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{

    [Range(0.1f, 10.0f)]
    public float MoveSpeed = 1.0f;

    public bool LockMove = false;

    public bool FaceRight = true;

    public bool SelectedMode { get; set; }// 是否在物体选取模式中
    private Animator animator;

    private Rigidbody rb;

    private UIManager uIManager;
    private SoundManager soundManager;
    private DebugManager debugManager;
    private SelectItemMgr selectItemManager;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = UIManager.Instance;
        soundManager = SoundManager.Instance;
        debugManager = DebugManager.Instance;
        selectItemManager = SelectItemMgr.Instance;


        animator = GetComponent<Animator>();
        SelectedMode = false;
        rb = GetComponent<Rigidbody>();

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

        if (moveDirection.x > 0)
        {
            FaceRight = true;
        }
        else if (moveDirection.x < 0.0f)
        {
            FaceRight = false;
        }

        if (!(moveDirection.x == 0 && moveDirection.y == 0))
        {
            animator.SetBool("Running", true);
            soundManager.Play("FootStep_00",0.4f);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (!LockMove)
        {
            SetFlip(moveDirection.x);
            rb.MovePosition(transform.position + (moveDirection * MoveSpeed * Time.deltaTime));
        }

    }

    private void SetFlip(float x)
    {
        if (x > 0 && transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (x < 0 && transform.rotation != Quaternion.Euler(0, 180, 0))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else return;
    }

    private void Select()
    {

        debugManager.UpdateData("Select Mode", SelectedMode.ToString());

        if (Input.GetKeyDown(KeyCode.F))
        {
            SelectedMode = !SelectedMode;
            if (selectItemManager.GetSelecteditemsLength() > 0) uIManager.SetSelectImageActive(SelectedMode);
            
        }
        if (SelectedMode)
        {
            selectItemManager.Select();
            // 选取模式下
            if (Input.GetKeyDown(KeyCode.K))
            {
                selectItemManager.SelectNextItem();
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                selectItemManager.SelectPreItem();
            }
            else
            {
                selectItemManager.SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                selectItemManager.Confirm();
                SelectedMode = !SelectedMode;
            }
        }
        else
        {
            selectItemManager.CancelAll();
        }
    }
}
