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

public abstract class Quest
{
    // 이름
    public string title;
    // 유형
    public QuestType type;

    // 시작 조건
    public abstract void StartQuest();

    // 완료 조건
    public abstract void ClearQuest();
}
