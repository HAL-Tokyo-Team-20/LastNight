using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Gun : Prosthetic
{
    private GameObject bullet_object;
    private float coolTime = 0.5f;
    private SimplePlayerController playerController;
    private PlayerAttackBehavior playerAttackBehavior;

    static private float coolFinTime = 0f;

    public Gun()
    {
        this.Type = ProstheticType.Gun;
        playerController = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<SimplePlayerController>();
        playerAttackBehavior = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<PlayerAttackBehavior>();

        // 获取预制体
        var handle = Addressables.LoadAssetAsync<GameObject>("Player_Bullet");
        bullet_object = handle.WaitForCompletion();
    }

    public override void SkillActive(Vector3 offset)
    {
        if (playerController.FaceRight)
        {
            bullet_object.GetComponent<Bullet>().Angle = 0.0f;
            GameObject.Instantiate(bullet_object, offset, Quaternion.Euler(0, 0, 90));
        }
        else
        {
            bullet_object.GetComponent<Bullet>().Angle = 180.0f;
            GameObject.Instantiate(bullet_object, offset, Quaternion.Euler(0, 0, 90));
        }

        // 设置冷却时间
        CoroutineHandler.Instance.StartMyCoroutine(MyTimer.Wait(() =>
            {
                playerAttackBehavior.GunCanAttack = true;
            }, coolTime));

        coolFinTime = Time.realtimeSinceStartup + coolTime;
    }

    public override float GetCoolTime()
    {
        float now = Time.realtimeSinceStartup;
        float result = (coolFinTime - now) / coolTime;
        return result > 0.0f ? result : 0.0f;
    }
}