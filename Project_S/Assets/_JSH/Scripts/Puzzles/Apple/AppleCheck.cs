using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCheck : MonoBehaviour
{
    public List<GameObject> dishes;

    // { true 라면 체크 시작, false 라면 체크 안함
    private bool isComplete;
    // } true 라면 체크 시작, false 라면 체크 안함

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
        if (isComplete == true) { return; }

        for (int i = 0; i < idx_; i++)
        {
            if (dishes[i].GetComponent<Dish>().isAppleOn == false)
            {
                return;
            }
        }

        if (idx_ == 3)
        {
            isComplete = true;

            // 완료
            StartCoroutine(StarManager.starManager.CallStar());
        }
    }

    // 숫자만큼 올린 사과 체크
    public void CheckMatchNumber()
    {
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

            if (first == 1) { break; }
            else { /* Do Nothing */ }
        }

        foreach (GameObject obj in dishes[1].GetComponent<Dish>().onDish)
        {
            if (obj.name == "Apple")
            {
                second += 1;
            }

            if (second == 2) { break; }
            else { /* Do Nothing */ }
        }

        foreach (GameObject obj in dishes[2].GetComponent<Dish>().onDish)
        {
            if (obj.name == "Apple")
            {
                third += 1;
            }

            if (third == 3) { break; }
            else { /* Do Nothing */ }
        }

        if (first == 1 && second == 2 && third == 3)
        {
            isComplete = false;

            // 완료
            StartCoroutine(StarManager.starManager.CallStar());
        }
    }
}
