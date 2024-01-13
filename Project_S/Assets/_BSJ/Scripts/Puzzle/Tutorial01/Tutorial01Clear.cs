using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial01Clear : MonoBehaviour, IActiveSign
{
    // { BSJ PuzzleManager 에 맞춰서 추가된 변수들
    // 이 퍼즐의 인덱스
    const int PUZZLEINDEX = 14;
    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;
    // } BSJ PuzzleManager 에 맞춰서 추가된 변수들

    public NPCBase tutorialNPC;

    // 클리어 팻말의 활성화 여부
    public void ActiveClearSign(bool _isClear)
    {
        clearSign.SetActive(_isClear);
    }

    public void CompleteTutorial()
    {
        // 이미 완료된 상태라면 함수 종료
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        // 맞게 잘 가져왔으면 클리어
        if (InventoryFake.Instance.CheckOneValue(QuestManager.Instance.idToQuest[tutorialNPC.questID].Value1, QuestManager.Instance.idToQuest[tutorialNPC.questID].Value2))
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
