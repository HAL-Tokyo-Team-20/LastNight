using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleCameraManager : MonoBehaviour
{

    void Start()
    {
        transform.DOLocalMoveX(4.2f, 25.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetAutoKill();

    }

}
