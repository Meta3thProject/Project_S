using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HiddenFlowerType
{
    WhiteFlower, RedFlower, YellowFlower
}

public class HiddenFlowers : MonoBehaviour
{
    // 꽃의 타입
    [field: SerializeField] public HiddenFlowerType flowerType;

    // 꽃이 쓰러지지 않기 위해 꽃의 시작 회전값 캐싱
    private Rigidbody rb;
    private Quaternion flowerRotate;

    private void Awake()
    {
        // 꽃이 쓰러지지 않게 하기 위한 설정
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        flowerRotate = transform.rotation;
    }

    /// <summary>
    /// 화분 트리거에 닿았을 때 실행하는 메서드.
    /// </summary>
    public void EnterPotTrigger()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        StartCoroutine(ConstraintsControl());
    }

    /// <summary>
    /// 잠시 후에 Constraints를 꺼주는 메서드.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ConstraintsControl()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        transform.rotation = flowerRotate;
    }
}
