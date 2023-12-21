using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCheck : MonoBehaviour
{
    public List<GameObject> dishes;

    private bool isInOder;
    private bool isMatchNumber;

    private int first;
    private int second;
    private int third;

    // 체크는 사과를 놓을 때마다
    public void CheckApple()
    {
        if (dishes[1].GetComponent<SecondDish>().isSecond == false)
        dishes[0].GetComponent<FirstDish>().isFirst = true;
    }

    public void CompleteCheck()
    {
        // 순서대로 올린 사과 체크


        // 숫자만큼 올린 사과 체크
        foreach (GameObject obj in dishes[0].GetComponent<Dish>().onDish)
        {
            if (obj.name == "Apple")
            {
                first += 1;
            }
        }

        foreach (GameObject obj in dishes[1].GetComponent<Dish>().onDish)
        {
            if (obj.name == "Apple")
            {
                second += 1;
            }
        }

        foreach (GameObject obj in dishes[2].GetComponent<Dish>().onDish)
        {
            if (obj.name == "Apple")
            {
                third += 1;
            }
        }
        
        if (first == 1 && second == 2 && third == 3)
        {

        }
        else
        {
            first = 0;
            second = 0;
            third = 0;
        }
    }
}
