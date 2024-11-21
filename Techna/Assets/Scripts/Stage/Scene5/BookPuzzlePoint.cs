using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzlePoint : MonoBehaviour
{
    public BookPuzzle bookPuzzle;

    public GameObject book;

    private void Awake()
    {
        bookPuzzle = GetComponent<BookPuzzle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == book)
        {
            bookPuzzle.puzzleNumCheck++;

            if(bookPuzzle.puzzleNumCheck == 4)
            {
                bookPuzzle.activate = true;
            }
        }
    }
}
