using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDataTemplate 
{

    public float test_data = 0.0f;

}

[System.Serializable]
public class SaveData
{

    private float test_data;

    public SaveData(SaveDataTemplate data)
    {
        test_data = data.test_data;
    }
}
