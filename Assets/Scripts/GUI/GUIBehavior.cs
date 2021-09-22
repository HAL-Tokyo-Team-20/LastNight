using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GUIBehavior : MonoBehaviour
{

    public KeyCode Active_Key = KeyCode.Z;

    [Header("Duration")]
    [Range(0.1f,3.0f)] public float Slipout_duration = 0.5f;
    [Range(0.1f,3.0f)] public float Fade_duration = 0.5f;

    [Header("Object List")]
    public List<Image> Menu_ImageList;

    private bool slipouted = false;
    private Vector2 canvans_WH = Vector2.zero;


    private void Awake()
    {
        canvans_WH = GetComponent<CanvasScaler>().referenceResolution;
    }

    private void Update()
    {
        
    }

    protected void UIImgae_Slip(float destnation_point ,List<Image> image_list,float duration)
    {
        var slip = slipouted ? slipouted = false : slipouted = true;

        float pos_dest = 0.0f;
        float fade_dest = 1.0f;

        if (slip)
        {
            pos_dest = destnation_point - (image_list[0].rectTransform.sizeDelta.x / 2);
            fade_dest = 1.0f;
        }
        else if (!slip)
        {
            pos_dest = destnation_point + (image_list[0].rectTransform.sizeDelta.x / 2);
            fade_dest = 0.0f;
        }

        foreach (Image img in image_list)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(img.rectTransform.DOMoveX(pos_dest, duration).SetEase(Ease.Linear));
            sequence.Join(img.DOFade(fade_dest, Fade_duration));

        }
    }
}
