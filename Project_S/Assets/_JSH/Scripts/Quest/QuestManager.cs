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
        int index_ = -1;

        // 수락한 퀘스트의 인덱스 찾기
        for (int i = 0; i < acceptedQuests.Count; i++)
        {
            if (idToQuest[id_] == acceptedQuests[i])
            {
                index_ = i;
                break;
            }
        }

        // 여전히 default일 경우 함수 종료
        if (index_ == -1) { return; }

        // ID 체크
        if (acceptedQuests[index_].EndNPC == NPCManager.Instance.interacted.npcId)
        {
            switch (acceptedQuests[index_].Type)
            {
                case QuestType.Delivery1:
                    if (InventoryFake.Instance.CheckOneValue(acceptedQuests[index_].Value1, acceptedQuests[index_].Value2))
                    {
                        CompleteQuest(id_);
                    }
                    else { /* Do Nothing */ }
                    break;
                case QuestType.Delivery2:
                    if (InventoryFake.Instance.CheckTwoValue(acceptedQuests[index_].Value1, acceptedQuests[index_].Value2))
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
            Debug.Log($"{acceptedQuests[acceptedQuests.IndexOf(idToQuest[id_])].title} 클리어");

            acceptedQuests.Remove(idToQuest[id_]);
        }
    }

    public void AcceptOrComplete(int id_)
    {// 상호작용 중인 NPC가 가진 퀘스트 타입
        switch (idToQuest[id_].Type)
        {
            case QuestType.Delivery1:
            case QuestType.Delivery2:
                // 수락하지 않은 퀘스트이고 완료하지 않은 퀘스트라면
                if (idToQuest[id_].IsAccepted == false &&
                    idToQuest[id_].IsCompleted == false)
                {
                    // 퀘스트 수락
                    AcceptQuest(id_);
                }
                // 수락한 퀘스트라면
                else if (idToQuest[id_].IsAccepted == true)
                {
                    // 퀘스트 완료 체크
                    CompleteCheck(id_);
                }

                break;
            case QuestType.Conversation:
                // 대화형은 멈출 수 없고, 끝까지 대화하면 완료이므로
                // 완료하지 않은 퀘스트라면
                if (idToQuest[id_].IsCompleted == false)
                {
                    // 퀘스트 수락
                    AcceptQuest(id_);
                    // 퀘스트 완료
                    CompleteQuest(id_);
                }
                // 완료한 퀘스트라면 아무것도 하지 않음
                else { /* Do Nothing */ }

                break;
            case QuestType.Puzzle:
                // 수락하지 않은 퀘스트이고 완료하지 않은 퀘스트라면
                if (idToQuest[id_].IsAccepted == false &&
                    idToQuest[id_].IsCompleted == false)
                {
                    // 퀘스트 수락
                    AcceptQuest(id_);
                }

                // 혹시나 모를 예외 처리
                if (NPCManager.Instance.interacted.GetComponent<IPuzzleHolder>() == null || 
                    NPCManager.Instance.interacted.GetComponent<IPuzzleHolder>() == default)
                {
                    Debug.Log($"오브젝트 이름: {NPCManager.Instance.interacted.npcName}");

                    return;
                }

                // 퍼즐을 클리어 했다면
                if (NPCManager.Instance.interacted.GetComponent<IPuzzleHolder>().PuzzleClearCheck() == true)
                {
                    // 퀘스트 완료
                    CompleteQuest(id_);
                }
                // 못했다면
                else { /* Do Nothing */ }

                break;
        }
    }
}
