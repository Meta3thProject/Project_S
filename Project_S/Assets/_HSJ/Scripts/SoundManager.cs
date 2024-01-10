using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if(_instance == null || _instance == default)
            {
                _instance = GFunc.CreateObj<SoundManager>("SoundManager");
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    private Dictionary<string, AudioSource> audioSources = default;

    private readonly int minNum = 5;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        SetAudioSources();
    }

    void SetAudioSources()
    {
        audioSources = new Dictionary<string, AudioSource>();
        for(int i = 0; i < minNum; i++)
        {
            GameObject tempObj = new GameObject("AudioSource " + i);
            tempObj.transform.SetParent(this.transform);
            AudioSource tempSource = tempObj.AddComponent<AudioSource>();
            audioSources.Add(tempObj.name, tempSource);
        }
    }
    // Update is called once per frame
    void Update()
    {
           
    }

    public void ChangeVolume(AudioSource source, float value_)
    {
        source.volume = value_;
    }

    private AudioSource CheckEmptySource()
    {
        AudioSource targetSource = default;
        foreach(AudioSource source in audioSources.Values)
        {
            if(!source.isPlaying)
            {
                targetSource = source;
                return targetSource;
            }
        }
        return targetSource;
    }

    public void PlayAudioClip(string clipName_, Vector3 position_)
    {    
        AudioSource targetSource = CheckEmptySource();
        targetSource.clip = ResourceManager.audioClips[clipName_];
        targetSource.Play();
        targetSource.transform.position = position_;
    }
}
