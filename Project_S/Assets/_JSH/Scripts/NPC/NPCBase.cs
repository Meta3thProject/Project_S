using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour, INPCBehaviour
{
    // ID
    public int npcId;
    // 이름
    public string npcName;

    // 퀘스트ID
    public int questID;

    // 출력문ID
    public int printID;

    // 매니져에서 사용할 초기화 함수
    public void Init(NPC_TABLEData data_)
    {
        npcId = data_.ID;
        npcName = data_.NPC_NAME;
        questID = data_.NPC_INITIAL_QUEST;
        printID = QuestManager.Instance.idToQuest[questID].beforeID;
    }

    public virtual void PopUpDialog()
    {
        switch (printID)
        {
            case 0:
                NPCManager.Instance.PopDown();
                break;
            // 여기부터 선택지
            case 302000:
            case 302501:
            case 302502:
            case 302503:
            case 302504:
            case 302505:
            case 302506:
                NPCManager.Instance.ActivateChoices(printID);
                printID = NPCManager.Instance.dialogueTable.dataArray[printID].LINK_DIALOG;
                break;
            // 여기부터 대사
            case 304000:
            case 304501:
            case 304502:
            case 304503:
            case 304504:
            case 304505:
            case 304506:
            case 304507:
            case 304508:
            case 304509:
            case 304510:
            case 304511:
            case 304512:
            case 304513:
            case 304514:
            case 304515:
            case 304516:
            case 304517:
            case 304518:
            case 304519:
            case 304520:
            case 304521:
            case 304522:
            case 304523:
            case 304524:
            case 304525:
            case 304526:
            case 304527:
            case 304528:
            case 304529:
            case 304530:
            case 304531:
            case 304532:
            case 304533:
            case 304534:
            case 304535:
            case 304536:
            case 304537:
            case 304538:
            case 304539:
            case 304540:
            case 304541:
            case 304542:
            case 304543:
            case 304544:
            case 304545:
            case 304546:
                NPCManager.Instance.ActivateMain(printID);
                break;
        }
    }

    public void SetPrintID(int id_)
    {
        // TODO: 퀘스트 수락 여부, 퀘스트 완료 여부를 체크해서 출력문ID 설정

    }
}
