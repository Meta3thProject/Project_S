using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC3 : MonoBehaviour
{
    // { NPC에 따라
    // 플레이어 감지 지역
    public GameObject trigger;
    // 퀘스트
    // 아직 안씀
    // 대사
    [SerializeField] ThreeTable threeChoices;
    public List<string> choiceDialog;
    // } NPC에 따라

    private void Awake()
    {
        choiceDialog = new List<string>();

        for (int i = 0; i < threeChoices.dataArray.Length; i++)
        {
            if (threeChoices.dataArray[i].ID == 0)
            {
                choiceDialog.Add(threeChoices.dataArray[i].Dialog);
            }
        }
    }

    [HideInInspector]
    public float viewAngle = 90f; // 시야각 설정
    [HideInInspector]
    public float viewRadius = 2f; // 시야 반경 설정

    public void DetectPlayer()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, 1 << LayerMask.NameToLayer("Player"));

        if (targetsInViewRadius.Length <= 0)
        {
            QuestManager.Instance.PopDown();
            return;
        }

        Transform target = default;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform targetTemp = targetsInViewRadius[i].transform;

            target = targetTemp;
        }

        Vector3 dirToTarget = (target.position - transform.position).normalized;

        QuestManager.Instance.PopUp(dirToTarget);
        QuestManager.Instance.ActivateThreeChoices(choiceDialog);

        // 선택지에 보상 추가
        QuestManager.Instance.oneOfThree.GetComponent<Choice>().targetIdx = Choice.MBTI.S;
        QuestManager.Instance.oneOfThree.GetComponent<Choice>().mbtiValue = 2;

        QuestManager.Instance.twoOfThree.GetComponent<Choice>().targetIdx = Choice.MBTI.T;
        QuestManager.Instance.twoOfThree.GetComponent<Choice>().mbtiValue = 2;

        QuestManager.Instance.threeOfThree.GetComponent<Choice>().targetIdx = Choice.MBTI.P;
        QuestManager.Instance.threeOfThree.GetComponent<Choice>().mbtiValue = 2;
    }
}
