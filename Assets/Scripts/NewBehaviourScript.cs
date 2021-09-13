using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        transform.DOMoveX(5.0f, 3.0f);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}