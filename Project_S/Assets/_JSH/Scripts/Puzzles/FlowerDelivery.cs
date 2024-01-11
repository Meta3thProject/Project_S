using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDelivery : MonoBehaviour
{
    // { BSJ PuzzleManager 에 맞춰서 추가된 변수들
    // 이 퍼즐의 인덱스
    const int PUZZLEINDEX = 18;
    // } BSJ PuzzleManager 에 맞춰서 추가된 변수들

    // 꽃배달 힌트
    public GameObject hint;

    // 배달 장소
    public GameObject[] addresses;

    // 힌트 제공
    public void DropHint()
    {
        // 이미 완료된 상태라면 함수 종료
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        // 퀘스트 수락 체크
        if (QuestManager.Instance.idToQuest[GetComponent<NPCBase>().questID].IsAccepted == true)
        {
            // NPC 앞에 힌트 생성
            Instantiate(hint, transform.position + transform.forward, Quaternion.identity);
        }
    }

    public void CompleteFlowerDelivery()
    {
        // 이미 완료된 상태라면 함수 종료
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }


        // 퍼즐 클리어 체크
        PuzzleManager.instance.puzzles[PUZZLEINDEX] = true;

        // 별의 총 갯수 증가
        StartCoroutine(StarManager.starManager.CallStar());

        // 별 구역의 클리어 체크
        PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);

        // 파이어베이스 RDB에 업데이트
        FirebaseManager.instance.PuzzleClearUpdateToDB(PUZZLEINDEX, true);
    }
}
