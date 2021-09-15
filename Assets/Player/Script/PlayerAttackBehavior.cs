using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Extension;
using DG.Tweening;

public class PlayerAttackBehavior : MonoBehaviour
{
    [Range(0.1f,1.5f)]
    public float headout_time = 0.5f;

    private GameObject bullet_object;
    private SpriteRenderer spriteRenderer;
    private BunkerSystem bunkerSystem;
    private CinemachineVirtualCamera player_camera;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bunkerSystem = GetComponent<BunkerSystem>();
        bullet_object = Resources.Load("Bullet") as GameObject;
        player_camera = GameObject.FindGameObjectWithTag("Player_Camera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

        // 遮蔽体外にいる
        if (!bunkerSystem.in_bunker)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bullet_object.GetComponent<Bullet>().Right = !spriteRenderer.flipX;
                Instantiate(bullet_object, transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 90));
            }
        }

        // 遮蔽体内にいる
        else if (bunkerSystem.in_bunker)
        {
            // 頭を外に
            if (Input.GetKey(KeyCode.R) && !bunkerSystem.headout)
            {
                // TODO: アニメーション追加
                bunkerSystem.headout = true;
                spriteRenderer.flipX = false;
                transform.DOLocalRotate(new Vector3(-25,0,0), headout_time);
                player_camera.GetCinemachineComponent<CinemachineTransposer>().DOVector3_FollowOffset(new Vector3(1.7f, 0.9f, -5f), headout_time).SetEase(Ease.Linear);
            }
            // 頭を隠す
            else if (Input.GetKeyUp(KeyCode.R) && bunkerSystem.headout)
            {
                // TODO: アニメーション追加
                bunkerSystem.headout = false;
                transform.DOLocalRotate(new Vector3(0, 0, 0), headout_time).OnComplete(() => spriteRenderer.flipX = true);
                player_camera.GetCinemachineComponent<CinemachineTransposer>().DOVector3_FollowOffset(new Vector3(1.2f, 0.9f, -5f), headout_time).SetEase(Ease.Linear);
            }
            // 頭を外にいる場合射撃可能
            if (bunkerSystem.headout)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // TODO: アニメーション追加
                    bullet_object.GetComponent<Bullet>().Right = true;
                    Instantiate(bullet_object, transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 90));
                }
            }
        }

       
    }
}
