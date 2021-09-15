using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Extension;
using DG.Tweening;

public class SwitchArea : MonoBehaviour
{

    public CinemachineVirtualCamera dolly_camera;
    public Vector3 player_destination;

    [Range(1.0f,20.0f)]
    public float transition_time = 5.0f;
    public bool to_maxpoint = true;

    private GameObject player;
    private CinemachineVirtualCamera player_camera;
    private CinemachineTrackedDolly dolly;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_camera = GameObject.FindGameObjectWithTag("Player_Camera").GetComponent<CinemachineVirtualCamera>();
        dolly = dolly_camera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
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
            player.transform.position = player_destination;
            dolly_camera.Priority = player_camera.Priority - 1;
            player.GetComponent<SimplePlayerController>().LockMove = false;
        });
    }
}
