using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMgr : UnitySingleton<GameObjectMgr>
{
    [SerializeField]
    private Dictionary<string, GameObject> Objects = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Objects["Player"] = GameObject.FindGameObjectWithTag("Player");
        Objects["Player_Camera"] = GameObject.FindGameObjectWithTag("Player_Camera");
    }

    public GameObject GetGameObject(string name)
    {
        GameObject obj;
        var result = Objects.TryGetValue(name, out obj)? obj : null;
        return result;
    }
}