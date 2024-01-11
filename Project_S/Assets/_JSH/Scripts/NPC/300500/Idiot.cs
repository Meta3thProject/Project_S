using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idiot : NPCBase, IPuzzleHolder
{
    // 숨을 장소
    public Transform hidePos;

    // 이 NPC가 체크해야하는 퍼즐의 인덱스
    public const int HIDESEEKINDEX = 18;
    public const int FLOWERDELINDEX = 18;

    // 301503, 301504
    public void SetQuestID()
    {
        if (questID == 301503)
        {
            if (PuzzleManager.instance.puzzles[HIDESEEKINDEX] == true)
            {
                questID = 301504;
            }
        }
    }

    public override void PopUpDialog()
    {
        // 완료를 NPC가 체크
        if (questID == 301503)
        {
            GetComponent<HideAndSeek>().ComleteHideAndSeek();
        }
        else if (questID == 301504)
        {

        }
        else { return; }

        base.PopUpDialog();

        // 퀘스트에 맞는 힌트 제공
        if (questID == 301503)
        {
            GetComponent<HideAndSeek>().DropHint();
        }
        else if (questID == 301504)
        {
            GetComponent<FlowerDelivery>().DropHint();
        }
        else { return; }

        SetQuestID();
    }

    public bool PuzzleClearCheck()
    {
        if (questID == 301503)
        {
            return PuzzleManager.instance.puzzles[HIDESEEKINDEX];
        }
        else if (questID == 301504)
        {
            return PuzzleManager.instance.puzzles[FLOWERDELINDEX];
        }
        else { return false; }
    }
}
