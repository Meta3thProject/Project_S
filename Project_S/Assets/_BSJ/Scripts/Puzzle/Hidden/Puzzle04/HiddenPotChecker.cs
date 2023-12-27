using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPotChecker : MonoBehaviour
{
    const int PUZZLEINDEX = 3;      // 이 퍼즐의 번호는 3번입니다.
    const int FLOWERCOUNT = 3;      // 꽃 퍼즐 갯수

    // 정답 배열
    [SerializeField] public int[] flowerPuzzleClearArray;

    // 클리어 체크 스크립트
    private HiddenPuzzleClear hiddenPuzzleClear;

    private void Awake()
    {
        flowerPuzzleClearArray = new int[FLOWERCOUNT] { 0, 0, 0 };

        hiddenPuzzleClear = transform.root.GetComponent<HiddenPuzzleClear>();
    }

    /// <summary>
    /// 올바른 색상의 버튼으로 변경했으면 배열 요소를 1로 바꾸는 메서드.
    /// </summary>
    public void CheckClearButton(int _index, bool _isClear)
    {
        if (_isClear)
        {
            flowerPuzzleClearArray[_index] = 1;
            CheckClearPuzzle();
        }

        else
        {
            flowerPuzzleClearArray[_index] = 0;
            CheckClearPuzzle();
        }
    }

    /// <summary>
    /// 배열의 요소가 전부 1이면 퍼즐 클리어
    /// </summary>
    public void CheckClearPuzzle()
    {
        int puzzleClear = 0;

        foreach (int _indexNumber in flowerPuzzleClearArray)
        {
            // 배열 요소에서 0이 있다면 퍼즐을 클리어한 것이 아님.
            if (_indexNumber == 0)
            {
                puzzleClear++;
            }
        }

        // 모든 버튼을 알맞는 색으로 변경했다면 퍼즐 클리어
        if (puzzleClear == 0)
        {
            hiddenPuzzleClear.IncreaseClearCheck(PUZZLEINDEX);
        }

        else
        {
            hiddenPuzzleClear.DecreaseClearCheck(PUZZLEINDEX);
        }
    }
}
