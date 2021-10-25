using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Gun : Prosthetic
{
    public GameObject bullet_object;
    SimplePlayerController playerController;
    public Gun()
    {
        this.Type = ProstheticType.Gun;
        playerController = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<SimplePlayerController>();

        // 获取预制体
        var handle = Addressables.LoadAssetAsync<GameObject>("Player_Bullet");
        bullet_object = handle.WaitForCompletion();
    }

    public override void SkillActive(Vector3 offset)
    {
        if (playerController.FaceRight)
        {
            bullet_object.GetComponent<Bullet>().Right = true;

            GameObject.Instantiate(bullet_object, offset, Quaternion.Euler(0, 0, 90));
        }
        else
        {
            bullet_object.GetComponent<Bullet>().Right = false;
            GameObject.Instantiate(bullet_object, offset, Quaternion.Euler(0, 0, 90));
        }
    }
}
