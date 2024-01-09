using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPuzzleTrigger : MonoBehaviour
{
    private CatPuzzleClear catPuzzleClear;
    private CatPuzzle catPuzzle;

    private void Awake()
    {
        // 이 퍼즐의 인덱스는 22번입니다.
        catPuzzleClear = transform.root.GetChild(22).GetComponent<CatPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CatPuzzle>() != null)
        {
            catPuzzle = other.GetComponent<CatPuzzle>();
            catPuzzle.transform.position = new Vector3(transform.position.x, catPuzzle.transform.position.y - 0.07f, transform.position.z);
            catPuzzle.EnterCatTrigger();
            catPuzzleClear.CheckClear();
        }
    }
}
