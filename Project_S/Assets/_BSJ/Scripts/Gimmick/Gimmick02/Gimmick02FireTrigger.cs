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

    // 현재 이 촛대에 불이 켜져있는지 체크
    private bool isFire;

    // 현재 토치에 불이 켜져있는지 체크하는 트리거
    // private SphereCollider touchFireTrigger;

    private void Awake()
    {
        // 초기값 셋팅
        isFire = false;

        fireEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        gimmick02 = transform.parent.parent.GetComponent<Gimmick02>();

        // touchFireTrigger = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFire) { return; }

        // 불 켜기
        if (other.CompareTag("Torch"))
        {
            isFire = true;

            fireEffect.Play();

            gimmick02.FireOn(fireIndex);
        }
    }
}
