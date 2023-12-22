using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tailor : NPCBase
{
    public GameObject emptyPlace;
    public PlayerStat playerStat;

    public bool isCompleted;

    private List<string> messages;
    private int printIdx;

    private void Awake()
    {
        messages = new List<string>();

        messages.Add("전시회에 출품할 옷이 필요해!<br>주제는 꽃이야!<br>3개중 2개는 만들었어!");
        messages.Add("정말 멋진 색이야!");

        isCompleted = false;

        printIdx = 0;
    }

    public void CompleteCheck()
    {
        FabricStat stat_ = emptyPlace.GetComponent<FabricCheck>().CheckFabric();

        if (stat_ == null) { return; }
        else if (stat_ != null /*&& isCompleted == false*/)
        {
            isCompleted = true;

            playerStat.AddPoint(stat_.target.ToString(), stat_.amount);
            printIdx += 1;
        }
    }

    public override void PopUpDialog()
    {
        if (isCompleted == false) { CompleteCheck(); }
        NPCManager.Instance.ActivateMain(messages[printIdx]);
    }
}
