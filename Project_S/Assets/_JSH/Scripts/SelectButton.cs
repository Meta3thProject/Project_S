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
    public MBTI target;
    // 상승량
    public int amount;

    private void Start()
    {
        stat.AddPoint(target.ToString(), amount);
    }
}
