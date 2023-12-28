using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // 싱글톤
    public static PuzzleManager instance;

    // 퍼즐의 갯수
    const int PUZZLECOUNT = 30;

    // 퍼즐 리스트
    public bool[] puzzles;

    // 퍼즐의 클리어 여부를 담은 딕셔너리 key : 퍼즐인덱스, value : 클리어 여부
    public Dictionary<int, bool> puzzleClearDictionary = new Dictionary<int, bool>();

    // 퍼즐 클리어 스크립트들... 순서대로 퍼즐 번호
    [field: SerializeField] public Upon3treePuzzleClear upon3TreePuzzleClear { get; private set; }
    [field: SerializeField] public LetterPuzzleClear letterPuzzleClear { get; private set; }
    [field: SerializeField] public ChessPuzzleClear ChessPuzzleClear { get; private set; }

    private void Awake()
    {
        // { 싱글톤
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }       // } 싱글톤

        // { 퍼즐 클리어 초기화
        puzzles = new bool[PUZZLECOUNT];

        for (int i = 0; i < PUZZLECOUNT; i++) 
        {
            puzzles[i] = false;
            CheckPuzzleClear(i, puzzles[i]);
        }       // } 퍼즐 클리어 초기화

        // 테스트
        upon3TreePuzzleClear._isClear = true;
    }

    /// <summary>
    /// 퍼즐의 클리어 여부를 판단해서 퍼즐클리어 배열과 딕셔너리를 업데이트하는 함수
    /// </summary>
    /// <param name="_puzzleIndex">퍼즐인덱스</param>
    /// <param name="_isClear">클리어여부</param>
    public void CheckPuzzleClear(int _puzzleIndex, bool _isClear)
    {
        // 배열 업데이트
        puzzles[_puzzleIndex] = _isClear;

        // 딕셔너리 업데이트 [ key가 있다면 value 업데이트 ]
        if (puzzleClearDictionary.ContainsKey(_puzzleIndex)) 
        {
            puzzleClearDictionary[_puzzleIndex] = _isClear;
        }

        else
        {
            puzzleClearDictionary.Add(_puzzleIndex, _isClear);
        }
    }
}
