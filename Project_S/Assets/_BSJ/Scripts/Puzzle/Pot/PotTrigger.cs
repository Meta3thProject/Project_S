using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotTrigger : MonoBehaviour
{
    // 퍼즐 클리어 스크립트
    PotPuzzleClear potPuzzleClear;
    PotPuzzle potPuzzle;

    private void Awake()
    {
        // 이 퍼즐의 인덱스는 11번입니다.
        potPuzzleClear = transform.root.GetChild(11).GetComponent<PotPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PotPuzzle>() != null)
        {
            potPuzzle = other.GetComponent<PotPuzzle>();

            // 도자기의 타입이 같으면 클리어 체크
            if(potPuzzle.potType == PotType.AnswerPot)
            {
                potPuzzle.EnterTrigger(transform.position);
                potPuzzleClear.CheckClearArray();
            }
        }
    }
}
