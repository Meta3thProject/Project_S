using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ButcherShop03Trigger : MonoBehaviour
{
    // 불이 붙었는지 안 붙었는지 체크
    [field: SerializeField] public bool isFire {  get; set; }
    
    // 각 고기들이 트리거에 몇개나 들어왔는지 체크
    [SerializeField] private int PorkCount;
    [SerializeField] private int MeatCount;
    [SerializeField] private int ChickenCount;

    // 정답 갯수
    [SerializeField] private int PorkCountAnswer;
    [SerializeField] private int MeatCountAnswer;
    [SerializeField] protected int ChickenCountAnswer;

    // 정답을 체크하는 스크립트
    private ButcherShop03Clear butcherShop03Clear;
    private FindMeat findMeat;

    // 솥에 들어간 고기가 몇개인지 체크하는 Text
    private TMP_Text porkCountText;
    private TMP_Text chickenCountText;
    private TMP_Text meatCountText;

    private void Awake()
    {
        // 고기 정답 캐싱
        PorkCountAnswer = transform.parent.parent.GetChild(0).GetChild(0).childCount;
        MeatCountAnswer = transform.parent.parent.GetChild(0).GetChild(1).childCount;
        ChickenCountAnswer = transform.parent.parent.GetChild(0).GetChild(2).childCount;

        // ButcherShop03Clear 캐싱
        butcherShop03Clear = transform.parent.parent.GetChild(1).GetComponent<ButcherShop03Clear>();

        // Text 캐싱
        porkCountText = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        chickenCountText = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        meatCountText = transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>();
    }

    private void Update()
    {
        // 불이 붙은 상태에서
        if(isFire)
        {
            // 고기가 정답대로 들어오면 클리어
            if (PorkCount == PorkCountAnswer && MeatCount == MeatCountAnswer && ChickenCount == ChickenCountAnswer)
            {
                butcherShop03Clear.CheckClear();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FindMeat>() != null)
        {
            findMeat = other.GetComponent<FindMeat>();

            // { 솥에 들어간 고기의 갯수 증가
            // 돼지고기
            if (findMeat.meatType == IceCubeType.Pork)
            {
                PorkCount++;
                porkCountText.text = $"돼 지 고 기 : {PorkCount}";
            }

            // 닭고기
            else if (findMeat.meatType == IceCubeType.Chicken)
            {
                ChickenCount++;
                chickenCountText.text = $"닭 고 기 : {ChickenCount}";
            }

            // 소고기
            else if (findMeat.meatType == IceCubeType.Meat)
            {
                MeatCount++;
                meatCountText.text = $"소 고 기 : {MeatCount}";
            }

            else { /*Do Nothing*/ }
            // } 솥에 들어간 고기의 갯수 증가
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FindMeat>() != null)
        {
            findMeat = other.GetComponent<FindMeat>();

            // { 솥에 들어간 고기의 갯수 감소
            // 돼지고기
            if (findMeat.meatType == IceCubeType.Pork)
            {
                PorkCount--;
                porkCountText.text = $"돼 지 고 기 : {PorkCount}";
            }

            // 닭고기
            else if (findMeat.meatType == IceCubeType.Chicken)
            {
                ChickenCount--;
                chickenCountText.text = $"닭 고 기 : {ChickenCount}";
            }

            // 소고기
            else if (findMeat.meatType == IceCubeType.Meat)
            {
                MeatCount--;
                meatCountText.text = $"소 고 기 : {MeatCount}";
            }

            else { /*Do Nothing*/ }
            // } 솥에 들어간 고기의 갯수 감소
        }
    }
}
