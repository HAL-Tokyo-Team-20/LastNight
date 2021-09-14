using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ADScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().material.DOFloat(1.0f, "_Range", 1.0f).SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
