using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : GSingleton<QuestManager>
{
    // 퀘스트 데이터
    public QUEST_TABLE questTable;
    public enum Tutorial
    {
        // 301100 ~ 301108 까지 튜토리얼
        Yes = 301100,
        No = 301109
    }

    // 퀘스트 Dictionary
    public Dictionary<int, Quest> idToQuest;

    // 현재 진행중인 퀘스트
    public Quest currQuest;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        idToQuest = new Dictionary<int, Quest>();
        currQuest = default;

        // 퀘스트 생성
        for (int i = 0; i < questTable.dataArray.Length; i++)
        {
            idToQuest.Add(questTable.dataArray[i].ID, new Quest(questTable.dataArray[i]));
        }
    }

    // 퀘스트 수락
    public void AcceptQuest(int id_)
    {
        // 이미 받은 퀘스트가 아니고 받으려는 퀘스트가 완료한 퀘스트가 아니면
        if (idToQuest[id_].IsAccepted == false && idToQuest[id_].IsCompleted == false)
        {
            // 퀘스트를 수락한다
            currQuest = idToQuest[id_];
            currQuest.Accept();
        }
        // 진행중인 퀘스트가 있으면 안됨
        else { /* Do Nothing */ }
    }

    // 퀘스트 완료 체크
    public void CompleteCheck()
    {
        // ID 체크
        if (NPCManager.Instance.interacted.npcId == currQuest.EndNPC)
        {
            switch (currQuest.Type)
            {
                case QuestType.Delivery1:                                                       // 소지품 생기면 이후 수정
                    if (InventoryFake.Instance.fakeItems[currQuest.Value1] >= currQuest.Value2)
                    {
                        //CompleteQuest()
                    }
                    else { /* Do Nothing */ }
                    break;
                case QuestType.Delivery2:
                    if (InventoryFake.Instance.fakeItems[currQuest.Value1] >= 1 &&
                        InventoryFake.Instance.fakeItems[currQuest.Value2] >= 1)
                    {
                        //CompleteQuest()
                    }
                    else { /* Do Nothing */ }
                    break;
                case QuestType.Conversation:
                    // 대화를 다 보고
                    // 다시 말을 걸면
                    // 클리어
                    break;
                case QuestType.Puzzle:
                    // 대화을 다 보고
                    // 퍼즐을 푼 뒤에
                    // 말을 걸면
                    // 클리어
                    break;
            }
        }
        else { /* Do Nothing */ }
    }

    // 퀘스트 완료
    public void CompleteQuest(int id_)
    {
        currQuest.Complete();
        currQuest = default;

        NPCManager.Instance.interacted.printID = idToQuest[NPCManager.Instance.interacted.printID].CompleteID;
    }
}
