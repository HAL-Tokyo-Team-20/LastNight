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

        // TODO: 霰弹枪子弹的预制体
        var handle = Addressables.LoadAssetAsync<GameObject>("Player_Bullet");
        bullet_object = handle.WaitForCompletion();
    }
    public override void SkillActive(Vector3 offset)
    {
        //霰弹枪攻击行为
        if (playerController.FaceRight)
        {
            for (int i = 0; i < 5; i++)
            {
                bullet_object.GetComponent<Bullet>().Angle = 15.0f * i - 30.0f;
                GameObject.Instantiate(bullet_object, offset, Quaternion.Euler(0, 0, 90));
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                bullet_object.GetComponent<Bullet>().Angle = 15.0f * i + 150.0f;
                GameObject.Instantiate(bullet_object, offset, Quaternion.Euler(0, 0, 90));
            }
        }
    }
}
