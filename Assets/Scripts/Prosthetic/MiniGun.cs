using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MiniGun : Prosthetic
{
    public GameObject bullet_object;
    SimplePlayerController playerController;
    public MiniGun()
    {
        this.Type = ProstheticType.MiniGun;
        playerController = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<SimplePlayerController>();

        // 获取预制体

    }
    public override void SkillActive(Vector3 offset)
    {
        //加特林攻击行为
        if (playerController.FaceRight)
        {

        }
        else
        {

        }
    }
}
