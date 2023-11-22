using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    // 플레이어
    public GameObject player;
    // 대화창 캔버스
    public Canvas windowCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("퀘스트 매니져가 둘 이상이다리");
        }

        windowCanvas.gameObject.SetActive(false);
    }

    public void PopUp(Vector3 dir_)
    {
        windowCanvas.gameObject.SetActive(true);
        windowCanvas.GetComponent<RectTransform>().position = (dir_ * 2) + player.transform.position;
        windowCanvas.GetComponent<RectTransform>().forward = dir_;
    }
}
