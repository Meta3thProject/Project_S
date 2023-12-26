using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBoxEffectSave : MonoBehaviour
{
    // 1번만 캐싱해두면 2,3은 자동으로 캐싱 되도록
    public ParticleSystem breakBoxEffect01 {  get; private set; }
    public ParticleSystem breakBoxEffect02 {  get; private set; }
    public ParticleSystem breakBoxEffect03 {  get; private set; }
    public ParticleSystem breakBoxEffect04 {  get; private set; }

    private void Awake()
    {
        // 이펙트 캐싱
        breakBoxEffect01 = transform.GetChild(0).GetComponent<ParticleSystem>();

        breakBoxEffect02 = breakBoxEffect01;
        breakBoxEffect03 = breakBoxEffect01;
        breakBoxEffect04 = breakBoxEffect01;
    }

    // 원하는 위치에서 박스 파괴 이펙트 생성
    public void BreakBoxEffectPlay(Vector3 _position)
    {
        if (breakBoxEffect01.isPlaying)
        {
            breakBoxEffect02.transform.position = _position;
            breakBoxEffect02.Play();
        }

        else if (breakBoxEffect01.isPlaying == false)
        {
            breakBoxEffect01.transform.position = _position;
            breakBoxEffect01.Play();
        }

        else if (breakBoxEffect02.isPlaying)
        {
            breakBoxEffect03.transform.position = _position;
            breakBoxEffect03.Play();
        }

        else
        {
            breakBoxEffect04.transform.position = _position;
            breakBoxEffect04.Play();
        }
    }
}
