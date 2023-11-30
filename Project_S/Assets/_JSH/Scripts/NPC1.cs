using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : MonoBehaviour, INPCBehaviour
{
    // { NPC에 따라
    // 플레이어 감지 지역
    public GameObject trigger;
    // 퀘스트
    // 아직 안씀
    // 대사

    public List<string> dialogs;
    // } NPC에 따라

    // 표시할 대화 인덱스
    private int dialogIdx = default;

    private void Awake()
    {
        dialogs = new List<string>();

        //for (int i = 0; i < dialogTable.dataArray.Length; i++)
        //{
        //    dialogs.Add(dialogTable.dataArray[i].Dialog);

        //    //Debug.Log(choiceDialog[i]);
        //}

        dialogIdx = 0;
    }


    public void PopUpDialog()
    {
        QuestManager.Instance.ActivateMain(dialogs[dialogIdx]);

        dialogIdx += 1;
        if (dialogIdx >= dialogs.Count)
        {
            dialogIdx = 0;
        }
    }
}
