using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tailor : NPCBase
{
    public GameObject emptyPlace;
    public PlayerStat playerStat;

    public bool isCompleted;

    private void Awake()
    {
        isCompleted = false;
    }

    public void CompleteCheck()
    {
        FabricStat stat_ = emptyPlace.GetComponent<FabricCheck>().CheckFabric();

        if (stat_ == null) { return; }
        else if (stat_ != null /*&& isCompleted == false*/)
        {
            isCompleted = true;

            playerStat.AddPoint(stat_.target.ToString(), stat_.amount);
        }
    }

    public override void PopUpDialog()
    {
        base.PopUpDialog();
    }
}
