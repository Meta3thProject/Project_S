using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciousMan3 : NPCBase
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
    }

    public override void PopUpDialog()
    {
        base.PopUpDialog();

        if (QuestManager.Instance.idToQuest[questID].IsCompleted == true)
        {
            BridgeOpen();
        }
    }

    public override void SetPrintID()
    {
        // 완료하지 않았다면
        if (QuestManager.Instance.idToQuest[questID].IsAccepted == true &&
            QuestManager.Instance.idToQuest[questID].IsCompleted == false)
        {
            if (QuestManager.Instance.idToQuest[questID].IngID == 0)
            { /* Do Nothing */ }
            else
            // 진행중 출력문
            { printID = QuestManager.Instance.idToQuest[questID].IngID; }
        }
        // 완료했다면
        else if (QuestManager.Instance.idToQuest[questID].IsCompleted == true)
        {
            printID = QuestManager.Instance.idToQuest[questID].CompleteID;
        }
    }
}
