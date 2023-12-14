using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCManager : GSingleton<NPCManager>
{
    // 플레이어
    public GameObject player;
    // 모든 NPC들
    public List<NPCBase> npcs;

    // NPC 데이터
    public NPC_TABLE npcTable;
    // 대사 데이터
    public DIALOGUE_TABLE dialogueTable;
    // 선택지 데이터
    public CHOICE_TABLE choiceTable;

    // 대사 Dictionary
    Dictionary<int, Dialogue> idToDialogue;
    // 선택지 Dictionary
    Dictionary<int, Choice> idToChoices;


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
        DontDestroyOnLoad(gameObject);

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

        oneOfOne.text = idToDialogue[id_].dialogue;
    }

    public void ActivateChoices(int id_)
    {
        twoChoices.SetActive(true);

        oneOfTwo.text = idToChoices[id_].choice1;
        twoOfTwo.text = idToChoices[id_].choice2;
    }
}
