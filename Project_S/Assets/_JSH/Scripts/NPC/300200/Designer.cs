using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Designer : NPCBase, IPuzzleHolder
{
    // 이 NPC가 체크해야하는 퍼즐의 인덱스
    public const int PUZZLEINDEX = 10;

    // 처음 말 걸었을 때, 퍼즐 벽이 쳐지는 스크립트.
    public PlayerEnterPuzzleTrigger transparentWall;

    public override void PopUpDialog()
    {
        // bsj _ 퍼즐 벽 쳐지는 메서드.
        if(printID == 304247) { transparentWall.ActiveWall(); }
        
        base.PopUpDialog();

        if(printID == 304252) { transparentWall.RemoveWall(); }
    }

    public bool PuzzleClearCheck()
    {
        return PuzzleManager.instance.puzzles[PUZZLEINDEX];
    }
}
