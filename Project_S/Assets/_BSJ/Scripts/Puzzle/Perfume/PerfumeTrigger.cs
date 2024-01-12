using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfumeTrigger : MonoBehaviour
{
    [field: SerializeField] private PerfumeType triggerPerfumeType;      // 이 트리거에 맞는 향수 타입

    private PerfumePuzzleClear perfumePuzzleClear;    // 퍼즐 클리어 스크립트
    private PerfumePuzzle perfumePuzzle;              // 트리거에 닿은 향수 아이템

    private bool isPerfumeInTrigger;                  // 이미 트리거와 상호작용하고 있는 향수 아이템이 있는지 체크
    private Vector3 perfumeItemSortPosition;          // 향수 아이템이 트리거에 닿았을 때 딱 달라붙게 하는 위치

    private void Awake()
    {
        perfumePuzzleClear = transform.root.GetChild(20).GetComponent<PerfumePuzzleClear>();        // 이 퍼즐의 인덱스는 20번입니다.
        perfumeItemSortPosition = transform.GetChild(0).transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPerfumeInTrigger) { return; }

        // 향수퍼즐 컴포넌트가 존재한다면,
        if (other.GetComponent<PerfumePuzzle>() != null)
        {
            isPerfumeInTrigger = true;

            perfumePuzzle = other.GetComponent<PerfumePuzzle>();
            perfumePuzzle.EnterPerfumeTrigger();
            SetPerfumePositionAndRotation(other);

            // 트리거 향수 타입이 향수 아이템 타입과 같다면 클리어 배열 1로 변경
            if (triggerPerfumeType == perfumePuzzle.perfumeType)
            {
                perfumePuzzleClear.IncreaseClearCheck((int)triggerPerfumeType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 향수퍼즐 컴포넌트가 존재한다면,
        if (other.GetComponent<PerfumePuzzle>() != null)
        {
            isPerfumeInTrigger = false;
            perfumePuzzle = other.GetComponent<PerfumePuzzle>();

            // 트리거 향수 타입이 향수 아이템 타입과 같다면 클리어 배열 0으로 변경
            if (perfumePuzzle.perfumeType == triggerPerfumeType)
            {
                perfumePuzzleClear.DecreaseClearCheck((int)triggerPerfumeType);
            }
        }
    }

    /// <summary>
    /// 향수 아이템의 위치와 회전을 트리거에 맞게 변경하는 메서드.
    /// </summary>
    private void SetPerfumePositionAndRotation(Collider other)
    {
        other.gameObject.transform.position = perfumeItemSortPosition;
        other.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
