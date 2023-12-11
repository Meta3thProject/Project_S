using System.Collections.Generic;
using UnityEngine;

public class NPCManager : GSingleton<NPCManager>
{
    // NPC 찾아서 대사 할당
    public List<NPCBase> npcs;
    // NPC 데이터
    public NPC_TABLE npcTable;
    // 대사 데이터
    public DIALOGUE_TABLE dialogueTable;
    // 선택지 데이터
    public CHOICE_TABLE choiceTable;
    // 퀘스트 이름, 타입, 목표
    // NPC 정보 오브젝트 생성?

    private const int NPC_ID_START = 300000;
    private const int CHOICE_ID_START = 302000;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < npcs.Count; i++)
        {
            // value로 넣어줄 list
            List<string> tmp_ = new List<string>();

            // ID 할당
            npcs[i].npcId = npcTable.dataArray[i].ID;
            // 이름 할당
            npcs[i].npcName = npcTable.dataArray[i].NAME;

            if (dialogueTable != null)
            {
                for (int j = 0; j < dialogueTable.dataArray.Length; j++)
                {
                    // 순서대로 테이블에 들어가있으므로 ID가 더 크면 더 찾을 필요없다 ( 테이블이 바뀌면 바꿔야한다 )
                    //if (npcs[i].npcId > dialogueTable.dataArray[j].NPC_ID) { break; }
                    // ID가 다르면 바로 다음 탐색
                    //else if (npcs[i].npcId != dialogueTable.dataArray[j].NPC_ID) { continue; }
                    // 같으면
                    if (npcs[i].npcId == dialogueTable.dataArray[j].NPC_ID)
                    {
                        npcs[i].keys.Add(dialogueTable.dataArray[j].ID);
                        npcs[i].dialogs.Add(dialogueTable.dataArray[j].DIALOGUE);
                    }

                    if (dialogueTable.dataArray[j].LINK_DIALOG_CHOICE != 0)
                    {
                        // 선택지ID는 302000부터 시작
                        // 인덱스는 302000을 빼면 나온다
                        int idx_ = dialogueTable.dataArray[j].LINK_DIALOG_CHOICE - CHOICE_ID_START;
                        npcs[i].keys.Add(choiceTable.dataArray[idx_].ID);
                        npcs[i].dialogs.Add(choiceTable.dataArray[idx_].CHOICE1);
                        npcs[i].dialogs.Add(choiceTable.dataArray[idx_].CHOICE2);
                    }

                    foreach (string str_ in npcs[0].dialogs)
                    {
                        Debug.Log(str_);
                    }
                }
            }
        }
    }
}
