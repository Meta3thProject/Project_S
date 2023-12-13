using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour, INPCBehaviour
{
    // ID
    public int npcId;
    // 이름
    public string npcName;
    // 플레이어 감지 지역: npc에 따라 필요없을 수도? 없앨 수도?
    public GameObject trigger;
    // 키로 사용할 리스트: 대화ID, 선택지ID 들이 들어감
    public List<int> keys;
    // 표시할 대화 키 인덱스
    private int keyIdx = default;
    // 대사: 키, ID: 값
    public List<string> dialogs;

    // 

    private void Awake()
    {
        keys = new List<int>();
        dialogs = new List<string>();

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

        switch (keyIdx)
        {
            // 여기부터 선택지
            case 302000:
                QuestManager.Instance.ActivateMain(dialogs[keys[keyIdx]]);
                break;
            // 여기부터 대사
            case 304000:
                QuestManager.Instance.ActivateChoices(dialogs, keyIdx);
                break;
        }
    }
}
