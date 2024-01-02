using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSortPuzzleClear : MonoBehaviour, IActiveSign
{
    const int PUZZLEINDEX = 4;      // 이 퍼즐의 번호는 4번 입니다.
    const int PUZZLECOUNT = 4;      // 퍼즐의 배열의 요소는 4개입니다.

    // 퍼즐의 클리어 배열
    [field: SerializeField] public int[] clearCheck { get; private set; }

    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;

    // 이펙트
    private ParticleSystem particle;

    private void Awake()
    {
        // 퍼즐 요소 배열 초기화 & 이펙트 캐싱
        clearCheck = new int[PUZZLECOUNT] { 0, 0, 0, 0 };
        particle = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
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
