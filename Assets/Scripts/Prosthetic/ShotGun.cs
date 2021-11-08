using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ShotGun : Prosthetic
{
    private GameObject bullet_object;
    private float coolTime = 3.0f;
    private SimplePlayerController playerController;
    private PlayerAttackBehavior playerAttackBehavior;

    public ShotGun()
    {
        this.Type = ProstheticType.ShotGun;
        playerController = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<SimplePlayerController>();
        playerAttackBehavior = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<PlayerAttackBehavior>();

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

        // 设置冷却时间
        CoroutineHandler.Instance.StartMyCoroutine(MyTimer.Wait(() =>
            {
                playerAttackBehavior.ShotGunCanAttack = true;
            }, coolTime));
    }
}