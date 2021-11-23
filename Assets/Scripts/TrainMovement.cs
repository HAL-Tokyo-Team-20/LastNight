using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainMovement : MonoBehaviour
{

    public float Duration;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool isDone;
    
    void Start()
    {
        isDone = true;
        startPos = transform.Find("StartPos").transform.position;
        endPos = transform.Find("EndPos").transform.position;
    }
    
    void Update()
    {
        if(isDone)
        {
            isDone = false;
            transform.DOMove(endPos, Duration)
                .SetEase(Ease.Linear)
                .SetAutoKill()
                .OnComplete(() =>
                {
                    transform.position = startPos;
                    isDone = true;           
                });         
        }
    }
}
