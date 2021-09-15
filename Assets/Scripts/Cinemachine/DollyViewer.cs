using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Extension;
using DG.Tweening;

public class DollyViewer : MonoBehaviour
{

    private CinemachineTrackedDolly dolly_camera;

    // Start is called before the first frame update
    void Start()
    {
        dolly_camera = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            dolly_camera.DOFloat_DollyPathPosition(3.0f,5.0f).SetEase(Ease.Linear);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            dolly_camera.DOFloat_DollyPathPosition(0.0f, 5.0f).SetEase(Ease.Linear);
        }
    }
}
