using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour, INPCBehaviour
{
    // ID
    public int npcId;
    // 이름
    public string npcName;

    // 퀘스트ID
    public int questID;

    // 출력문ID
    public int printID;

    private void Start()
    {
        if (QuestManager.Instance.idToQuest[questID].IsCompleted == true)
        {
            printID = QuestManager.Instance.idToQuest[questID].LastPrintID;
        }
    }

    // 매니져에서 사용할 초기화 함수
    public void Init(NPC_TABLEData data_)
    {
        npcId = data_.ID;
        npcName = data_.NPC_NAME;
        questID = data_.NPC_INITIAL_QUEST;
        printID = QuestManager.Instance.idToQuest[questID].BeforeID;
    }

    public virtual void PopUpDialog()
    {
        if (printID >= 302000 && printID < 303000)
        {
            NPCManager.Instance.ActivateChoices(printID);
        }
        else if (printID >= 304000 && printID < 305000)
        {
            NPCManager.Instance.ActivateMain(printID);
        }
        else
        {
            // 대사창 내림
            NPCManager.Instance.PopDown();

            // 타입별로 다르게 진행
            NPCManager.Instance.SetIDByQuestType();
        }
    }

    public virtual void SetPrintID()
    {
        // 완료하지 않은 퀘스트라면
        if (QuestManager.Instance.idToQuest[questID].IsAccepted == true &&
            QuestManager.Instance.idToQuest[questID].IsCompleted == false)
        {
            if (QuestManager.Instance.idToQuest[questID].IngID == 0)
            { /* Do Nothing */ }
            else
            // 진행중 출력문
            { printID = QuestManager.Instance.idToQuest[questID].IngID; }
        }
        else if (QuestManager.Instance.idToQuest[questID].IsNoBusiness == true)
        {
            printID = QuestManager.Instance.idToQuest[questID].LastPrintID;
        }
        // 완료한 퀘스트라면
        else if (QuestManager.Instance.idToQuest[questID].IsCompleted == true)
        {
            // 완료 출력문
            printID = QuestManager.Instance.idToQuest[questID].CompleteID;
            // 변화없음
        }
    }
}
