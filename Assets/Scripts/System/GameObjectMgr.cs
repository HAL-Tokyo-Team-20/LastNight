using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMgr : UnitySingleton<GameObjectMgr>
{
    private Dictionary<string, GameObject> Objects = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Objects["Player"] = GameObject.FindGameObjectWithTag("Player");
    }

    public GameObject GetGameObject(string name)
    {
        GameObject obj;
        if (Objects.TryGetValue(name, out obj))
        {
            return obj;
        }
        else
        {
            return null;
        }
    }
}