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
            NPCManager.Instance.PopDown();
        }
    }

    public void SetPrintID(int id_)
    {
        // TODO: 퀘스트 수락 여부, 퀘스트 완료 여부를 체크해서 출력문ID 설정

    }
}