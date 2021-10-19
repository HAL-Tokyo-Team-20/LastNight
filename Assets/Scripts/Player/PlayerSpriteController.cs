using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;
using UnityEngine;


public class PlayerSpriteController : UnitySingleton<PlayerSpriteController>
{

    public SpriteResolver LeftHandResolver;
    public SpriteResolver RightHandResolver;

    public string GetLeftHandLabel()
    {
        return LeftHandResolver.GetLabel();
    }

    public void SetLeftHandLabel(string label)
    {
        LeftHandResolver.SetCategoryAndLabel("Lefthand_Down", label);
    }

    public string GetRightHandLabel()
    {
        return RightHandResolver.GetLabel();
    }

    public void SetRightHandLabel(string label)
    {
        RightHandResolver.SetCategoryAndLabel("Righthand_Down", label);
    }
}
