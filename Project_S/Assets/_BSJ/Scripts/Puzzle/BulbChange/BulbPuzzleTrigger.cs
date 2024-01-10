using BNG;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulbPuzzleTrigger : MonoBehaviour
{
    private BulbChangeClear bulbChangeClear;    // 퍼즐 클리어 스크립트

    private BulbPuzzle bulb;            // 트리거에 닿은 전구
    private Rigidbody bulbRigidBody;    // 전구의 리지드바디

    private bool isBulbInTrigger;       // 이미 트리거와 상호작용하고 있는 전구가 있는지 체크

    private void Awake()
    {
        isBulbInTrigger = false;
        bulbChangeClear = transform.root.GetChild(19).GetComponent<BulbChangeClear>();        // 이 퍼즐의 번호는 19번입니다.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBulbInTrigger) { return; }

        if (other.GetComponent<BulbPuzzle>() != null)
        {
            bulb = other.GetComponent<BulbPuzzle>();

            if(bulb.isSpiritHandHit == false)
            {
                isBulbInTrigger = true;
                bulb.EnterBulbTrigger();
                SetBulbGravity(other, useGravity_: false);
                SetBulbPositionAndRotation(other);
            }

            // 정답 전구를 트리거에 넣었다면
            if(bulb.bulbType == BulbType.bulb)
            {
                bulbChangeClear.CheckClear();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (bulb != null)
        {
            bulb.EnterBulbTrigger(isEnterTrigger_ : false);
            SetBulbGravity(other, useGravity_: true);
            isBulbInTrigger = false;
        }
    }

    /// <summary>
    /// Bulb의 중력을 셋팅하는 메서드.
    /// </summary>
    /// <param name="other">부딪힌 콜라이더</param>
    /// <param name="useGravity_">중력 셋팅의 조건체크 여부</param>
    private void SetBulbGravity(Collider other, bool useGravity_)
    {
        if (other.GetComponent<BulbPuzzle>())
        {
            bulbRigidBody = other.GetComponent<Rigidbody>();
            bulbRigidBody.useGravity = useGravity_;
        }

        if(useGravity_ == false)
        {
            bulbRigidBody.velocity = Vector3.zero;
            bulbRigidBody.angularVelocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Bulb의 위치와 회전을 트리거에 맞게 변경하는 매서드.
    /// </summary>
    /// <param name="other">부딪힌 콜라이더</param>
    private void SetBulbPositionAndRotation(Collider other)
    {
        other.gameObject.transform.position = this.transform.position;
        other.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
