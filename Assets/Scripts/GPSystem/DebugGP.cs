using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGP : MonoBehaviour
{
    float radis;
    private Transform player_center;
    void Start()
    {
        GameObject player = GameObjectMgr.Instance.GetGameObject("Player");
        player_center = player.transform.Find("center_point");

        radis = SelectItemMgr.Instance.DistanceToSelect;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0, 0, 0.5f);
        Gizmos.DrawSphere(player_center.position, 30);
    }
}
