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

    public bool GunCanAttack { get; set; }
    public bool ShotGunCanAttack { get; set; }
    public bool MiniGunCanAttack { get; set; }

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
    private Animator effect_animator;

    private void Awake()
    {
        // LoadAsset
        var handle = Addressables.LoadAssetAsync<GameObject>("Player_Bullet");
        bullet_object = handle.WaitForCompletion();
    }

    // Start is called before the first frame update
    private void Start()
    {
        playerCameraController = PlayerCameraController.Instance;
        debugManager = DebugManager.Instance;
        soundManager = SoundManager.Instance;
        gameObejectManager = GameObjectMgr.Instance;

        bunkerSystem = GetComponent<BunkerSystem>();
        playerController = GetComponent<SimplePlayerController>();
        animator = GetComponent<Animator>();
        player_camera = gameObejectManager.GetGameObject("Player_Camera").GetComponent<CinemachineVirtualCamera>();

        effect_animator = transform.GetChild(3).GetComponent<Animator>();

        prosthetic = new Gun();

        GunCanAttack = true;
        ShotGunCanAttack = true;
        MiniGunCanAttack = true;
    }

    // Update is called once per frame
    private void Update()
    {
        // Change Prosthetic
        if (Input.GetKeyDown(KeyCode.I))//next
        {
            effect_animator.SetTrigger("Effect00");
            switch (prosthetic.Type)
            {
                case ProstheticType.Gun:
                    prosthetic = new ShotGun();
                    break;

                case ProstheticType.MiniGun:
                    prosthetic = new Gun();
                    break;

                case ProstheticType.ShotGun:
                    prosthetic = new MiniGun();
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.U))//prev
        {
            effect_animator.SetTrigger("Effect00");
            switch (prosthetic.Type)
            {
                case ProstheticType.Gun:
                    prosthetic = new MiniGun();
                    break;

                case ProstheticType.MiniGun:
                    prosthetic = new ShotGun();
                    break;

                case ProstheticType.ShotGun:
                    prosthetic = new Gun();
                    break;
            }
        }

        debugManager.UpdateData("Prosthetic Type", prosthetic.Type.ToString());

        // OutSide Bunker
        if (!bunkerSystem.in_bunker)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (prosthetic.Type == ProstheticType.Gun)
                {
                    if (!GunCanAttack) return;
                    GunCanAttack = false;
                }
                else if (prosthetic.Type == ProstheticType.MiniGun)
                {
                    if (!MiniGunCanAttack) return;
                    MiniGunCanAttack = false;
                }
                else if (prosthetic.Type == ProstheticType.ShotGun)
                {
                    if (!ShotGunCanAttack) return;
                    ShotGunCanAttack = false;
                }
                animator.SetTrigger("Attacking");
                prosthetic.SkillActive(transform.position + new Vector3(0f, 0.85f, 0f));
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
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (prosthetic.Type == ProstheticType.Gun)
                    {
                        if (!GunCanAttack) return;
                        GunCanAttack = false;
                    }
                    else if (prosthetic.Type == ProstheticType.MiniGun)
                    {
                        if (!MiniGunCanAttack) return;
                        MiniGunCanAttack = false;
                    }
                    else if (prosthetic.Type == ProstheticType.ShotGun)
                    {
                        if (!ShotGunCanAttack) return;
                        ShotGunCanAttack = false;
                    }
                    animator.SetTrigger("Attacking");
                    prosthetic.SkillActive(transform.position + new Vector3(0f, 0.65f, 0f));
                    soundManager.Play("GunShot_00", 0.1f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            animator.SetTrigger("BeAttack");
        }
    }
}