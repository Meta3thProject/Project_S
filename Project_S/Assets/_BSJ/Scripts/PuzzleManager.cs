using Firebase.Database;
using Firebase.Extensions;
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

    // 퍼즐 클리어 스크립트들입니다. [ 순서대로 퍼즐 번호 ] DB에서 퍼즐 클리어의 여부에 따라 해당 퍼즐 스크립트에 클리어 여부를 체크해주기 위해 캐싱해두었음.
    [field: SerializeField] public Upon3treePuzzleClear upon3TreePuzzleClear { get; private set; }      // 0
    [field: SerializeField] public LetterPuzzleClear letterPuzzleClear { get; private set; }            // 1
    [field: SerializeField] public FlowerPuzzleClear flowerPuzzleClear { get; private set; }            // 2
    [field: SerializeField] public ChessPuzzleClear chessPuzzleClear { get; private set; }              // 3
    [field: SerializeField] public BookSortPuzzleClear bookSortPuzzleClear { get; private set; }        // 4
    [field: SerializeField] public TreePuzzleClear treePuzzleClear { get; private set; }                // 5
    [field: SerializeField] public ShieldPuzzleClear shieldPuzzleClear { get; private set; }            // 6
    [field: SerializeField] public StudioPuzzleClear studioPuzzleClear { get; private set; }            // 7
    [field: SerializeField] public TaxidermyClear taxidermyClear { get; private set; }                  // 8
    // TODO : 접시에 사과 올리기 퍼즐                                                                     // 9
    // TODO : 옷감 퍼즐                                                                                  // 10
    [field: SerializeField] public PotPuzzleClear potPuzzleClear { get; private set; }                  // 11
    [field: SerializeField] public ButcherShop01Clear butcherShop01Clear { get; private set; }          // 12
    // TODO : 듀토리얼 퍼즐 01                                                                           // 13
    // TODO : 듀토리얼 퍼즐 02                                                                           // 14
    [field: SerializeField] public ButcherShop02Clear butcherShop02Clear { get; private set; }          // 15
    [field: SerializeField] public ButcherShop03Clear butcherShop03Clear { get; private set; }          // 16
    [field: SerializeField] public HiddenPuzzleClear hiddenPuzzleClear { get; private set; }            // 17
    // TODO : 추후 개발 될 퍼즐이 더 추가될 예정 ...

    // 파이어 베이스
    private DatabaseReference reference;    // 루트 레퍼런스

    private void Awake()
    {
        // { 싱글톤
        if (null == instance)
        {
            instance = this;

            // LEGACY : 메인 씬에서만 PuzzleManager를 사용할 예정
            // DontDestroyOnLoad(this.gameObject);
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
    }

    private void Start()
    {
        // 퍼즐 DB 받아오기
        FirebaseManager.instance.PuzzleClearUpdateFromDB();
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

    /// <summary>
    /// 클리어 여부를 체크해서 펫말을 활성 / 비활성화 하는 이유
    /// </summary>
    public void ActiveSign()
    {
        // 퍼즐이 클리어가 되었다면 팻말 세우기
        for(int i = 0; i < PUZZLECOUNT; i++)
        {
            if (i == 0) { upon3TreePuzzleClear.ActiveClearSign(puzzles[i]);}
            else if(i == 1) { letterPuzzleClear.ActiveClearSign(puzzles[i]);}
            else if(i == 2) { flowerPuzzleClear.ActiveClearSign(puzzles[i]); }
            else if(i == 3) { chessPuzzleClear.ActiveClearSign(puzzles[i]);}
            else if(i == 4) { bookSortPuzzleClear.ActiveClearSign(puzzles[i]);}
            else if(i == 5) { treePuzzleClear.ActiveClearSign(puzzles[i]);}
            else if(i == 6) { shieldPuzzleClear.ActiveClearSign(puzzles[i]);}
            else if(i == 7) { studioPuzzleClear.ActiveClearSign(puzzles[i]);}
            else if(i == 8) { taxidermyClear.ActiveClearSign(puzzles[i]);}
            /* else if(i == 9) { TODO: 접시에 사과 올리기 퍼즐 클리어 } */
            /* else if(i == 10) { TODO : 옷감 퍼즐 클리어 } */
            else if(i == 11) { potPuzzleClear.ActiveClearSign(puzzles[i]);}
            else if(i == 12) { butcherShop01Clear.ActiveClearSign(puzzles[i]);}
            /* else if(i == 13) { TODO : 듀토리얼 퍼즐 01 } */
            /* else if(i == 14) { TODO : 듀토리얼 퍼즐 02 } */
            else if(i == 15) { butcherShop02Clear.ActiveClearSign(puzzles[i]);}
            else if(i == 16) { butcherShop03Clear.ActiveClearSign(puzzles[i]);}
            else if(i == 17) { hiddenPuzzleClear.ActiveClearSign(puzzles[i]);}
        }
    }

    /// <summary>
    /// 획득한 총 별의 갯수를 설정해줍니다.
    /// </summary>
    public void InitAllStarCount()
    {
        int starCount = 0;

        foreach(bool _isClear in puzzles)
        {
            if(_isClear)
            {
                starCount++;
            }
        }

        // 획득한 총 별의 수 셋팅
        StarManager.starManager.getStarCount = starCount;
    }
}
