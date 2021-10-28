using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCarGen : MonoBehaviour
{
    [Tooltip("SkyCar预制体")]
    public GameObject SkyCar;
    [Tooltip("生成的时间")]

    public float GenTime;

    public MoveDirEnum MoveDir;

    private Vector3 StartPosition;


    private void Start()
    {
        StartPosition = transform.Find("StartPos").transform.position;

        InvokeRepeating("Gen", GenTime, GenTime);
    }

    private void Gen()
    {
        var car = GameObject.Instantiate(SkyCar, StartPosition, new Quaternion(0, 0, 0, 0));
        car.GetComponent<SkyCar>().MoveDir = MoveDir;
    }
}
