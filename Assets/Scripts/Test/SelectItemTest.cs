using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemTest : MonoBehaviour
{
    List<GameObject> list = new List<GameObject>();
    GameObject player;

    void Start()
    {

    }

    void Add()
    {
        list.Sort(
            (obj1, obj2) =>
            {
                float dist1 = Vector3.Distance(obj1.transform.position, player.transform.position);

                float dist2 = Vector3.Distance(obj2.transform.position, player.transform.position);

                return dist1 <= dist2 ? 1 : -1;
            }
        );
    }
}
