using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MiniGun : Prosthetic
{
    public GameObject bullet_object;
    SimplePlayerController playerController;

    private const int ShotTimes = 6;
    public MiniGun()
    {
        this.Type = ProstheticType.MiniGun;
        playerController = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<SimplePlayerController>();

        // TODO: 加特林子弹的预制体
        var handle = Addressables.LoadAssetAsync<GameObject>("Player_Bullet");
        bullet_object = handle.WaitForCompletion();
    }
    public override void SkillActive(Vector3 offset)
    {
        //加特林攻击行为
        if (playerController.FaceRight)
        {
            for (int i = 0; i < ShotTimes; i++)
            {
                CoroutineHandler.Instance.StartMyCoroutine(MyTimer.Wait(() =>
                    {
                        float y = Random.Range(-1.0f, 1.0f);
                        bullet_object.GetComponent<Bullet>().Angle = y;
                        GameObject.Instantiate(bullet_object, offset, Quaternion.Euler(0, 0, 90));
                    }, i * 0.1f));

            }
        }
        else
        {
            for (int i = 0; i < ShotTimes; i++)
            {
                CoroutineHandler.Instance.StartMyCoroutine(MyTimer.Wait(() =>
                   {
                       float y = Random.Range(-1.0f, 1.0f);
                       bullet_object.GetComponent<Bullet>().Angle = y + 180.0f;
                       GameObject.Instantiate(bullet_object, offset, Quaternion.Euler(0, 0, 90));
                   }, i * 0.1f));

            }
        }
    }
}
