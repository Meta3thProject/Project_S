using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazierTrigger : MonoBehaviour
{
    [Header("횟불 번호 (0, 1, 2, 3)")]
    [SerializeField] int brazierIndex;

    [Header("자동으로 불이 꺼지는 시간")]
    [SerializeField] private float OffFireTime;

    // 불 이펙트
    private ParticleSystem fireEffect;

    // 트리거 스크립트
    private BrazierChecker brazierChecker;

    // 현재 불이 켜져있는지 체크
    private bool isFire;

    private void Awake()
    {
        // 초기값 셋팅
        isFire = false;

        fireEffect = transform.parent.GetChild(1).GetComponent<ParticleSystem>();
        brazierChecker = transform.parent.parent.parent.GetComponent<BrazierChecker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 불 켜기
        if (other.CompareTag("Torch"))
        {
            if (isFire) { return; }

            isFire = true;

            fireEffect.Play();

            brazierChecker.CheckLightFire(brazierIndex, true);
        }

        // 불 끄기
        else if (other.CompareTag("FireOff"))
        {
            isFire = false;

            fireEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            brazierChecker.CheckLightFire(brazierIndex, false);
        }

        else { /*DoNothing*/ }
    }
}
