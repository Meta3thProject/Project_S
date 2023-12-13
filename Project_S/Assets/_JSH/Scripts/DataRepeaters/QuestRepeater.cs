using System;
using UnityEngine;

[Serializable]
public class QuestRepeater
{
    // ID는 KEY
    public string title;
    public int type;

    // 완료 조건 체크용, NPC가 필요하면 여기부터 들어간다
    public int value1;
    public int value2;
    public int value3;
    // 첫 대사
    public int dialogue;
    // 보상
    public int reward;
    // 선행 퀘스트 ID
    public int lead;
    // 연계 퀘스트 ID
    public int chain;

    #region 프로퍼티
    public string Title
    {
        get { return title; }
        private set { title = value; }
    }

    public int Type
    {
        get { return type; }
        private set { type = value; }
    }

    public int Value1
    {
        get { return value1; }
        private set { value1 = value; }
    }

    public int Value2
    {
        get { return value2; }
        private set { value2 = value; }
    }

    public int Value3
    {
        get { return value3; }
        private set { value3 = value; }
    }

    public int Dialogue
    {
        get { return dialogue; }
        private set { dialogue = value; }
    }

    public int Reward
    {
        get { return reward; }
        private set { reward = value; }
    }

    public int Lead
    {
        get { return lead; }
        private set { lead = value; }
    }

    public int Chain
    {
        get { return chain; }
        private set { chain = value; }
    }
    #endregion 프로퍼티

    public QuestRepeater(QUEST_TABLEData data_)
    {
        Title = data_.QUEST_TITLE;
        Type = data_.QUEST_TYPE;
        value1 = data_.VALUE1;
        value2 = data_.VALUE2;
        value3 = data_.VALUE3;
        dialogue = data_.DIALOGUE;
        reward = data_.REWARD;
        lead = data_.LEAD_QUEST;
        chain = data_.CHAIN_QUEST;
    }
}