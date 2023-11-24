using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Interact : NPCState
{
    // 플레이어와 만나거나 상호작용하는 상태
    // 대사 출력, 퀘스트 접수

    public override void OnStateEnter()
    {
        // 퀘스트 데이터 로드?
        throw new System.NotImplementedException();
    }

    public override void OnStateExecute()
    {
        // 할당된 퀘스트의 유형에 맞게 UI 활성화 및 대사 출력
        throw new System.NotImplementedException();
    }

    public override void OnStateExit()
    {
        // UI 비활성화
        throw new System.NotImplementedException();
    }
}
