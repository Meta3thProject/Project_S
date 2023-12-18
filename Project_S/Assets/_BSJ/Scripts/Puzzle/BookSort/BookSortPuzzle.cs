using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BookType
{
    Book01, Book02, Book03, Book04
}

public class BookSortPuzzle : MonoBehaviour
{
    [field: SerializeField] public BookType BookType { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void EnterBookTrigger()
    {
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
    }
}