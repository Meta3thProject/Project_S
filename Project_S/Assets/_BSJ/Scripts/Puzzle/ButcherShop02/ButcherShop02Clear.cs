using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherShop02Clear : MonoBehaviour
{
    const int PUZZLECOUNT = 3;

    [field: SerializeField] public bool isClear { get; private set; }

    [SerializeField] private int[] clearCheck;


    private void Awake()
    {
        clearCheck = new int[PUZZLECOUNT] { 0, 0, 0 };
        isClear = false;
    }

    public void IncreaseClearCheck(int _indexNumber)
    {
        clearCheck[_indexNumber] = 1;
        CheckClearArray();
    }

    public void DecreaseClearCheck(int _indexNumber)
    {
        clearCheck[_indexNumber] = 0;
    }

    public void PuzzleClear()
    {
        isClear = true;
        StartCoroutine(StarManager.starManager.CallStar());
        Debug.Log("정육점 퍼즐 2클리어!");
    }

    private void CheckClearArray()
    {
        foreach (int i in clearCheck)
        {
            if (i == 0)
            {
                return;
            }
        }

        if (isClear == false)
        {
            PuzzleClear();
        }
    }


}
