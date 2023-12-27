using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPotTrigger : MonoBehaviour
{
    [Header("화분의 index 넘버")]
    [SerializeField] private int potIndexNumber;

    [SerializeField] private HiddenFlowerType triggerFlowerType;

    private HiddenPotChecker hiddenPotChecker;
    private HiddenFlowers hiddenFlowers;

    // 이미 화분에 꽃이 있는지 체크
    private bool isFlowerExist;

    private void Awake()
    {
        isFlowerExist = false;
        hiddenPotChecker = transform.parent.GetComponent<HiddenPotChecker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isFlowerExist) { return; }

        // 꽃이 들어왔다면,
        if (other.GetComponent<HiddenFlowers>() != null)
        {
            isFlowerExist = true;

            // 위치를 고정해줍니다.
            hiddenFlowers = other.GetComponent<HiddenFlowers>();

            hiddenFlowers.transform.position = this.transform.position;
            hiddenFlowers.EnterPotTrigger();

            // 트리거와 같은 타입의 꽃이라면 클리어 배열에 1을 올립니다.
            if (hiddenFlowers.flowerType == triggerFlowerType)
            {
                hiddenPotChecker.CheckClearButton(potIndexNumber, true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 꽃이 나갔다면
        if (other.GetComponent<HiddenFlowers>() != null)
        {
            isFlowerExist = false;

            hiddenFlowers = other.GetComponent<HiddenFlowers>();

            // 트리거와 같은 타입의 꽃이라면 클리어 배열을 0으로 내립니다.
            if (hiddenFlowers.flowerType == triggerFlowerType)
            {
                hiddenPotChecker.CheckClearButton(potIndexNumber, false);
            }
        }
    }
}
