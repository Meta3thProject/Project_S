using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goddess : NPCBase
{
    public override void PopUpDialog()
    {
        base.PopUpDialog();
        if (printID == 304624)
        {
            Invoke("AfterTalkStartEnding", 1.0f);
        }
    }

    public void AfterTalkStartEnding()
    {
        NPCManager.Instance.PopDown();
        EndingAfterTalk.endingAfterTalk.StartEndingtalk();
    }
}