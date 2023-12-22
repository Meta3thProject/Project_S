using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotPuzzleClear : MonoBehaviour
{
    [field: SerializeField] public bool isClear { get; private set; }

    public void PuzzleClear()
    {
        isClear = true;
        StartCoroutine(StarManager.starManager.CallStar());
        Debug.Log("클리어!");
    }

    public void CheckClear()
    {
        if (isClear == false)
        {
            PuzzleClear();
        }
    }
}
