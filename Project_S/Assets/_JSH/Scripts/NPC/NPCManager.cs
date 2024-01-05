using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    // 싱글턴
    public static NPCManager Instance;

    // 플레이어
    public GameObject player;
    // 현재 상호작용 중인 NPC
    public NPCBase interacted;

    // NPC 데이터
    public NPC_TABLE npcTable;
    // 대사 데이터
    public DIALOGUE_TABLE dialogueTable;
    // 선택지 데이터
    public CHOICE_TABLE choiceTable;

    // 모든 NPC들
    public List<NPCBase> npcs;
    // 대사 Dictionary
    public Dictionary<int, Dialogue> idToDialogue;
    // 선택지 Dictionary
    public Dictionary<int, Choice> idToChoices;

    // 대화창 캔버스
    public Canvas windowCanvas;
    // 대화창
    public GameObject main;
    public TextMeshProUGUI oneOfOne;
    // 2지 선택창
    public GameObject twoChoices;
    public TextMeshProUGUI oneOfTwo;
    public TextMeshProUGUI twoOfTwo;

    private const int NPC_ID_START = 300000;
    private const int CHOICE_ID_START = 302000;
    private const int DIALOGUE_ID_START = 304000;

    private void Awake()
    {
        // { 싱글톤
        if (null == Instance)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        // } 싱글톤

        player = GameObject.FindObjectOfType<CharacterController>().transform.gameObject;

        idToDialogue = new Dictionary<int, Dialogue>();
        idToChoices = new Dictionary<int, Choice>();

        main.SetActive(false);
        twoChoices.SetActive(false);

        // npc데이터 분배
        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].Init(npcTable.dataArray[i]);
        }

        // 대사 데이터 생성
        for (int i = 0; i < dialogueTable.dataArray.Length; i++)
        {
            idToDialogue.Add(dialogueTable.dataArray[i].ID, new Dialogue(dialogueTable.dataArray[i]));
        }

        // 선택지 데이터 생성
        for (int i = 0; i < choiceTable.dataArray.Length; i++)
        {
            idToChoices.Add(choiceTable.dataArray[i].ID, new Choice(choiceTable.dataArray[i]));
        }
    }

    public void PopUp(Vector3 dir_)
    {
        windowCanvas.GetComponent<RectTransform>().position = (dir_ * 2) + player.transform.position;
        windowCanvas.GetComponent<RectTransform>().forward = dir_;
    }

    public void PopDown()
    {
        main.SetActive(false);
        twoChoices.SetActive(false);

        windowCanvas.GetComponent<RectTransform>().position = Vector3.zero;
    }

    public void ActivateMain(int id_)
    {
        main.SetActive(true);
        twoChoices.SetActive(false);

        oneOfOne.text = idToDialogue[id_].dialogue;

        SetIDAfterDialogue();
    }

    /// <summary>
    /// 데이터 테이블이 없는 NPC를 위한 함수
    /// </summary>
    /// <param name="text_">출력할 문자열</param>
    public void ActivateMain(string text_)
    {
        main.SetActive(true);
        twoChoices.SetActive(false);

        oneOfOne.text = text_;
    }

    public void ActivateChoices(int id_)
    {
        twoChoices.SetActive(true);

        oneOfTwo.text = idToChoices[id_].choice1;
        SelectButton button1 = oneOfTwo.transform.parent.GetComponent<SelectButton>();
        button1.targetMBTI = idToChoices[id_].value1;
        button1.amount = idToChoices[id_].value2;
        button1.nextPrintID = idToChoices[id_].linkDlg1;

        twoOfTwo.text = idToChoices[id_].choice2;
        SelectButton button2 = twoOfTwo.transform.parent.GetComponent<SelectButton>();
        button2.targetMBTI = idToChoices[id_].value3;
        button2.amount = idToChoices[id_].value4;
        button2.nextPrintID = idToChoices[id_].linkDlg2;
    }

    // 퀘스트 ID에 따라 진행 방법이 달라짐
    public void SetIDByQuestType()
    {
        // 상호작용 중인 NPC가 가진 퀘스트 타입
        switch (QuestManager.Instance.idToQuest[interacted.questID].Type)
        {
            case QuestType.Delivery1:
            case QuestType.Delivery2:
                // 수락하지 않은 퀘스트이고 완료하지 않은 퀘스트라면
                if (QuestManager.Instance.idToQuest[interacted.questID].IsAccepted == false &&
                    QuestManager.Instance.idToQuest[interacted.questID].IsCompleted == false)
                {
                    // 퀘스트 수락
                    QuestManager.Instance.AcceptQuest(interacted.questID);
                }
                // 수락한 퀘스트라면
                else if (QuestManager.Instance.idToQuest[interacted.questID].IsAccepted == true)
                {
                    // 퀘스트 완료 체크
                    QuestManager.Instance.CompleteCheck(interacted.questID);
                }

                break;
            case QuestType.Conversation:
                // 대화형은 멈출 수 없고, 끝까지 대화하면 완료이므로
                // 완료하지 않은 퀘스트라면
                if (QuestManager.Instance.idToQuest[interacted.questID].IsCompleted == false)
                {
                    // 퀘스트 수락
                    QuestManager.Instance.AcceptQuest(interacted.questID);
                    // 퀘스트 완료
                    QuestManager.Instance.CompleteQuest(interacted.questID);
                }
                // 완료한 퀘스트라면 아무것도 하지 않음
                else { /* Do Nothing */ }

                break;
            case QuestType.Puzzle:
                // 수락하지 않은 퀘스트이고 완료하지 않은 퀘스트라면
                if (QuestManager.Instance.idToQuest[interacted.questID].IsAccepted == false &&
                    QuestManager.Instance.idToQuest[interacted.questID].IsCompleted == false)
                {
                    // 퀘스트 수락
                    QuestManager.Instance.AcceptQuest(interacted.questID);
                }

                // 혹시나 모를 예외 처리
                if(interacted.GetComponent<IPuzzleHolder>() == null || interacted.GetComponent<IPuzzleHolder>() == default)
                {
                    Debug.Log($"오브젝트 이름: {interacted.npcName}");
                
                    return;
                }

                // 퍼즐을 클리어 했다면
                if (interacted.GetComponent<IPuzzleHolder>().PuzzleClearCheck() == true)
                {
                    // 퀘스트 완료
                    QuestManager.Instance.CompleteQuest(interacted.questID);
                }
                // 못했다면
                else { /* Do Nothing */ }

                break;
        }

        // 다음 출력문ID 설정 함수 콜
        interacted.SetPrintID();
    }

    // 대화 진행 함수
    public void SetIDAfterDialogue()
    {
        // 0이 아니면
        if (idToDialogue[interacted.printID].linkDialogue != 0)
        {
            // 다음 출력문ID 설정
            interacted.printID = idToDialogue[interacted.printID].linkDialogue;
        }
        // 0이면
        else
        {
            // 타입별로 다르게 진행
            SetIDByQuestType();

            // 대사창 내림
            PopDown();
        }
    }

    /// <summary>
    /// 선택지 선택 후 대화 진행 함수
    /// </summary>
    /// <param name="id_">선택지의 다음 출력문ID</param>
    public void SetIDAfterSelect(int id_)
    {
        interacted.printID = id_;
    }
}
