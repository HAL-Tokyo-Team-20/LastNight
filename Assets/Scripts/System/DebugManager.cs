using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

[System.Serializable]
public class DebugData
{
    public string name;
    public string data;

    public DebugData(string name) { this.name = name; }
}

public class DebugManager : UnitySingleton<DebugManager>
{
    [Range(0.1f, 2.0f)]
    public float RefreshTime = 0.5f;

    [SerializeField]
    public List<DebugData> DebugDatas;

    public Dictionary<string, DebugData> DebugDataDictionary = new Dictionary<string, DebugData>();

    private int framecount;
    private float prevtime;

    // Start is called before the first frame update
    void Start()
    {
        framecount = 0;
        prevtime = 0.0f;

        foreach (var data in DebugDatas)
        {
            DebugDataDictionary.Add(data.name, data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Sample();
    }

    // Ìžé
    private void Sample()
    {
        // Fps
        framecount++;
        float time = Time.realtimeSinceStartup - prevtime;

        if (time >= RefreshTime)
        {

            UpdateData("FPS", (framecount / time).ToString(".00"));
            UpdateData("Memory Usage (MB)", ((System.GC.GetTotalMemory(false) + Profiler.usedHeapSizeLong) / 1024f / 1024f).ToString(".00"));

            framecount = 0;
            prevtime = Time.realtimeSinceStartup;
        }


    }

    public void UpdateData(string key,string str)
    {
        if (DebugDataDictionary.ContainsKey(key))
            DebugDataDictionary[key].data = str;

        else if (!DebugDataDictionary.ContainsKey(key))
        {
            var debugdata = new DebugData(key);
            DebugDatas.Add(debugdata);
            DebugDataDictionary.Add(key, debugdata);
        }
    }

    public int GetDebugDatasLength()
    {
        return DebugDataDictionary.Count;
    }

}
