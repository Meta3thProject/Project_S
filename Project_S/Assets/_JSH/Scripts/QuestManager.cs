using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;

    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("QuestManager 인스턴스가 없다리");
                instance = new QuestManager();
            }
            return instance;
        }
    }

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
        instance = this;
        Debug.Log(instance.name);

        main.SetActive(false);
        twoChoices.SetActive(false);
        threeChoices.SetActive(false);
    }

    public void PopUp(Vector3 dir_)
    {
        switch (currentQuest.type)
        {
            case QuestType.Tutorial:
                break;
            default:
                break;
        }
        windowCanvas.GetComponent<RectTransform>().position = (dir_ * 2) + player.transform.position;
        windowCanvas.GetComponent<RectTransform>().forward = dir_;
    }
}
