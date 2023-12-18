using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePuzzleClear : MonoBehaviour
{
    const int TREEPUZZLECOUNT = 3;

    [SerializeField] private GameObject[] trees = new GameObject[TREEPUZZLECOUNT];

    [field: SerializeField] public int[] clearCheck { get; private set; }

    [field: SerializeField] public bool isClear { get; private set; }

    private void Awake()
    {
        // 나무 오브젝트 캐싱
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i] = transform.GetChild(i).gameObject;
        }

        clearCheck = new int[3] { 0, 0, 0 };
        isClear = false;
    }

    public void IncreaseClearCheck(int _indexNumber)
    {
        clearCheck[_indexNumber] = 1;
        CheckClearArray(_indexNumber, TREEPUZZLECOUNT);
    }

    public void DecreaseClearCheck(int _indexNumber)
    {
        clearCheck[_indexNumber] = 0;
    }

    public void PuzzleClear()
    {
        isClear = true;
        StartCoroutine(StarManager.starManager.CallStar());
        Debug.Log("나무 퍼즐 클리어!");
    }

    private void CheckClearArray(int _BrokenTreeNumber, int _puzzleCount)
    {
        Debug.Log($"_BrokenTreeNumber : {_BrokenTreeNumber}");
        int clearCount = 0;

        // 0번부터 순차적으로 나무가 깨지지 않았다면 나무 재생성.
        for(int i = 0; i < _BrokenTreeNumber + 1; i++)
        {
            Debug.Log("for 문 들어옴");
            if (clearCheck[i] == 0)
            {
                Debug.Log("if 문 들어옴");
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
                Debug.Log(clearCount);
            }
        }

        // 클리어!
        if (clearCount == _puzzleCount && isClear == false)
        {
            PuzzleClear();
        }
    }
}
