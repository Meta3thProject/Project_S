using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : NPCBase, IDeliveryAndChoice
{
    public override void PopUpDialog()
    {
        base.PopUpDialog();
    }

    public void CheckDelivered(bool first_, bool second_)
    {
        if (first_ == true && second_ == false)
        {
            NPCManager.Instance.DisableChoice2();
        }
        else if (first_ == false && second_ == true)
        {
            NPCManager.Instance.DisableChoice1();
        }
        // 애초에 있을 수 없지만 예외처리
        else if (first_ == false && second_ == false)
        {
            NPCManager.Instance.DisableChoice1();
            NPCManager.Instance.DisableChoice2();
        }
        else // (first_ == true && second_ == true)
        { /* Do Nothing */ }
    }
}
