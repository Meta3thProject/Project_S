using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePuzzleClear : MonoBehaviour, IActiveSign
{
    const int PUZZLEINDEX = 5;      // 이 퍼즐의 번호는 5번 입니다.
    const int TREEPUZZLECOUNT = 3;  // 퍼즐의 요소는 3개입니다.

    // 나무 오브젝트
    [SerializeField] private GameObject[] trees = new GameObject[TREEPUZZLECOUNT];

    // 퍼즐의 클리어 배열
    [field: SerializeField] public int[] clearCheck { get; private set; }

    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;

    // 퍼즐을 막는 투명벽
    [SerializeField] private PlayerEnterPuzzleTrigger transparentWall;

    private void Awake()
    {
        // 나무 오브젝트 캐싱
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i] = transform.GetChild(i).gameObject;
        }

        clearCheck = new int[TREEPUZZLECOUNT] { 0, 0, 0 };
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
        CheckClearArray(_indexNumber, TREEPUZZLECOUNT);
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
    private void CheckClearArray(int _BrokenTreeNumber, int _puzzleCount)
    {
        int clearCount = 0;

        // 0번부터 순차적으로 나무가 깨지지 않았다면 나무 재생성.
        for(int i = 0; i < _BrokenTreeNumber + 1; i++)
        {
            if (clearCheck[i] == 0)
            {
                for (int j = 0; j < trees.Length; j++)
                {
                    DecreaseClearCheck(j);
                    TreePuzzle treePuzzle = trees[j].GetComponent<TreePuzzle>();
                    StartCoroutine(treePuzzle.RespawnTree());
                }

                return;
            }

            else if(clearCheck[i] != 0)
            {
                clearCount++;
            }
        }

        // 클리어!
        if (clearCount == _puzzleCount && PuzzleManager.instance.puzzles[PUZZLEINDEX] == false)
        {
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
