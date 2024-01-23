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
            aftertalkstartending();
        }
    }

    public void aftertalkstartending()
    {
        EndingAfterTalk.endingAfterTalk.StartEndingtalk();
    }
}