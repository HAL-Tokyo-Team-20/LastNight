using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
  
    public static void SaveAll(SaveDataTemplate data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData l_data = new SaveData(data);

        formatter.Serialize(stream, l_data);
        stream.Close();
    }

    public static SaveData LoadAll()
    {
        string path = Application.persistentDataPath + "/data.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData l_data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return l_data;
        }
        else
        {
            Debug.Log("File Error");
            return null;
        }
    }
}
