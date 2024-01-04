using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopOwner : NPCBase, IPuzzleHolder
{
    // 이 NPC가 체크해야하는 퍼즐의 인덱스
    public const int PUZZLEINDEX = 11;

    public override void PopUpDialog()
    {
        base.PopUpDialog();
    }

    public bool PuzzleClearCheck()
    {
        return PuzzleManager.instance.puzzles[PUZZLEINDEX];
    }
}
