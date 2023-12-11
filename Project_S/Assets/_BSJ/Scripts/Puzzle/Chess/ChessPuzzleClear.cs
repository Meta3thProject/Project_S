using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPuzzleClear : MonoBehaviour
{
    [field: SerializeField]
    public int[] clearCheck { get; private set; }

    private void Awake()
    {
        clearCheck = new int[3] { 0, 0, 0 }; 
    }

    public void IncreaseClearCheck(int _indexNumber)
    {
        clearCheck[_indexNumber] = 1;
        CheckClearArray();
    }

    public void DecreaseClearCheck(int _indexNumber)
    {
        clearCheck[_indexNumber] = 0;
    }

    private void CheckClearArray()
    {
        foreach (int i in clearCheck) 
        {
            if (i == 0)
            {
                return;
            }
        }

        Debug.Log("클리어!");
    }
}
