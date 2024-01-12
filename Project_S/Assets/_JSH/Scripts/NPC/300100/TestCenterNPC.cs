using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCenterNPC : NPCBase
{
    public Tutorial01Clear tutorial;

    // 투명 벽 지우는 필드
    public PlayerEnterPuzzleTrigger transparent_Wall;

    public override void PopUpDialog()
    {
        base.PopUpDialog();

        if (printID == 304171)
        {
            tutorial.CompleteTutorial();
            transparent_Wall.RemoveWall();
        }
    }
}
