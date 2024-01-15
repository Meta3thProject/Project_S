using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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

    private Queue<AudioSource> audioSources = default;
    private AudioSource BGMSource = default;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    private void Init()
    {
        audioSources = new Queue<AudioSource>();
        BGMSource = GFunc.GetRootObj("Player").GetComponentInChildren<AudioSource>();
        PlayBGMClip("SE_BGM_Tutorial");

        SetAudioSources();
    }

    private AudioSource SetAudioSources()
    {
            GameObject tempObj = new GameObject("AudioSource " + audioSources.Count);
            tempObj.transform.SetParent(this.transform);
            AudioSource tempSource = tempObj.AddComponent<AudioSource>();
            audioSources.Enqueue(tempSource);  

            return tempSource;
    }
    
    private AudioSource CheckEmptySource()
    {
        AudioSource targetSource = default;
        foreach(AudioSource source in audioSources)
        {
            if(!source.isPlaying)
            {
                targetSource = source;
                return targetSource;
            }
        }
        targetSource = SetAudioSources();

        return targetSource;
    }

    public void PlayBGMClip(string clipName_)
    {
        BGMSource.clip = ResourceManager.bgmClips[clipName_];
        BGMSource.Play();
    }
    public void PlaySfxClip(string clipName_, Vector3 position_ ,float volume = 0.5f)
    {
        AudioSource source = CheckEmptySource();
        
        source.transform.position = position_;
        AudioClip clip = ResourceManager.sfxClips[clipName_];        
        source.clip = clip;
        source.volume = volume;
        source.Play();        
    }    
}
