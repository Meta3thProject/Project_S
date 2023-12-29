using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSortTrigger : MonoBehaviour
{
    // 트리거의 책 타입
    [SerializeField] private BookType bookType;

    // 책 스크립트
    private BookSortPuzzle bookSortPuzzle;

    // 퍼즐 클리어 스크립트
    private BookSortPuzzleClear bookSortPuzzleClear;

    private void Awake()
    {
        // 이 퍼즐의 번호는 4번입니다.
        bookSortPuzzleClear = transform.root.GetChild(4).GetComponent<BookSortPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BookSortPuzzle>() != null)
        {
            bookSortPuzzle = other.GetComponent<BookSortPuzzle>();

            if (bookSortPuzzle.BookType == bookType)
            {
                bookSortPuzzle.EnterBookTrigger();
                bookSortPuzzleClear.IncreaseClearCheck((int)bookType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BookSortPuzzle>() != null)
        {
            bookSortPuzzle = other.GetComponent<BookSortPuzzle>();

            if (bookSortPuzzle.BookType == bookType)
            {
                bookSortPuzzleClear.DecreaseClearCheck((int)bookType);
            }
        }
    }
}
