using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    // 싱글턴
    public static NPCManager Instance;

    // 플레이어
    public GameObject player;

    // HSJ_ 240115
    private MenuController menuController;

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
        Instance = this;
        // HSJ_ 240115
        menuController = player.GetComponent<MenuController>();


        // BSJ _ 240115
        //// { 싱글톤
        //if (null == Instance)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}
        //// } 싱글톤

        //player = GameObject.FindObjectOfType<CharacterController>().transform.gameObject;

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
        menuController.DisableMove(false);

        windowCanvas.GetComponent<RectTransform>().position = (dir_ * 2) + player.transform.position;
        windowCanvas.GetComponent<RectTransform>().forward = dir_;
    }

    public void PopDown()
    {
        menuController.DisableMove(true);

        main.SetActive(false);
        twoChoices.SetActive(false);

        windowCanvas.GetComponent<RectTransform>().position = Vector3.zero;

        Debug.Log("팝다운");
    }

    public void ActivateMain(int id_)
    {
        main.SetActive(true);
        twoChoices.SetActive(false);

        oneOfOne.text = idToDialogue[id_].dialogue;

        // 다음 출력문ID 설정
        interacted.printID = idToDialogue[interacted.printID].linkDialogue;
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

    public void DisableChoice1()
    {
        twoOfTwo.transform.parent.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    public void DisableChoice2()
    {
        oneOfTwo.transform.parent.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    // 퀘스트 ID에 따라 진행 방법이 달라짐
    public void SetIDByQuestType()
    {
        // 수락 완료 처리
        QuestManager.Instance.AcceptOrComplete(interacted.questID);

        // 다음 출력문ID 설정 함수 콜
        interacted.SetPrintID();
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
