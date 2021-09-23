using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCarGen : MonoBehaviour
{
    [Tooltip("SkyCar预制体")]
    public GameObject SkyCar;
    [Tooltip("生成的时间")]
    public float GenTime;

    private Vector3 StartPosition;
    private float timer;

    void Start()
    {
        timer = 0f;
        StartPosition = transform.parent.Find("StartPos").transform.position;
    }


    void Update()
    {
        timer += 1f;
        if (timer >= GenTime)
        {
            timer = 0f;
            GameObject.Instantiate(SkyCar, StartPosition, new Quaternion());
        }
    }
}
