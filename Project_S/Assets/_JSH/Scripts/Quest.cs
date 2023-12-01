using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유형
public enum QuestType
{
    Collect = 11,
    Adventure = 12,
    Delivery = 21,
    Choice = 22,
    Destroy = 30,
    Puzzle = 40,
    Tutorial = 60
}

public class Quest
{
    // ID
    public int id;
    // 이름
    public string title;
    // 목표
    public string goal;
    // 유형
    public QuestType type;
    // 시작 대화 ID
    // 보상
    // 다음 퀘스트
    public int nextId;


    // 시작 조건
    public virtual void StartQuest()
    {
        switch (type)
        {
            case QuestType.Collect:
                break;
            case QuestType.Adventure:
                break;
            case QuestType.Delivery:
                break;
            case QuestType.Choice:
                break;
            case QuestType.Destroy:
                break;
            case QuestType.Puzzle:
                break;
            case QuestType.Tutorial:
                break;
        }
    }

    // 완료
    public virtual void ClearQuest()
    {
        // 다음 퀘스트로 
        // 현재 진행중인 퀘스트 변경 (게임 매니져에서 관리할 듯 하다)
        // 퀘스트 정보 창 갱신
        // UI 찾아와야 함
    }
}
