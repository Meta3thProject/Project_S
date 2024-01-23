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
    }

    public override void PopUpDialog()
    {
        base.PopUpDialog();

        if (printID == QuestManager.Instance.idToQuest[questID].LastPrintID)
        {
            BridgeOpen();
        }
    }
}
