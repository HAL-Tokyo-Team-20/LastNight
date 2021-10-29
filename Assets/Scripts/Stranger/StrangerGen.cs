using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerGen : MonoBehaviour
{
    [Tooltip("路人预制体")]
    public GameObject StrangerObj;

    [Tooltip("最短生成时间(s)")]
    public float MinGenTime;

    [Tooltip("最长生成时间(s)")]
    public float MaxGenTime;

    [Tooltip("Z方向的偏移量1")]
    [Range(-1.0f, 1.0f)]
    public float OffsetZ1;

    [Tooltip("Z方向的偏移量2")]
    [Range(-1.0f, 1.0f)]
    public float OffsetZ2;

    private float timer;
    private float genTime;
    private bool isPlayerEnter;
    private Vector3 genPosition;
    void Start()
    {
        isPlayerEnter = false;
        timer = 0;
        genTime = Random.Range(MinGenTime, MaxGenTime);
        genPosition = transform.GetChild(0).transform.position;
    }


    void Update()
    {
        // 玩家进入, 则直接return
        if (isPlayerEnter)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= genTime)
        {
            timer = 0;
            genTime = Random.Range(MinGenTime, MaxGenTime);

            // 生成2个路人, 1个向左1个向右
            Vector3 pos = genPosition + new Vector3(0, 0, OffsetZ1);
            var obj1 = GameObject.Instantiate(StrangerObj, pos, new Quaternion(0, 0, 0, 0));
            obj1.GetComponent<StrangerBehavior>().MoveDir = MoveDirEnum.Left;

            pos = genPosition + new Vector3(0, 0, OffsetZ2);
            var obj2 = GameObject.Instantiate(StrangerObj, pos, new Quaternion(0, 0, 0, 0));
            obj2.GetComponent<StrangerBehavior>().MoveDir = MoveDirEnum.Right;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerEnter = false;
        }
    }
}
