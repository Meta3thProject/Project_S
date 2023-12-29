using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TaxidermyClear : MonoBehaviour
{
    const int PUZZLEINDEX = 8;      // 이 퍼즐의 번호는 8번 입니다.
    const int PUZZLECOUNT = 4;      // 이 퍼즐의 요소는 4개 입니다.

    // 퍼즐의 클리어 체크 배열
    [field: SerializeField] public int[] clearCheck { get; private set; }

    // 퍼즐의 클리어 여부
    public bool _isClear;

    private void Awake()
    {
        clearCheck = new int[PUZZLECOUNT] { 0, 0, 0, 0 };
        _isClear = false;
    }

    /// <summary>
    /// 퍼즐 배열의 요소를 1로 만드는 함수
    /// </summary>
    /// <param name="_indexNumber">배열의 요소 인덱스</param>
    public void IncreaseClearCheck(int _indexNumber)
    {
        clearCheck[_indexNumber] = 1;
        CheckClearArray();
    }

    /// <summary>
    /// 퍼즐 배열의 요소를 0으로 만드는 메서드
    /// </summary>
    /// <param name="_indexNumber">배열의 요소 인덱스</param>
    public void DecreaseClearCheck(int _indexNumber)
    {
        clearCheck[_indexNumber] = 0;
    }

    /// <summary>
    /// 퍼즐을 클리어 했는지 체크하는 메서드
    /// </summary>
    private void CheckClearArray()
    {
        // 배열에 0이 없다면 퍼즐 클리어
        foreach (int i in clearCheck)
        {
            if (i == 0)
            {
                return;
            }
        }

        // 퍼즐 클리어 체크
        _isClear = true;

        // 별의 총 갯수 증가
        StartCoroutine(StarManager.starManager.CallStar());

        // 별 구역의 클리어 체크
        PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);
    }
}
