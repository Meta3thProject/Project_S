using System.Collections.Generic;
using UnityEngine;

public enum Tutorial
{
    // 301100 ~ 301108 까지 튜토리얼
    Yes = 301100,
    No = 301108
}

public class QuestManager : MonoBehaviour
{
    // 싱글턴
    public static QuestManager Instance;

    // 퀘스트 데이터
    public QUEST_TABLE questTable;

    // 퀘스트 Dictionary
    public Dictionary<int, Quest> idToQuest;

    // 현재 진행중인 퀘스트
    public List<Quest> acceptedQuests;

    private void Awake()
    {
        // { 싱글톤
        if (null == Instance)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        // } 싱글톤

        idToQuest = new Dictionary<int, Quest>();
        acceptedQuests = new List<Quest>();

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
            // 진행중인 퀘스트 리스트에 추가
            acceptedQuests.Add(idToQuest[id_]);
            // 퀘스트를 수락한다
            acceptedQuests[acceptedQuests.IndexOf(idToQuest[id_])].Accept();
        }
        // 진행중인 퀘스트가 있으면 안됨
        else { /* Do Nothing */ }
    }

    // 퀘스트 완료 배달형만 체크
    public void CompleteCheck(int id_)
    {
        int index_ = default;

        // 수락한 퀘스트의 인덱스 찾기
        for (int i = 0; i < acceptedQuests.Count; i++)
        {
            if (acceptedQuests[i] == idToQuest[id_])
            {
                index_ = i; 
                break;
            }
        }

        // 여전히 default일 경우 함수 종료
        if (index_ == default) { return; }

        // ID 체크
        if (NPCManager.Instance.interacted.npcId == acceptedQuests[index_].EndNPC)
        {
            switch (acceptedQuests[index_].Type)
            {
                case QuestType.Delivery1:                                                       // 소지품 생기면 이후 수정
                    if (InventoryFake.Instance.fakeItems[acceptedQuests[index_].Value1] >= acceptedQuests[index_].Value2)
                    {
                        CompleteQuest(id_);
                    }
                    else { /* Do Nothing */ }
                    break;
                case QuestType.Delivery2:
                    if (InventoryFake.Instance.fakeItems[acceptedQuests[index_].Value1] >= 1 ||
                        InventoryFake.Instance.fakeItems[acceptedQuests[index_].Value2] >= 1)
                    {
                        CompleteQuest(id_);
                    }
                    else { /* Do Nothing */ }
                    break;
            }
        }
        else { /* Do Nothing */ }
    }

    // 퀘스트 완료
    public void CompleteQuest(int id_)
    {
        // 완료한 퀘스트는 퀘스트 리스트에 존재하지 않는다
        if (acceptedQuests.IndexOf(idToQuest[id_]) == -1) { return; }
        // 완료한 퀘스트가 아니면 리스트에 존재한다
        else
        {
            acceptedQuests[acceptedQuests.IndexOf(idToQuest[id_])].Complete();
            acceptedQuests.Remove(idToQuest[id_]);
        }
    }
}
