using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Detect : NPCState
{
    // 플레이어의 특정 위치 도달을 감지한 상태
    // 플레이어에게 다가간다

    public override void OnStateEnter()
    {
        // 플레이어 서치? or 플레이어 위치 가져오기
        throw new System.NotImplementedException();
    }

    public override void OnStateExecute()
    {
        // 플레이어에게 접근, 특정 거리에서 정지
        throw new System.NotImplementedException();
    }

    public override void OnStateExit()
    {
        // 아무것도 안함
        throw new System.NotImplementedException();
    }
}
