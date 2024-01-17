using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

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
    }

    private Queue<AudioSource> audioSources = default;
    private AudioSource BGMSource = default;
    private float BGMVolume = 0f;
    private bool isFadeOutEnd = default;
    public enum BGMState
    {
        NONE = -1, TUTORIAL, MAIN, NPC, PUZZLE, ENDING
    }

    public BGMState bgmState = BGMState.NONE
        ;
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
        bgmState = BGMState.TUTORIAL;
        SetAudioSources();
    }

    private void Update()
    {

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

    public void ChangeBGM(BGMState state_)
    {
        if (state_ == bgmState) { return; }

        switch (state_)
        {
            case BGMState.TUTORIAL:
                StopBGMClip();
                PlayBGMClip("SE_BGM_Tutorial");
                break;
            case BGMState.MAIN:
                StopBGMClip();
                PlayBGMClip("SE_BGM_Main_theme");
                break;
            case BGMState.NPC:
                StopBGMClip();
                PlayBGMClip("SE_BGM_NPC_Dialog");
                break;
            case BGMState.PUZZLE:
                StopBGMClip();
                PlayBGMClip("SE_BGM_Puzzle");
                break;
            case BGMState.ENDING:
                StopBGMClip();
                PlayBGMClip("SE_BGM_Ending");
                break;
        }
        bgmState = state_;
    }

    public void PlayBGMClip(string clipName_)
    {
        if (BGMSource.isPlaying == true) { return; }

        BGMSource.clip = ResourceManager.bgmClips[clipName_];
        StartCoroutine(BGMFadeIn());
        BGMSource.Play();
    }

    public void StopBGMClip()
    {
        if (BGMSource.isPlaying == false)  { return; }

        BGMSource.Stop();

    }
    private IEnumerator BGMFadeIn()
    {
        float timer = 0;
        while (timer <= 1)
        {
            timer += Time.deltaTime * 0.5f;
            BGMVolume = Mathf.Lerp(0f, 1f, timer);
            BGMSource.volume = BGMVolume;
            yield return null;
        }
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
