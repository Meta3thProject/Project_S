using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciousMan4 : NPCBase
{
    private void Start()
    {
        if (QuestManager.Instance.idToQuest[questID].IsCompleted == true)
        {
            BridgeOpen();
        }
    }

    public void BridgeOpen()
    {
        // 비활성화
        gameObject.SetActive(false);
        NPCManager.Instance.PopDown();
    }

    public override void PopUpDialog()
    {
        base.PopUpDialog();

        if (printID == QuestManager.Instance.idToQuest[questID].LastPrintID)
        {
            Invoke("BridgeOpen", 1.0f);
        }
    }

    public override void SetPrintID()
    {
        // 완료하지 않은 퀘스트라면
        if (QuestManager.Instance.idToQuest[questID].IsAccepted == true)
        {
            printID = QuestManager.Instance.idToQuest[questID].IngID; 
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
