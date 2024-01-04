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
    // 체크해야하는 퍼즐의 인덱스: 퍼즐 매니저에서 13, 14에 해당한다
    public const int AXEPUZZLE = 13;
    public const int DOORPUZZLE = 14;

    // 퀘스트 진행 함수: 상호작용 시 호출
    public void SetQuestID()
    {
        // 현재 퀘스트ID에 해당하는 퀘스트를 완료했다면
        if (QuestManager.Instance.idToQuest[questID].IsCompleted)
        {
            // 301106 퀘스트는 다른 NPC의 퀘스트이므로 기준으로 한다
            // 다음 퀘스트ID 설정
            if (questID < 301106)
            {
                questID += 1;
            }
            // 301106 건너뛰기
            if (questID == 301106) { questID += 1; }

            if (questID == 301107)
            {
                // 바뀐 퀘스트ID에 맞는 출력문ID로 설정
                printID = QuestManager.Instance.idToQuest[questID].BeforeID;
            }
        }
        // 완료하지 않았다면 아무것도 하지 않음
        else { /* Do Nothing */ }
    }

    // 이동법 익히기
    public void HowToMove()
    {
        // 일정 거리내에 플레이어가 감지되면
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20);
        // 감지한 것이 없으면 함수 종료
        if (colliders.Length == 0) { return; }
        // 감지한 것들 중에 
        foreach (Collider collider in colliders)
        {
            // 플레이어가 있으면
            if (collider.gameObject == NPCManager.Instance.player)
            {
                // 대화 시작
                break;
            }
        }
    }

    public override void PopUpDialog()
    {
        base.PopUpDialog();

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

    public bool PuzzleClearCheck()
    {
        // 아직 퍼즐이 없으므로 true 반환
        return true;

        //if (questID == 301102)
        //{
        //    return PuzzleManager.instance.puzzles[AXEPUZZLE];
        //}
        //else if (questID == 301105)
        //{
        //    return PuzzleManager.instance.puzzles[DOORPUZZLE];
        //}
    }
}
