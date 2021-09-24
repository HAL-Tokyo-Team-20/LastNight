using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{

    protected void WindowPop(RectTransform rect,Vector2 size,float duration)
    {
        // Open
        if (!rect.gameObject.activeInHierarchy)
        {
            rect.gameObject.SetActive(true);
            rect.DOSizeDelta(size, duration)
                .SetEase(Ease.Linear);
        }

        // Close
        else if (rect.gameObject.activeInHierarchy)
        {
            rect.DOSizeDelta(new Vector2(0, 0), duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => rect.gameObject.SetActive(false));
        }
    }


}
