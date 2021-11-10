using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerBehavior : MonoBehaviour
{
    public MoveDirEnum MoveDir { get; set; }

    public float MoveSpeed = 3.0f;

    private Rigidbody rb;
    private float Player_Z;

    private List<SpriteRenderer> sp_list;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player_Z = GameObjectMgr.Instance.GetGameObject("Player").transform.position.z;

        if (MoveDir == MoveDirEnum.Right) transform.rotation = Quaternion.Euler(0, 180, 0);

        MoveSpeed = Random.Range(1.0f, 2.0f);

        // Get All SpriteRenderer
        sp_list = new List<SpriteRenderer>();
        transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>(false,sp_list);

        SetSortingLayer();

        // TODO:随机生成服饰等

    }

    void Update()
    {
        if (MoveDir == MoveDirEnum.Left)
        {
            rb.MovePosition(transform.position + (Vector3.left * MoveSpeed * Time.deltaTime));
        }
        else if (MoveDir == MoveDirEnum.Right)
        {
            rb.MovePosition(transform.position + (Vector3.right * MoveSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void SetSortingLayer()
    {
        string layername = "Back";

        var result = Player_Z > transform.position.z? layername = "Front" : layername = "Back";

        foreach(SpriteRenderer sp in sp_list)
        {
            sp.sortingLayerName = layername;
        }
    }
}
