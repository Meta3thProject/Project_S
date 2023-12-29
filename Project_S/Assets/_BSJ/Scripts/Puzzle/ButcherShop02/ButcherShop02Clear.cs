using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherShop02Clear : MonoBehaviour
{
    const int PUZZLEINDEX = 15;     // 이 퍼즐의 번호는 15번 입니다.
    const int PUZZLECOUNT = 3;      // 퍼즐의 배열의 요소는 3개입니다.

    // 퍼즐을 클리어 했는지 체크
    public bool _isClear;

    // 퍼즐의 클리어 배열
    [SerializeField] private int[] clearCheck;

    private void Awake()
    {
        clearCheck = new int[PUZZLECOUNT] { 0, 0, 0 };
        _isClear = false;
    }

    /// <summary>
    /// 퍼즐 배열의 요소를 1로 만드는 함수
    /// </summary>
    /// <param name="_indexNumber">배열의 요소 인덱스</param>
    public void IncreaseClearCheck(int _indexNumber)
    {
        // 이미 클리어가 되었다면 리턴
        if (_isClear) { return; }

        clearCheck[_indexNumber] = 1;
        CheckClearArray();
    }

    /// <summary>
    /// 퍼즐 배열의 요소를 0으로 만드는 메서드
    /// </summary>
    /// <param name="_indexNumber">배열의 요소 인덱스</param>
    public void DecreaseClearCheck(int _indexNumber)
    {
        // 이미 클리어가 되었다면 리턴
        if (_isClear) { return; }

        clearCheck[_indexNumber] = 0;
    }

    /// <summary>
    /// 퍼즐을 클리어 했는지 체크하는 메서드
    /// </summary>
    public void CheckClearArray()
    {
        if (_isClear) { return; }

        // 퍼즐의 요소가 0인지 체크하기 위한 변수.
        int noClearCheckCount = 0;

        foreach (int clearIndex in clearCheck)
        {
            // 퍼즐의 요소가 0이면 (클리어되지 않았다면) 클리어 카운트를 1 증가시킵니다.
            if (clearIndex == 0)
            {
                noClearCheckCount++;
                break;
            }
        }

        // 퍼즐 클리어가 되지 않은 0번이 없기 때문에 클리어
        if (noClearCheckCount == 0)
        {
            // 퍼즐 클리어 체크
            _isClear = true;

            // 별의 총 갯수 증가
            StartCoroutine(StarManager.starManager.CallStar());

            // 별 구역의 클리어 체크
            PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);
        }
    }
}
