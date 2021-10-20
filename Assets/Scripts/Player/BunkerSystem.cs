using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using Cinemachine.Extension;

public class BunkerSystem : MonoBehaviour
{

    public KeyCode ActiveKey = KeyCode.E;

    [Range(0.1f, 1.0f)]
    public float EnterTime = 0.5f;
    [HideInInspector]
    public bool in_bunker { get; set; }
    [HideInInspector]
    public bool headout { get; set; }

    private HashSet<GameObject> bunkerSet;

    private Animator animator;
    private SimplePlayerController simplePlayerController;
    private PlayerCameraController playerCameraController;
    private Vector3 EnterPosition;
    private CinemachineVirtualCamera cm_player;
    

    // Start is called before the first frame update
    void Start()
    {

        playerCameraController = PlayerCameraController.Instance;

        bunkerSet = new HashSet<GameObject>();
        animator = GetComponent<Animator>();
        simplePlayerController = GetComponent<SimplePlayerController>();
        cm_player = GameObject.FindGameObjectWithTag("Player_Camera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

        // TODO: Support multi Bunker
        if (Input.GetKeyDown(ActiveKey) && bunkerSet.Count == 1)
        {
            simplePlayerController.LockMove = true;
            GameObject bunkerPoint = null;

            foreach (GameObject g in bunkerSet)
            {
                bunkerPoint = g.transform.gameObject;
            }

            if (!in_bunker)
            {
                in_bunker = true;
                EnterPosition = transform.position;
                transform.DOMove(bunkerPoint.transform.position, EnterTime);
                playerCameraController.Offset(new Vector3(1.2f, 1.15f, -4.35f), 0.75f);
                animator.SetTrigger("BunkerIn");
                animator.SetBool("InBunker", true);
            }
            else if (in_bunker)
            {
                transform.DOMove(EnterPosition, EnterTime).OnComplete(ExitBunker);
                playerCameraController.Offset(new Vector3(0.5f, 1.15f, -5f), 0.75f);
                animator.SetBool("InBunker", false);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bunker"))
        {
            bunkerSet.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bunker"))
        {
            bunkerSet.Remove(other.gameObject);
        }
    }

    private void ExitBunker()
    {
        simplePlayerController.LockMove = false;
        in_bunker = false;
    }
}
