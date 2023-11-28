using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유형
public enum QuestType
{
    Collect,
    Adventure,
    Delivery,
    Choice,
    Destroy,
    Puzzle,
    Tutorial
}

public class Quest
{
    // 이름
    public string title;
    // 유형
    public QuestType type;

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

    // 완료 조건
    public virtual void ClearQuest()
    {

    }
}
