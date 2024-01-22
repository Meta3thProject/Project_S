using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TestCenterNPC : NPCBase, IPuzzleHolder
{
    // 이 NPC가 체크해야하는 퍼즐의 인덱스
    public const int PUZZLEINDEX = 14;

    public Tutorial01Clear tutorial;

    public LittleSpirit littleSpirit;

    // 투명 벽 지우는 필드
    public PlayerEnterPuzzleTrigger transparent_Wall;

    public override void PopUpDialog()
    {
        tutorial.CompleteTutorial();
        littleSpirit.SetLastQuest();

        base.PopUpDialog();

        if (printID == 304171)
        {
            transparent_Wall.RemoveWall();
        }
    }

    public bool PuzzleClearCheck()
    {
        return PuzzleManager.instance.puzzles[PUZZLEINDEX];
    }
}
