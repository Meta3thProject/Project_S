using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : GSingleton<QuestManager>
{
    // 퀘스트 리스트: <ID, 정보>
    public Dictionary<int, Quest> questList;

    // 플레이어
    public GameObject player;
    // 현재 진행중인 퀘스트
    public Quest currentQuest;
    // 대화창 캔버스
    public Canvas windowCanvas;

    // 대화창
    public GameObject main;
    public TextMeshProUGUI oneOfOne;
    // 2지 선택창
    public GameObject twoChoices;
    public TextMeshProUGUI oneOfTwo;
    public TextMeshProUGUI twoOfTwo;
    // 3지 선택창
    public GameObject threeChoices;
    public TextMeshProUGUI oneOfThree;
    public TextMeshProUGUI twoOfThree;
    public TextMeshProUGUI threeOfThree;

    private void Awake()
    {
        main.SetActive(false);
        twoChoices.SetActive(false);
        threeChoices.SetActive(false);
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
        threeChoices.SetActive(false);

        windowCanvas.GetComponent<RectTransform>().position = Vector3.zero;
    }

    public void ActivateMain(string dialog_)
    {
        main.SetActive(true);
        oneOfOne.text = dialog_;
    }

    public void ActivateTwoChoices(List<string> dialogs_)
    {
        twoChoices.SetActive(true);
        oneOfTwo.text = dialogs_[0];
        twoOfTwo.text = dialogs_[1];
    }

    public void ActivateThreeChoices(List<string> dialogs_)
    {
        threeChoices.SetActive(true);
        oneOfThree.text = dialogs_[0];
        twoOfThree.text = dialogs_[1];
        threeOfThree.text = dialogs_[2];
    }
}
