using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Extension;
using DG.Tweening;

public class SwitchArea : MonoBehaviour
{

    [Header("Cinemachine")]
    public CinemachineVirtualCamera dolly_camera;

    [Header("Destination Information")]
    public string destination_name;
    public Vector3 player_destination;
    public BoxCollider camerabox_destination;

    [Header("Etc")]
    [Range(1.0f,20.0f)]
    public float transition_time = 5.0f;
    public bool to_maxpoint = true;

    private GameObject player;
    private CinemachineVirtualCamera player_camera;
    private CinemachineTrackedDolly dolly;
    private UIManager uIManager;

    private void Start()
    {
        player = GameObjectMgr.Instance.GetGameObject("Player"); ;
        player_camera = GameObject.FindGameObjectWithTag("Player_Camera").GetComponent<CinemachineVirtualCamera>();
        dolly = dolly_camera.GetCinemachineComponent<CinemachineTrackedDolly>();
        uIManager = UIManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIManager.ActiveBlackframe(true);
            uIManager.SetHintTextDotween(" 'B' To go " + destination_name,2.0f).SetEase(Ease.Linear);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIManager.ActiveBlackframe(false);
            uIManager.GetHintText().SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("Confirm"))
            {
                uIManager.GetHintText().SetActive(false);
                player.GetComponent<SimplePlayerController>().LockMove = true;
                // Change To DollyCamera
                player_camera.Priority = dolly_camera.Priority - 1;
                StartCoroutine(DollyStart());
            }
        }
    }
    
    IEnumerator DollyStart()
    {
        yield return new WaitForSeconds(2.0f);

        float dolly_destination = dolly.m_Path.MaxPos;
        if (!to_maxpoint) dolly_destination = 0.0f;
        dolly.DOFloat_DollyPathPosition(dolly_destination, transition_time).SetEase(Ease.Linear).OnComplete(()=> {
            if (camerabox_destination) player_camera.GetComponent<CinemachineConfiner>().m_BoundingVolume = camerabox_destination;
            player.transform.position = player_destination;
            dolly_camera.Priority = player_camera.Priority - 1;
            player.GetComponent<SimplePlayerController>().LockMove = false;

        });
    }
}
