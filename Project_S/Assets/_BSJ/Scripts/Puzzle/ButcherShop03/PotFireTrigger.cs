using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotFireTrigger : MonoBehaviour
{
    // 불 이펙트
    private ParticleSystem fireEffect;

    // 트리거 스크립트
    private ButcherShop03Trigger butcherShop03Trigger;

    // 불이 꺼지는 시간
    [SerializeField] private float OffFireTime;

    private void Awake()
    {
        butcherShop03Trigger = transform.parent.GetChild(0).GetComponent<ButcherShop03Trigger>();
        fireEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Torch"))
        {
            fireEffect.Play();
            butcherShop03Trigger.isFire = true;

            // 몇 초 뒤에 불이 꺼지게 하기
            StartCoroutine(OffFire(OffFireTime));
        }
    }

    private IEnumerator OffFire(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);

        fireEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        butcherShop03Trigger.isFire = false;
    }
}
