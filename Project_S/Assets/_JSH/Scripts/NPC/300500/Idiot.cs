using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idiot : NPCBase, IPuzzleHolder
{
    // 숨을 장소
    public Transform hidePos;

    // 이 NPC가 체크해야하는 퍼즐의 인덱스
    public const int HIDESEEKINDEX = 17;
    public const int FLOWERDELINDEX = 18;

    // 처음 말 걸었을 때, 퍼즐 벽이 쳐지는 스크립트.
    public PlayerEnterPuzzleTrigger hideAndSeekTransparentWall;
    public PlayerEnterPuzzleTrigger flowerDeliveryTransparentWall;

    private void Start()
    {
        if (questID == 301504)
        {
            ChangePosition();
        }
    }

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
        // 처음 말 걸었을 때 퍼즐 벽 활성화
        if (printID == 304516) { hideAndSeekTransparentWall.ActiveWall(); }
        else if (printID == 304522) { flowerDeliveryTransparentWall.ActiveWall(); }

        // 완료를 NPC가 체크
        if (questID == 301503)
        {
            PuzzleManager.instance.hideAndSeekPuzzleClear.ComleteHideAndSeek();
        }
        else { return; }

        base.PopUpDialog();

        // npc 마지막 대화 일 때 퍼즐 벽 비활성화
        if (printID == 304521) { hideAndSeekTransparentWall.RemoveWall(); }
        else if (printID == 304525) { flowerDeliveryTransparentWall.RemoveWall(); }

        // 퀘스트에 맞는 힌트 제공
        if (questID == 301503)
        {
            PuzzleManager.instance.hideAndSeekPuzzleClear.DropHint();
            ChangePosition();
        }
        else if (questID == 301504)
        {
            PuzzleManager.instance.flowerDeliveryPuzzleClear.DropHint();
        }
        else { return; }

        SetQuestID();
    }

    public void ChangePosition()
    {
        transform.position = hidePos.transform.position;
        transform.rotation = hidePos.transform.rotation;
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
