using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeek : MonoBehaviour, IActiveSign
{
    // { BSJ PuzzleManager 에 맞춰서 추가된 변수들
    // 이 퍼즐의 인덱스
    const int PUZZLEINDEX = 21;
    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;
    // } BSJ PuzzleManager 에 맞춰서 추가된 변수들

    // NPC
    public NPCBase npc;

    // 숨바꼭질 힌트
    public GameObject hint;

    // 클리어 팻말의 활성화 여부
    public void ActiveClearSign(bool _isClear)
    {
        clearSign.SetActive(_isClear);
    }

    // 힌트 제공
    public void DropHint()
    {
        // 이미 완료된 상태라면 함수 종료
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        // 퀘스트 수락 체크
        if (QuestManager.Instance.idToQuest[npc.questID].IsAccepted == true)
        {
            // 힌트 생성
            Instantiate(hint, npc.transform.position, Quaternion.identity);
        }
    }

    // 완료 체크
    public void ComleteHideAndSeek()
    {
        // 이미 완료된 상태라면 함수 종료
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        if (QuestManager.Instance.idToQuest[npc.questID].IsAccepted == true)
        {
            // 퍼즐 클리어 체크
            PuzzleManager.instance.puzzles[PUZZLEINDEX] = true;

            // 별의 총 갯수 증가
            StartCoroutine(StarManager.starManager.CallStar());

            // 별 구역의 클리어 체크
            PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);

            // 파이어베이스 RDB에 업데이트
            FirebaseManager.instance.PuzzleClearUpdateToDB(PUZZLEINDEX, true);

            // 팻말 활성화
            ActiveClearSign(true);
        }
    }
}
