using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upon3treePuzzleClear : MonoBehaviour
{
    // 이 퍼즐의 번호는 0번 입니다.
    private int PUZZLEINDEX = 0;

    public void PuzzleClear()
    {
        // 이미 퍼즐을 클리어 했다면 리턴.
        if (PuzzleManager.instance.puzzles[0] == true) { return; }

        // 퍼즐 클리어 체크
        PuzzleManager.instance.puzzles[0] = true;

        // 별의 총 갯수 증가
        StartCoroutine(StarManager.starManager.CallStar());

        // 별 구역의 클리어 체크
        PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);

        // 파이어베이스 RDB에 업데이트
        FirebaseManager.instance.PuzzleClearUpdateToDB(PUZZLEINDEX, true);
    }


}
