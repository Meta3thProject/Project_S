using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxidermyClear : MonoBehaviour, IActiveSign
{
    const int PUZZLEINDEX = 8;      // 이 퍼즐의 번호는 8번 입니다.
    const int PUZZLECOUNT = 4;      // 이 퍼즐의 요소는 4개 입니다.

    // 퍼즐의 클리어 체크 배열
    [field: SerializeField] public int[] clearCheck { get; private set; }

    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;

    // 퍼즐을 막는 투명벽
    [SerializeField] private PlayerEnterPuzzleTrigger transparentWall;

    // 퍼즐을 클리어하면 나오는 덫과 총 오브젝트 아이템
    [SerializeField] private GameObject trap;
    [SerializeField] private GameObject gum;

    private void Awake()
    {
        clearCheck = new int[PUZZLECOUNT] { 0, 0, 0, 0 };
    }

    /// <summary>
    /// 퍼즐 배열의 요소를 1로 만드는 함수
    /// </summary>
    /// <param name="_indexNumber">배열의 요소 인덱스</param>
    public void IncreaseClearCheck(int _indexNumber)
    {
        // 이미 퍼즐을 클리어 했다면 리턴
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        clearCheck[_indexNumber] = 1;
        CheckClearArray();
    }

    /// <summary>
    /// 퍼즐 배열의 요소를 0으로 만드는 메서드
    /// </summary>
    /// <param name="_indexNumber">배열의 요소 인덱스</param>
    public void DecreaseClearCheck(int _indexNumber)
    {
        // 이미 퍼즐을 클리어 했다면 리턴
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        clearCheck[_indexNumber] = 0;
    }

    /// <summary>
    /// 퍼즐을 클리어 했는지 체크하는 메서드
    /// </summary>
    private void CheckClearArray()
    {
        // 배열에 0이 없다면 퍼즐 클리어
        foreach (int i in clearCheck)
        {
            if (i == 0)
            {
                return;
            }
        }

        // 총 & 덫 아이템을 드랍한다.
        DropItem();

        // 퍼즐 클리어 체크
        PuzzleManager.instance.puzzles[PUZZLEINDEX] = true;

        // 별의 총 갯수 증가
        StartCoroutine(StarManager.starManager.CallStar());

        // 별 구역의 클리어 체크
        PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);

        // 파이어베이스 RDB에 업데이트
        FirebaseManager.instance.PuzzleClearUpdateToDB(PUZZLEINDEX, true);

        // 클리어 팻말 활성화
        ActiveClearSign(true);

        // 퍼즐을 막는 투명벽 해제
        transparentWall.RemoveWall();
    }

    /// <summary>
    /// 클리어 팻말의 활성화 여부
    /// </summary>
    /// <param name="_isClear"></param>
    public void ActiveClearSign(bool _isClear)
    {
        clearSign.SetActive(_isClear);
    }

    /// <summary>
    /// 박제퍼즐을 다 알맞게 놓았을 때, 총 & 덫 아이템을 드랍하는 메서드.
    /// </summary>
    private void DropItem()
    {
        trap.SetActive(true);
        gum.SetActive(true);
    }
}
