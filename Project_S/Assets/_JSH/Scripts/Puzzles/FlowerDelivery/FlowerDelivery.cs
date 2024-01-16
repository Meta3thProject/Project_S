using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDelivery : MonoBehaviour, IActiveSign
{
    // { BSJ PuzzleManager 에 맞춰서 추가된 변수들
    // 이 퍼즐의 인덱스
    const int PUZZLEINDEX = 18;
    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;
    // } BSJ PuzzleManager 에 맞춰서 추가된 변수들

    // NPC
    public NPCBase npc;

    // 꽃배달 힌트
    public GameObject hint;

    // 배달 장소
    public GameObject[] addresses;

    // 정답
    private int[] answer;

    private void Awake()
    {
        answer = new int[4];

        answer[0] = 104010;
        answer[1] = 104008;
        answer[2] = 104011;
        answer[3] = 104009;
    }

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
            // NPC 앞에 힌트 생성
            Instantiate(hint, transform.position + transform.forward, Quaternion.identity);
        }
    }

    public void CompleteFlowerDelivery()
    {
        // 이미 완료된 상태라면 함수 종료
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        for (int i = 0; i < addresses.Length; i++)
        {
            // 배달된 꽃바구니
            GameObject flower_ = addresses[i].GetComponent<FlowerColorCheck>().delivered;

            // 하나라도 비어있으면 함수 종료
            if (flower_ == null || flower_ == default)
            { return; }

            // 배달된 꽃바구니의 ID와 정답 비교: 
            // 아니라면
            if (flower_.GetComponent<ItemData>().itemID != answer[i])
            // 함수 종료
            { return; }
            // 맞다면 통과
            else { /* Do Nothing */ }
        }

        // 끝까지 통과하면 퍼즐 클리어

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

    public bool IsFlowerBusket(int id_)
    {
        for(int i = 0; i < answer.Length; i++)
        {
            if(id_ == answer[i])
            {
                return true;
            }
        }

        return false;
    }
}
