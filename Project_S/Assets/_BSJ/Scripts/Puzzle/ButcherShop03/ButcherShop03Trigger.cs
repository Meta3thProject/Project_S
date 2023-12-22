using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Awake()
    {
        // 고기 정답 캐싱
        PorkCountAnswer = transform.parent.GetChild(0).GetChild(0).childCount;
        MeatCountAnswer = transform.parent.GetChild(0).GetChild(1).childCount;
        ChickenCountAnswer = transform.parent.GetChild(0).GetChild(2).childCount;

        // ButcherShop03Clear 캐싱
        butcherShop03Clear = transform.parent.parent.GetChild(1).GetComponent<ButcherShop03Clear>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<FindMeat>() != null)
        {
            findMeat = collision.collider.GetComponent<FindMeat>();

            // 소고기
            if (findMeat.meatType == IceCubeType.Meat)
            {
                MeatCount++;
            }

            // 돼지고기
            else if (findMeat.meatType == IceCubeType.Pork)
            {
                PorkCount++;
            }

            // 닭고기
            else if (findMeat.meatType == IceCubeType.Chicken)
            {
                ChickenCount++;
            }

            else { /*Do Nothing*/ }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<FindMeat>() != null)
        {
            findMeat = collision.collider.GetComponent<FindMeat>();

            // 소고기
            if (findMeat.meatType == IceCubeType.Meat)
            {
                MeatCount--;
            }

            // 돼지고기
            else if (findMeat.meatType == IceCubeType.Pork)
            {
                PorkCount--;
            }

            // 닭고기
            else if (findMeat.meatType == IceCubeType.Chicken)
            {
                ChickenCount--;
            }

            else { /*Do Nothing*/ }
        }
    }

}
