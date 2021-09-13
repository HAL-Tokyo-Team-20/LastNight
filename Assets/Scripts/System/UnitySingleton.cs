using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    单例模式
    1. 同时间只存在一个对象
    2. 快速获取对象的方法
*/
public class UnitySingleton<T> : MonoBehaviour
    where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    // obj.hideFlags = HideFlags.HideAndDontSave; // 隐藏实例化的new game object
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
}