using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour, INPCBehaviour
{
    // { NPC에 따라
    // 플레이어 감지 지역
    public GameObject trigger;
    // 키로 사용할 리스트
    public List<int> keys;
    // 대사: 키, ID: 값
    public Dictionary<int, string> dialogs;
    // } NPC에 따라

    // 표시할 대화 키 인덱스
    private int keyIdx = default;

    private void Awake()
    {
        keys = new List<int>();
        dialogs = new Dictionary<int, string>();

        //for (int i = 0; i < dialogTable.dataArray.Length; i++)
        //{
        //    dialogs.Add(dialogTable.dataArray[i].Dialog);

        //    //Debug.Log(choiceDialog[i]);
        //}

        keyIdx = 0;
    }

    public virtual void PopUpDialog()
    {
        QuestManager.Instance.ActivateMain(dialogs[keys[keyIdx]]);

        if (keyIdx >= keys.Count)
        {
            QuestManager.Instance.PopDown();
        }
    }
}
