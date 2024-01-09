using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPuzzleClear : MonoBehaviour
{
    const int PUZZLEINDEX = 22;     // 이 퍼즐의 번호는 22번 입니다.

    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;

    // 퍼즐을 완료하기 위한 NPC
    [SerializeField] private NPCBase catOwner;

    /// <summary>
    /// 클리어 체크 메서드.
    /// </summary>
    public void CheckClear()
    {
        // 이미 퍼즐을 클리어 했다면 리턴
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        PuzzleClear();
    }

    /// <summary>
    /// 퍼즐을 클리어하는 메서드.
    /// </summary>
    public void PuzzleClear()
    {
        // 이미 퍼즐을 클리어 했다면 리턴
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        // 퍼즐 클리어 체크
        PuzzleManager.instance.puzzles[PUZZLEINDEX] = true;

        // 별의 총 갯수 증가
        StartCoroutine(StarManager.starManager.CallStar());

        // 별 구역의 클리어 체크
        PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);

        // 클리어 팻말 활성화
        ActiveClearSign(true);

        // NPC 대화
        NPCManager.Instance.interacted = catOwner;

        Vector3 npcDir = (catOwner.transform.position - NPCManager.Instance.player.transform.position).normalized;
        npcDir.y = 0;

        NPCManager.Instance.PopUp(npcDir);

        NPCManager.Instance.interacted.PopUpDialog();

        // 파이어베이스 RDB에 업데이트
        FirebaseManager.instance.PuzzleClearUpdateToDB(PUZZLEINDEX, true);
    }

    /// <summary>
    /// 클리어 팻말의 활성화 여부
    /// </summary>
    /// <param name="_isClear"></param>
    public void ActiveClearSign(bool _isClear)
    {
        clearSign.SetActive(_isClear);
    }
}
