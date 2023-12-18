using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudioPuzzleClear : MonoBehaviour
{
    const int PUZZLE_COUNT = 4;

    [field: SerializeField] public int[] clearCheck { get; private set; }

    [field: SerializeField] public bool isClear { get; private set; }

    private void Awake()
    {
        clearCheck = new int[PUZZLE_COUNT] { 0, 0, 0, 0 };
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
        Debug.Log("클리어!");
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
