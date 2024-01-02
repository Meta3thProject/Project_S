using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherShop02Clear : MonoBehaviour
{
    const int PUZZLEINDEX = 15;     // 이 퍼즐의 번호는 15번 입니다.
    const int PUZZLECOUNT = 3;      // 퍼즐의 배열의 요소는 3개입니다.

    // 퍼즐의 클리어 배열
    [SerializeField] private int[] clearCheck;

    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;

    private void Awake()
    {
        // 퍼즐 요소 배열 초기화
        clearCheck = new int[PUZZLECOUNT] { 0, 0, 0 };
    }

    /// <summary>
    /// 퍼즐 배열의 요소를 1로 만드는 함수
    /// </summary>
    /// <param name="_indexNumber">배열의 요소 인덱스</param>
    public void IncreaseClearCheck(int _indexNumber)
    {
        // 이미 퍼즐을 클리어 했다면 리턴
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        clearCheck[_indexNumber] = 1;
        CheckClearArray();
    }

    /// <summary>
    /// 퍼즐 배열의 요소를 0으로 만드는 메서드
    /// </summary>
    /// <param name="_indexNumber">배열의 요소 인덱스</param>
    public void DecreaseClearCheck(int _indexNumber)
    {
        // 이미 퍼즐을 클리어 했다면 리턴
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        clearCheck[_indexNumber] = 0;
    }

    /// <summary>
    /// 퍼즐을 클리어 했는지 체크하는 메서드
    /// </summary>
    public void CheckClearArray()
    {
        // 이미 퍼즐을 클리어 했다면 리턴
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

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
            PuzzleManager.instance.puzzles[PUZZLEINDEX] = true;

            // 별의 총 갯수 증가
            StartCoroutine(StarManager.starManager.CallStar());

            // 별 구역의 클리어 체크
            PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);

            // 파이어베이스 RDB에 업데이트
            FirebaseManager.instance.PuzzleClearUpdateToDB(PUZZLEINDEX, true);

            // 클리어 팻말 활성화
            ActiveClearSign(true);
        }
    }

    /// <summary>
    /// 클리어 팻말의 활성화 여부
    /// </summary>
    /// <param name="_isClear"></param>
    public void ActiveClearSign(bool _isClear)
    {
        clearSign.SetActive(_isClear);
    }
}
