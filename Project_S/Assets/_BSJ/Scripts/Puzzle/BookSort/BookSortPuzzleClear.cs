using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSortPuzzleClear : MonoBehaviour
{
    const int PUZZLE_COUNT = 4;

    [field: SerializeField]
    public int[] clearCheck { get; private set; }

    [SerializeField] bool isClear = false; 

    private ParticleSystem particle;

    private void Awake()
    {
        clearCheck = new int[PUZZLE_COUNT] { 0, 0, 0, 0 };
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
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

        if (isClear == false)
        {
            isClear = true;
            particle.Play();
            StartCoroutine(StarManager.starManager.CallStar());
            Debug.Log("북 퍼즐 클리어");
        }
        
    }
}
