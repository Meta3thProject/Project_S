using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCheck : MonoBehaviour
{
    public List<GameObject> dishes;

    // 순서 체크 
    private bool isInOrder;
    // 완료 체크
    private bool isComplete;

    private int first;
    private int second;
    private int third;

    private void Awake()
    {
        isComplete = false;
    }

    // 순서대로 올린 사과 체크
    public void CheckInOrder(int idx_)
    {
        // 이미 완료된 상태라면 함수 종료
        if (isComplete == true) { return; }

        // 순서대로 놓지 않았을 때 
        if (isInOrder == false)
        {
            for (int i = 0; i < dishes.Count; i++)
            {
                // 하나라도 사과가 아직 놓여있다면 함수 종료
                if (dishes[i].GetComponent<Dish>().isAppleOn == true) { return; }

                // 사과가 하나도 놓여있지 않다면 다시 체크하기 위해 리셋
                isInOrder = true;
            }
        }

        for (int i = 0; i < idx_; i++)
        {
            // 하나라도 순서대로 놓지 않으면
            if (dishes[i].GetComponent<Dish>().isAppleOn == false)
            {
                isInOrder = false;
                return;
            }
        }

        // 세개를 모두 순서대로 놓았다면
        if (idx_ == 3 && isInOrder == true)
        {
            isComplete = true;

            // 완료
            StartCoroutine(StarManager.starManager.CallStar());
        }
    }

    // 숫자만큼 올린 사과 체크
    public void CheckMatchNumber()
    {
        // 이미 완료된 상태라면 함수 종료
        if (isComplete == true) { return; }

        first = 0;
        second = 0;
        third = 0;

        foreach (GameObject obj in dishes[0].GetComponent<Dish>().onDish)
        {
            if (obj.name == "Apple")
            {
                first += 1;
            }

            //if (first == 1) { break; }
            //else { /* Do Nothing */ }
        }

        foreach (GameObject obj in dishes[1].GetComponent<Dish>().onDish)
        {
            if (obj.name == "Apple")
            {
                second += 1;
            }

            //if (second == 2) { break; }
            //else { /* Do Nothing */ }
        }

        foreach (GameObject obj in dishes[2].GetComponent<Dish>().onDish)
        {
            if (obj.name == "Apple")
            {
                third += 1;
            }

            //if (third == 3) { break; }
            //else { /* Do Nothing */ }
        }

        if (first == 1 && second == 2 && third == 3)
        {
            isComplete = true;

            // 완료
            StartCoroutine(StarManager.starManager.CallStar());
        }
    }
}
