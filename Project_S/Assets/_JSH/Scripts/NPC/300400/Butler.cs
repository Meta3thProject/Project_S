using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butler : NPCBase, IPuzzleHolder
{
    // 이 NPC가 체크해야하는 퍼즐의 인덱스
    public const int PUZZLEINDEX = 24;

    public override void PopUpDialog()
    {
        base.PopUpDialog();
    }

    public bool PuzzleClearCheck()
    {
        return PuzzleManager.instance.puzzles[PUZZLEINDEX];
    }
}