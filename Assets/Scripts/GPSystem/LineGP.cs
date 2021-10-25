using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;



public class LineGP : MonoBehaviour
{
    Vector3 end;
    Vector3 current;

    Transform center_pos;

    public float MoveSpeed = 2.0f;

    public bool IsShoot { get; set; }

    bool arrive;

    LineRenderer lineRender;

    Spring3D spring;

    Material line_material = null;

    private void Awake()
    {
        var handle = Addressables.LoadAssetAsync<Material>("Line_Material");
        line_material = handle.WaitForCompletion();
    }

    void Start()
    {
    }

    public void Reset()
    {
        // line
        lineRender = gameObject.AddComponent<LineRenderer>();
        lineRender.positionCount = 2;
        lineRender.startWidth = 0.05f;
        lineRender.endWidth = 0.05f;
        // TODO: set material
        if (line_material) lineRender.material = line_material;
        center_pos = GameObjectMgr.Instance.GetGameObject("Player").transform.Find("center_point");
        current = center_pos.position;
        end = transform.position;
        IsShoot = false;
        arrive = false;

        spring = GetComponent<Spring3D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsShoot)
        {
            return;
        }

        Shoot();

        lineRender.SetPosition(0, center_pos.position);
        lineRender.SetPosition(1, current);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsShoot = false;
            Destroy(lineRender);
        }
    }

    void Shoot()
    {
        if (!arrive)
        {
            current = Vector3.MoveTowards(current, end, MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(current, end) <= 0.2f)
            {
                current = end;
                arrive = true;

                // 设置spring有效
                spring.Reset();
                spring.IsActive = true;

            }
        }
    }
}
