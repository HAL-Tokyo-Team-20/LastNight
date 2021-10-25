using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ShotGun : Prosthetic
{
    public GameObject bullet_object;
    SimplePlayerController playerController;
    public ShotGun()
    {
        this.Type = ProstheticType.ShotGun;
        playerController = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<SimplePlayerController>();

        // 获取预制体
    }
    public override void SkillActive(Vector3 offset)
    {
        //霰弹枪攻击行为
        if (playerController.FaceRight)
        {

        }
        else
        {

        }
    }
}
