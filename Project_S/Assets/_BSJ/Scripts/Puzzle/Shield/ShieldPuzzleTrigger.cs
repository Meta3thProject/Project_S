using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPuzzleTrigger : MonoBehaviour
{
    // 방패 타입
    [SerializeField] private ShieldType shieldTriggerType;

    [Header("방패가 트리거에 닿았을 때 회전을 위한 값")]
    [SerializeField] private float rotationValueX;
    [SerializeField] private float rotationValueY;
    [SerializeField] private float rotationValueZ;

    // 트리거에 딱 달라붙게 하기 위한 회전값과 위치값
    private Quaternion _Eulur;
    private Transform target;

    // 방패퍼즐 클리어 스크립트
    private ShieldPuzzle shield;
    private ShieldPuzzleClear shieldPuzzleClear;

    // 방패가 트리거에 붙어 있는지 체크
    private bool isInterative = false;

    private void Awake()
    {
        // 퍼즐의 번호의 인덱스가 6번입니다.
        shieldPuzzleClear = transform.root.GetChild(6).GetComponent<ShieldPuzzleClear>();

        _Eulur = Quaternion.Euler(rotationValueX, rotationValueY, rotationValueZ);
        target = transform.GetChild(0).transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInterative) { return; }

        // 방패퍼즐 컴포넌트가 존재한다면,
        if (other.GetComponent<ShieldPuzzle>() != null)
        {
            isInterative = true;
            shield = other.GetComponent<ShieldPuzzle>();

            // 트리거 방패 타입과 타입이 같다면, 트리거에 딱 달라붙게하고, 클리어 배열 1로 만듦.
            if (shield.shieldType == shieldTriggerType)
            {
                shield.EnterShieldShapeTrigger(_Eulur, target.transform.position);
                shieldPuzzleClear.IncreaseClearCheck((int)shieldTriggerType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 방패퍼즐 컴포넌트가 존재한다면,
        if (other.GetComponent<ShieldPuzzle>() != null)
        {
            isInterative = false;
            shield = other.GetComponent<ShieldPuzzle>();

            shield.ExitShieldShapeTrigger();

            // 트리거 방패 타입과 타입이 같다면, 클리어 배열 0으로 만듦.
            if (shield.shieldType == shieldTriggerType)
            {
                shieldPuzzleClear.DecreaseClearCheck((int)shieldTriggerType);
            }
        }
    }
}
