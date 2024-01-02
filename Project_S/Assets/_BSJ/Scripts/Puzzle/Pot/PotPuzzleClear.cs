using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotPuzzleClear : MonoBehaviour, IActiveSign
{
    const int PUZZLEINDEX = 11;      // 이 퍼즐의 번호는 11번 입니다.

    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;

    /// <summary>
    /// 퍼즐을 클리어 했는지 체크하는 메서드
    /// </summary>
    public void CheckClearArray()
    {
        // 이미 퍼즐을 클리어 했다면 리턴
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

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

    /// <summary>
    /// 클리어 팻말의 활성화 여부
    /// </summary>
    /// <param name="_isClear"></param>
    public void ActiveClearSign(bool _isClear)
    {
        clearSign.SetActive(_isClear);
    }
}
