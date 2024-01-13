using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butler : NPCBase, IPuzzleHolder
{
    // 이 NPC가 체크해야하는 퍼즐의 인덱스
    public const int PUZZLEINDEX = 22;

    // 처음 말 걸었을 때, 퍼즐 벽이 쳐지는 스크립트.
    public PlayerEnterPuzzleTrigger transparentWall;

    // 고양이 오브젝트
    public GameObject catDoll;

    public override void PopUpDialog()
    {
        // 처음 말 걸때 퍼즐 벽 활성화 + 고양이 오브젝트 활성화
        if(printID == 304450) { transparentWall.ActiveWall(); catDoll.SetActive(true); }

        base.PopUpDialog();

        // 마지막 말 걸 때 퍼즐 벽 비활성화
        if(printID == 304455 ) { transparentWall.RemoveWall(); }
    }

    public bool PuzzleClearCheck()
    {
        return PuzzleManager.instance.puzzles[PUZZLEINDEX];
    }
}