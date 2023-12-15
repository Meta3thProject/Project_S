using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSortTrigger : MonoBehaviour
{
    [SerializeField] private BookType bookType;

    private BookSortPuzzleClear bookSortPuzzleClear;
    private BookSortPuzzle bookSortPuzzle;

    private void Awake()
    {
        bookSortPuzzleClear = transform.parent.GetComponent<BookSortPuzzleClear>();
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
