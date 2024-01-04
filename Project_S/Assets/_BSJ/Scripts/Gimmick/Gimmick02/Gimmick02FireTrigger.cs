using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick02FireTrigger : MonoBehaviour
{
    [Header("기믹 횟불 번호 (0, 1)")]
    [SerializeField] int fireIndex;

    // 불 이펙트
    private ParticleSystem fireEffect;

    // 기믹 체크 스크립트
    private Gimmick02 gimmick02;

    // 현재 불이 켜져있는지 체크
    private bool isFire;

    private void Awake()
    {
        // 초기값 셋팅
        isFire = false;

        fireEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        gimmick02 = transform.parent.parent.GetComponent<Gimmick02>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 불 켜기
        if (other.CompareTag("Torch"))
        {
            if (isFire) { return; }

            isFire = true;

            fireEffect.Play();

            gimmick02.FireOn(fireIndex);
        }
    }
}
