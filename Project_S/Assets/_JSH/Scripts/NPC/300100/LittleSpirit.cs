using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSpirit : NPCBase, IPuzzleHolder
{
    // 퀘스트가 변화함 301100 ~ 301108
    // 퀘스트가 변화하면 이동함
    // 정해놓은 이동 시점들
    private int[] ids = { 304117, 304133, 304172 };
    // 정해놓은 이동할 위치들: 직렬화
    public Transform[] fixedLocations;
    // 도끼로 부술 나무 장벽
    public GameObject woodenWall;
    // 체크해야하는 퍼즐의 인덱스: 퍼즐 매니저에서 13, 14에 해당한다
    // 퍼즐하나가 사라짐 13
    public const int PUZZLEINDEX = 14;

    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            printID = QuestManager.Instance.idToQuest[questID].CompleteID;
            SetQuestID();
        }

        if (QuestManager.Instance.idToQuest[301105].IsCompleted)
        {
            questID = 301107;
            printID = QuestManager.Instance.idToQuest[questID].BeforeID;
        }

        // 301102 301107
        if (questID >= 301102 && questID < 301103)
        {
            transform.position = fixedLocations[0].position;
            transform.rotation = fixedLocations[0].rotation;
        }
        else if (questID >= 301104 && questID < 301107)
        {
            transform.position = fixedLocations[1].position;
            transform.rotation = fixedLocations[1].rotation;
        }
        else if (questID == 301107)
        {
            transform.position = fixedLocations[2].position;
            transform.rotation = fixedLocations[2].rotation;
        }
    }

    // 퀘스트 진행 함수: 상호작용 시 호출
    public void SetQuestID()
    {
        // 현재 퀘스트ID에 해당하는 퀘스트를 완료했다면
        if (QuestManager.Instance.idToQuest[questID].IsCompleted)
        {
            // 301106 퀘스트는 다른 NPC의 퀘스트이므로 기준으로 한다
            // 다음 퀘스트ID 설정 301105까지
            if (questID < 301105) { questID += 1; }
        }
        // 완료하지 않았다면 아무것도 하지 않음
        else { /* Do Nothing */ }
    }

    public void SetLastQuest()
    {
        questID = 301107;
        printID = QuestManager.Instance.idToQuest[questID].BeforeID;
        MoveNPC();
    }

    // 이동
    public void MoveNPC()
    {
        // 현재 출력문ID로 이동할지 말지 정한다
        for (int i = 0; i < ids.Length; i++)
        {
            if (printID == ids[i])
            {
                // 해당 위치로 이동
                transform.position = fixedLocations[i].position;
                transform.rotation = fixedLocations[i].rotation;
            }
        }
    }

    public override void PopUpDialog()
    {
        base.PopUpDialog();

        SetQuestID();
        MoveNPC();
    }

    public bool PuzzleClearCheck()
    {
        if (questID == 301102)
        {
            // 나무벽이 활성화된 상태라면 false, 비활성화된 상태라면 true 반환
            return !woodenWall.activeInHierarchy;
        }
        else if (questID == 301105)
        {
            return PuzzleManager.instance.puzzles[PUZZLEINDEX];
        }
        else { return false; }
    }
}
