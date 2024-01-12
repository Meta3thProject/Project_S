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
    private Dictionary<string, AudioSource> audioSources = default;
    private AudioSource BGMSource = default;

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

        BGMSource = GFunc.GetRootObj(Define.PLAYER).GetComponentInChildren<AudioSource>();
        //BGMSource.clip = ResourceManager.bgmClips["SE_BGM_Tutorial"];
        //BGMSource.Play();
        PlayBGMClip("SE_BGM_Tutorial");
    }
    // Update is called once per frame
    void Update()
    {
        
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

    public void PlayBGMClip(string clipName_)
    {
        BGMSource.clip = ResourceManager.bgmClips[clipName_];
        BGMSource.Play();
    }
    public void PlayAudioClip(string clipName_, Vector3 position_)
    {
        AudioSource.PlayClipAtPoint(ResourceManager.sfxClips[clipName_], position_);
        
        //    GameObject gameObject = new GameObject("One shot audio");
        //    gameObject.transform.position = position_;
        //    AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        //    audioSource.clip = clip;
        //    audioSource.spatialBlend = 1f;
        //    audioSource.volume = volume;
        //    audioSource.Play();
        //    Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
    }    
}
