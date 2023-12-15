using System;

// 유형
public enum QuestType
{
    Delivery1 = 1,
    Delivery2,
    Conversation,
    Puzzle
}

[Serializable]
public class Quest
{
    // ID는 key

    /// <summary>
    /// 퀘스트 제목
    /// </summary>
    public string title;

    /// <summary>
    /// 퀘스트를 주는 NPC
    /// </summary>
    public int startNPC;
    /// <summary>
    /// 퀘스트 완료 NPC
    /// </summary>
    public int endNPC;

    // { 완료를 위해 체크해야함
    /// <summary>
    /// 완료 체크를 위한 아이템ID 첫번째
    /// </summary>
    public int value1;
    /// <summary>
    /// 완료 체크를 위한 아이템ID 두번째
    /// </summary>
    public int value2;
    // } 완료를 위해 체크해야함

    /// <summary>
    /// 퀘스트의 유형
    /// 1: 배달형1
    /// 2: 배달형2
    /// 3: 대화형
    /// 4: 퍼즐형
    /// </summary>
    public QuestType type;

    /// <summary>
    /// 수락 X 완료 X 출력문 ID
    /// </summary>
    public int beforeID;
    /// <summary>
    /// 수락 O 완료 X 출력문 ID
    /// </summary>
    public int ingID;
    /// <summary>
    /// 수락 O 완료 O 출력문 ID
    /// </summary>
    public int completeID;

    /// <summary>
    /// 퀘스트 완료 보상 아이템 ID
    /// </summary>
    public int rewardID;

    public Quest(QUEST_TABLEData data_)
    {
        title = data_.QUEST_TITLE;

        startNPC = data_.START_NPC;
        endNPC = data_.END_NPC;

        value1 = data_.VALUE1;
        value2 = data_.VALUE2;

        type = (QuestType)data_.QUEST_TYPE;

        beforeID = data_.BEFORE_DIALOGUE;
        ingID = data_.DOING_DIALOGUE;
        completeID = data_.COMPLETE_DIALOGUE;

        rewardID = data_.REWARD;
    }
}
