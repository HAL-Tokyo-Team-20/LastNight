using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Extension;
using DG.Tweening;

public class PlayerCameraController : UnitySingleton<PlayerCameraController>
{

    private CinemachineVirtualCamera vc;
    private CinemachineTransposer transposer;

    // Start is called before the first frame update
    void Start()
    {
        vc = GetComponent<CinemachineVirtualCamera>();
        transposer = vc.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void Offset(Vector3 endValue,float duration)
    {
        transposer.DOVector3_FollowOffset(endValue,duration).SetEase(Ease.Linear).SetAutoKill();
    }
}
