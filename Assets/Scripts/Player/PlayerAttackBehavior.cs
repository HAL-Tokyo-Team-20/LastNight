using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cinemachine;
using Cinemachine.Extension;
using DG.Tweening;

public class PlayerAttackBehavior : MonoBehaviour
{
    [Range(0.1f, 1.5f)]
    public float headout_time = 0.5f;

    public Prosthetic prosthetic;

    [SerializeField]
    private GameObject bullet_object;
    private Animator animator;
    private BunkerSystem bunkerSystem;
    private SimplePlayerController playerController;
    private CinemachineVirtualCamera player_camera;

    private PlayerCameraController playerCameraController;
    private DebugManager debugManager;
    private SoundManager soundManager;
    private GameObjectMgr gameObejectManager;


    private void Awake()
    {
        // LoadAsset
        var handle = Addressables.LoadAssetAsync<GameObject>("Player_Bullet");
        bullet_object = handle.WaitForCompletion();


        prosthetic = new Prosthetic();
    }

    // Start is called before the first frame update
    void Start()
    {

        playerCameraController = PlayerCameraController.Instance;
        debugManager = DebugManager.Instance;
        soundManager = SoundManager.Instance;

        bunkerSystem = GetComponent<BunkerSystem>();
        playerController = GetComponent<SimplePlayerController>();
        animator = GetComponent<Animator>();
        player_camera = gameObejectManager.GetGameObject("Player_Camera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

        debugManager.UpdateData("Prosthetic Type",prosthetic.Type.ToString());

        // OutSide Bunker
        if (!bunkerSystem.in_bunker)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Attacking");
                Shooting(new Vector3(0f, 0.85f, 0f));
                soundManager.Play("GunShot_00", 0.1f);
            }
        }
        // InSide Bunker
        else if (bunkerSystem.in_bunker)
        {
            // HeadOut
            if (Input.GetKey(KeyCode.R) && !bunkerSystem.headout)
            {
                // TODO: Add Animation
                bunkerSystem.headout = true;
                {
                    transform.DOLocalRotate(new Vector3(-25, 0, 0), headout_time);
                    playerCameraController.Offset(new Vector3(1.7f, 1.15f, -4.35f), headout_time);
                }
            }
            // HeadIn
            else if (Input.GetKeyUp(KeyCode.R) && bunkerSystem.headout)
            {

                bunkerSystem.headout = false;
                {
                    transform.DOLocalRotate(new Vector3(0, 0, 0), headout_time);
                    playerCameraController.Offset(new Vector3(1.2f, 1.15f, -4.35f), headout_time);
                }
            }

            if (bunkerSystem.headout)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetTrigger("Attacking");
                    Shooting(new Vector3(0f, 0.65f, 0f));
                    soundManager.Play("GunShot_00", 0.1f);
                }
            }
        }

    }



    void Shooting(Vector3 offset)
    {
        if (playerController.FaceRight)
        {
            bullet_object.GetComponent<Bullet>().Right = true;
            Instantiate(bullet_object, transform.position + offset, Quaternion.Euler(0, 0, 90));
        }
        else
        {
            bullet_object.GetComponent<Bullet>().Right = false;
            Instantiate(bullet_object, transform.position + offset, Quaternion.Euler(0, 0, 90));
        }
    }

}
