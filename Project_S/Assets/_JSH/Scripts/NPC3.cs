using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC3 : MonoBehaviour, INPCBehaviour
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

        //for (int i = 0; i < threeChoices.dataArray.Length; i++)
        //{
        //    if (threeChoices.dataArray[i].ID == 0)
        //    {
        //        choiceDialog.Add(threeChoices.dataArray[i].Dialog);
        //    }
        //}
    }

    public void PopUpDialog()
    {
        QuestManager.Instance.ActivateThreeChoices(choiceDialog);

        // 선택지에 보상 추가
        QuestManager.Instance.oneOfThree.GetComponentInParent<Choice>().targetIdx = Choice.MBTI.S;
        QuestManager.Instance.oneOfThree.GetComponentInParent<Choice>().mbtiValue = 2;

        QuestManager.Instance.twoOfThree.GetComponentInParent<Choice>().targetIdx = Choice.MBTI.T;
        QuestManager.Instance.twoOfThree.GetComponentInParent<Choice>().mbtiValue = 2;

        QuestManager.Instance.threeOfThree.GetComponentInParent<Choice>().targetIdx = Choice.MBTI.P;
        QuestManager.Instance.threeOfThree.GetComponentInParent<Choice>().mbtiValue = 2;
    }
}
