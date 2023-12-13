using System;

// 유형
public enum QuestType
{
    Collection = 11,    // 수집
    Produce = 12,       // 제작
    Delivery1 = 21,     // 배달1
    Delivery2 = 22,     // 배달2
    Episode = 30,       // 대화
    Exploration = 40,   // 탐사
    Destroy = 50,       // 파괴
    Instruction = 60,   // 지시
}

[Serializable]
public class Quest
{
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

    public Quest(QUEST_TABLEData data_)
    {
        title = data_.QUEST_TITLE;
        goal = data_.QUEST_GOAL;
        type = (QuestType)data_.QUEST_TYPE;
        nextId = data_.CHAIN_QUEST;
    }

    // 시작 조건
    public virtual void StartQuest()
    {
        switch (type)
        {
            case QuestType.Collection:
                break;
            case QuestType.Produce:
                break;
            case QuestType.Delivery1:
                break;
            case QuestType.Delivery2:
                break;
            case QuestType.Episode:
                break;
            case QuestType.Exploration:
                break;
            case QuestType.Destroy:
                break;
            case QuestType.Instruction:
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
