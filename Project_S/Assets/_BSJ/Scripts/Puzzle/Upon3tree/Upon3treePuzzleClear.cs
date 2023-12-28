using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upon3treePuzzleClear : MonoBehaviour
{
    // 이 퍼즐의 번호는 0번 입니다.
    const int PUZZLEINDEX = 0;

    // 퍼즐의 클리어 여부
    public bool _isClear;

    private void Awake()
    {
        // 퍼즐의 클리어 여부 [거짓으로 초기화]
        _isClear = false;
    }

    public void PuzzleClear()
    {
        // 이미 퍼즐을 클리어 했다면 리턴.
        if (_isClear) { return; }

        // 퍼즐 클리어 체크
        _isClear = true;

        // 별의 총 갯수 증가
        StartCoroutine(StarManager.starManager.CallStar());

        // 별 구역의 클리어 체크
        PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);
    }


}
