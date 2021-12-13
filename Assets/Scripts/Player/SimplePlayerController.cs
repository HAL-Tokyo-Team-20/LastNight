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

    public bool OnGround { get; set; }// 是否在地面上
    private Animator animator;

    private Rigidbody rb;
    [SerializeField] private List<Material> materials;

    private UIManager uIManager;
    private SoundManager soundManager;
    private DebugManager debugManager;
    private SelectItemMgr selectItemManager;

    private Quaternion preQua;

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

        var spriteRenderers = transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in spriteRenderers)
        {
            materials.Add(s.material);
        }

        OnGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Move 
        Move();

        // 选取模式
        Select();

        // 设置normal
        if (preQua != transform.rotation)
        {
            SetNormal();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            OnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            OnGround = false;
        }
    }

    private void SetNormal()
    {
        if (transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            foreach (Material m in materials)
            {
                m.SetFloat("_Normal_Z", 1.0f);
            }
        }
        else if (transform.rotation == Quaternion.Euler(0, 180, 0))
        {
            foreach (Material m in materials)
            {
                m.SetFloat("_Normal_Z", -1.0f);
            }
        }
    }
    private void Move()
    {

        preQua = transform.rotation;

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
            //soundManager.Play("FootStep_00", 0.4f);
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
        // 如果在空中, 直接return, 不做处理
        if (!OnGround) return;

        debugManager.UpdateData("Select Mode", SelectedMode.ToString());
        debugManager.UpdateData("Select Item Length", selectItemManager.GetSelecteditemsLength().ToString());

        selectItemManager.Select();

        //if (Input.GetKeyDown(KeyCode.F) && selectItemManager.GetSelecteditemsLength() > 0)
        if (Input.GetButtonDown("F") && selectItemManager.GetSelecteditemsLength() > 0)
        {
            SelectedMode = !SelectedMode;
            uIManager.SetSelectImageActive(SelectedMode);
        }

        if (SelectedMode)
        {
            
            // 选取模式下
            //if (Input.GetKeyDown(KeyCode.K))
            if (Input.GetButtonDown("Next"))
            {
                selectItemManager.SelectNextItem();
            }
            //else if (Input.GetKeyDown(KeyCode.J))
            else if (Input.GetButtonDown("Pre"))
            {
                selectItemManager.SelectPreItem();
            }
            else
            {
                selectItemManager.SelectItem();
            }

            //if (Input.GetKeyDown(KeyCode.Return))
            if (Input.GetButtonDown("Confirm"))
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
