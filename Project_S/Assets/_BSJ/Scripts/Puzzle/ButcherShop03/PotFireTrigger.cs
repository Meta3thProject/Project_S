using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotFireTrigger : MonoBehaviour
{
    // 불 이펙트
    private ParticleSystem fireEffect;

    // 트리거 스크립트
    private ButcherShop03Trigger butcherShop03Trigger;

    private void Awake()
    {
        butcherShop03Trigger = transform.parent.parent.GetChild(0).GetComponent<ButcherShop03Trigger>();
        fireEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Torch"))
        {
            fireEffect.Play();
            butcherShop03Trigger.isFire = true;
        }
    }
}
