using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    // 선택지가 보상을 가지고 있는다
    // 보상은 MBTI 스탯
    // 0~7 8개의 스탯 중 하나에 플러스 수치 부여
    // IESNTFJP 순서
    public enum MBTI
    {
        I,
        E,
        S,
        N,
        T,
        F,
        J,
        P
    }

    public MBTI targetIdx = default;
    // 상승 수치
    public int mbtiValue = default;

    public void AcceptReward()
    {
        // 플레이어 스탯 증가
        switch (targetIdx)
        {
            // 아직 플레이어 스탯이 없음
            case MBTI.I:
                break;
            case MBTI.E:
                break;
            case MBTI.S:
                break;
            case MBTI.N:
                break;
            case MBTI.T:
                break;
            case MBTI.F:
                break;
            case MBTI.J:
                break;
            case MBTI.P:
                break;
        }

        // 선택지 창 비활성화
        transform.parent.gameObject.SetActive(false);
        // 뭐가 올랐는지 표시
        QuestManager.Instance.ActivateMain(string.Format("{0} + {1}", targetIdx.ToString(), mbtiValue));
    }
}
