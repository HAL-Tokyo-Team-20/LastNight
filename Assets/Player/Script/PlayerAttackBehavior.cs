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

        // �Օ��̊O�ɂ���
        if (!bunkerSystem.in_bunker)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bullet_object.GetComponent<Bullet>().Right = !spriteRenderer.flipX;
                Instantiate(bullet_object, transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 90));
            }
        }

        // �Օ��̓��ɂ���
        else if (bunkerSystem.in_bunker)
        {
            // �����O��
            if (Input.GetKey(KeyCode.R) && !bunkerSystem.headout)
            {
                // TODO: �A�j���[�V�����ǉ�
                bunkerSystem.headout = true;
                spriteRenderer.flipX = false;
                transform.DOLocalRotate(new Vector3(-25,0,0), headout_time);
                player_camera.GetCinemachineComponent<CinemachineTransposer>().DOVector3_FollowOffset(new Vector3(1.7f, 0.9f, -5f), headout_time).SetEase(Ease.Linear);
            }
            // �����B��
            else if (Input.GetKeyUp(KeyCode.R) && bunkerSystem.headout)
            {
                // TODO: �A�j���[�V�����ǉ�
                bunkerSystem.headout = false;
                transform.DOLocalRotate(new Vector3(0, 0, 0), headout_time).OnComplete(() => spriteRenderer.flipX = true);
                player_camera.GetCinemachineComponent<CinemachineTransposer>().DOVector3_FollowOffset(new Vector3(1.2f, 0.9f, -5f), headout_time).SetEase(Ease.Linear);
            }
            // �����O�ɂ���ꍇ�ˌ��\
            if (bunkerSystem.headout)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // TODO: �A�j���[�V�����ǉ�
                    bullet_object.GetComponent<Bullet>().Right = true;
                    Instantiate(bullet_object, transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 90));
                }
            }
        }

       
    }
}
