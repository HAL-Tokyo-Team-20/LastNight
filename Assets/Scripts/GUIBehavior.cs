using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GUIBehavior : MonoBehaviour
{

    public KeyCode Active_Key = KeyCode.Z;
    public float Slipout_duration = 0.5f;
    public List<Image> Menu_ImageList;

    private bool slipouted = false;
    private Vector2 canvans_WH = Vector2.zero;

    private Tweener slip_tweener;

    private void Awake()
    {
        canvans_WH = GetComponent<CanvasScaler>().referenceResolution;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Active_Key))
        {
            UIImgae_Slip(canvans_WH.x,Menu_ImageList,Slipout_duration);
        }
    }

    protected void UIImgae_Slip(float destnation_point ,List<Image> image_list,float duration)
    {
        var slip = slipouted ? slipouted = false : slipouted = true;

        float dest = 0.0f;
        if (slip)
        {
            dest = destnation_point - (image_list[0].rectTransform.sizeDelta.x / 2);
        }
        else if (!slip)
        {
            dest = destnation_point + (image_list[0].rectTransform.sizeDelta.x / 2);
        }

        foreach (Image img in image_list)
        {
            slip_tweener = img.rectTransform.DOMoveX(dest, duration).SetEase(Ease.Linear);
        }
    }
}
