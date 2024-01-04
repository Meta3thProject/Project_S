using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFunc : MonoBehaviour
{
    public AudioSource playeraudioSource;
    public Slider GlobalSound;

    private void Start()
    {
        // 초기 오디오 볼륨을 원하는 값(여기서는 0.5)으로 설정
        playeraudioSource.volume = 0.5f;
        // 슬라이더의 값도 같이 설정해줍니다.
        GlobalSound.normalizedValue = playeraudioSource.volume;
        // 슬라이더의 값이 변경될 때마다 오디오 볼륨 조절하는 이벤트 핸들러 추가
        GlobalSound.onValueChanged.AddListener(delegate { SoundControl();});
    }

    public void SoundControl()
    {
        // 슬라이더 값으로 오디오 볼륨 조절 (0.0부터 1.0까지의 값)
        playeraudioSource.volume = GlobalSound.normalizedValue;
    }
}