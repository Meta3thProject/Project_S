using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPuzzleClear : PuzzleClear
{
    const int PUZZLEINDEX = 17;
    const int PUZZLECOUNT = 4;

    [field: SerializeField] public int[] clearCheck { get; private set; }

    private void Awake()
    {
        clearCheck = new int[PUZZLECOUNT] { 0, 0, 0, 0 };
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

    /// <summary>
    /// 게임 중에 퍼즐이 클리어됐을 경우 호출하는 메서드.
    /// </summary>
    public void InGamePuzzleClear()
    {
        isClear = true;
        StartCoroutine(StarManager.starManager.CallStar());

        PuzzleManager.instance.puzzles[PUZZLEINDEX] = true;
    }

    /// <summary>
    /// 게임을 시작할 때 DB에서 클리어가 되었을 때 호출하는 메서드.
    /// </summary>
    public void PuzzleClearUpdateFromDB()
    {
        isClear = true;

        PuzzleManager.instance.puzzles[PUZZLEINDEX] = true;
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
            InGamePuzzleClear();
        }
    }
}
