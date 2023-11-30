using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : MonoBehaviour, INPCBehaviour
{
    // { NPC에 따라
    // 플레이어 감지 지역
    public GameObject trigger;
    // 퀘스트
    // 아직 안씀
    // 대사

    public List<string> choiceDialog;
    // } NPC에 따라

    private void Awake()
    {
        choiceDialog = new List<string>();

        //for (int i = 0; i < twoChoices.dataArray.Length; i++)
        //{
        //    if (twoChoices.dataArray[i].ID == 0)
        //    {
        //        choiceDialog.Add(twoChoices.dataArray[i].Dialog);
        //    }
        //}
    }

    public void PopUpDialog()
    {
        QuestManager.Instance.ActivateTwoChoices(choiceDialog);

        // 선택지에 보상 추가
        QuestManager.Instance.oneOfTwo.GetComponentInParent<Choice>().targetIdx = Choice.MBTI.I;      
        QuestManager.Instance.oneOfTwo.GetComponentInParent<Choice>().mbtiValue = 2;
        
        QuestManager.Instance.twoOfTwo.GetComponentInParent<Choice>().targetIdx = Choice.MBTI.E;
        QuestManager.Instance.twoOfTwo.GetComponentInParent<Choice>().mbtiValue = 2;
    }
}
