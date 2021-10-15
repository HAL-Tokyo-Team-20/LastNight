using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundData
{
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public float playedTime;
    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
}

public class SoundManager : UnitySingleton<SoundManager>
{

    [Range(5,25)]
    public int MaxSoundTrack = 10;

    [SerializeField]
    private List<SoundData> soundDatas;

    private AudioSource[] audioSourceList;

    private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    private void Awake()
    {

        audioSourceList = new AudioSource[MaxSoundTrack];

        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        foreach (var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }
    }

    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            if (!audioSourceList[i].isPlaying) return audioSourceList[i];
        }

        return null;
    }

    public void Play(AudioClip clip,float volume)
    {
        var audioSource = GetUnusedAudioSource();
        if (audioSource == null) return; 
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void Play(string name)
    {
        if (soundDictionary.TryGetValue(name, out var soundData))
        {
            if (Time.realtimeSinceStartup - soundData.playedTime < soundData.clip.length) return;
            soundData.playedTime = Time.realtimeSinceStartup;
            Play(soundData.clip,soundData.volume);
        }
    }

    public void Play(string name, float interval_time)
    {
        if (soundDictionary.TryGetValue(name, out var soundData))
        {
            if (Time.realtimeSinceStartup - soundData.playedTime < interval_time) return;
            soundData.playedTime = Time.realtimeSinceStartup;
            Play(soundData.clip,soundData.volume);
        }
    }
}
