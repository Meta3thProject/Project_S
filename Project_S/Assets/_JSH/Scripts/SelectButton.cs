using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    // 선택지를 고르면 
    // 1. MBTI 상승
    // 2. 다음 대사 출력

    // 플레이어 스탯 클래스
    public PlayerStat stat;

    // 상승 MBTI
    public MBTI targetMBTI;
    // 상승량
    public int amount;
    // 다음 출력문ID
    public int nextPrintID;

    // 몇번째 선택지인지
    public bool isFirst;

    public void RewardAndReset()
    {
        stat.AddPoint(targetMBTI.ToString(), amount);
    }

    // 선택 시 실행되는 함수
    public void OnSelect()
    {
        // MBTI 상승
        stat.AddPoint(targetMBTI.ToString(), amount);

        // 다음 출력문ID 설정 후 출력
        NPCManager.Instance.SetIDAfterSelect(nextPrintID);
    }
}
