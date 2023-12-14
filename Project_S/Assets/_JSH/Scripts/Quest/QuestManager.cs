using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : GSingleton<QuestManager>
{
    // 퀘스트 데이터
    public QUEST_TABLE questTable;

    // 퀘스트 Dictionary
    public Dictionary<int, Quest> idToQuest;

    // 현재 진행중인 퀘스트ID
    public int currQuestID;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        idToQuest = new Dictionary<int, Quest>();

        // 퀘스트 생성
        for (int i = 0; i < questTable.dataArray.Length; i++)
        {
            idToQuest.Add(questTable.dataArray[i].ID, new Quest(questTable.dataArray[i]));
        }
    }

    // 퀘스트 완료 체크

    // 퀘스트 완료

}
