using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOwner : NPCBase, IPuzzleHolder
{
    // 이 NPC가 체크해야하는 퍼즐의 인덱스
    //public const int PUZZLEINDEX = 아직 없음

    public override void PopUpDialog()
    {
        base.PopUpDialog();
    }

    public bool PuzzleClearCheck()
    {
        // 아직 퍼즐이 없으므로 true 반환
        return true;
    }
}
