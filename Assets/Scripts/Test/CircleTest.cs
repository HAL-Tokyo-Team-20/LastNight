using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTest : MonoBehaviour
{
    public Transform AroundPoint;
    public float AngularSpeed = 5f;
    public float AroundRadius;

    private float angled;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 累加已经转过的角度
        angled += (AngularSpeed * Time.deltaTime) % 360;
        // 计算x位置
        float posX = AroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);
        // 计算z位置
        float posY = AroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);

        // 更新位置
        transform.position = new Vector3(posX, posY, 0) + AroundPoint.position;
    }
}
