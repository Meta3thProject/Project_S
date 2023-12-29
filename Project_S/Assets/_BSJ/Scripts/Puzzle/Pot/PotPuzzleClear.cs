using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PotPuzzleClear : MonoBehaviour
{
    const int PUZZLEINDEX = 11;      // 이 퍼즐의 번호는 11번 입니다.

    // 퍼즐의 클리어 여부
    public bool _isClear;

    /// <summary>
    /// 퍼즐을 클리어 했는지 체크하는 메서드
    /// </summary>
    public void CheckClearArray()
    {
        if (_isClear) { return; }

        // 퍼즐 클리어 체크
        _isClear = true;

        // 별의 총 갯수 증가
        StartCoroutine(StarManager.starManager.CallStar());

        // 별 구역의 클리어 체크
        PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);
    }
}
