using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.VFX;

public class LineGP : MonoBehaviour
{
    Vector3 end;
    Vector3 current;

    Transform center_pos;

    public float MoveSpeed = 2.0f;

    public bool IsShoot { get; set; }

    bool arrive;

    LineRenderer lineRender;

    //Spring3D spring;
    SpringMgr spring;

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
        center_pos = GameObjectMgr.Instance.GetGameObject("Player").transform.Find("grap_point");
        current = center_pos.position;
        end = transform.position;
        IsShoot = false;
        arrive = false;

        //spring = GetComponent<Spring3D>();
        spring = GetComponent<SpringMgr>();
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
            GameObjectMgr.Instance.GetGameObject("Player").GetComponent<Animator>().SetBool("Graped", false);
            IsShoot = false;
            Destroy(lineRender);
            StartCoroutine(ActiveEffect());
        }
    }

    private IEnumerator ActiveEffect()
    {
        yield return new WaitUntil(()=> GameObjectMgr.Instance.GetGameObject("Player").GetComponent<SimplePlayerController>().OnGround);
        GameObjectMgr.Instance.GetGameObject("Player").transform.Find("Player_FallVFX").GetComponent<VisualEffect>().Play();
        StartCoroutine(StopEffect());
        //StartCoroutine(HitStop(0.8f,0.15f));
    }

    private IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(0.25f);
        GameObjectMgr.Instance.GetGameObject("Player").transform.Find("Player_FallVFX").GetComponent<VisualEffect>().Stop();
        GameObjectMgr.Instance.GetGameObject("Player").transform.Find("Player_FallVFX").GetComponent<VisualEffect>().SendEvent("Onstop");
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

    private IEnumerator HitStop(float firstwait, float secondwait)
    {
        yield return new WaitForSeconds(firstwait);
        GameObjectMgr.Instance.GetGameObject("Player").transform.GetComponent<Animator>().speed = 0.0f;
        yield return new WaitForSeconds(secondwait);
        GameObjectMgr.Instance.GetGameObject("Player").transform.GetComponent<Animator>().speed = 1.0f;
    }
}
