using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioClip audioClip = default;
    private AudioSource audioSource = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
           
    }

    public void ChangeVolume(AudioSource source, float value_)
    {
        source.volume = value_;
    }

    public void PlayAudioClip(AudioClip clip, AudioSource source)
    {
        source.PlayOneShot(clip);
    }
}
