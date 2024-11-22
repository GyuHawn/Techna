using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzlePoint : MonoBehaviour
{
    public BookPuzzle bookPuzzle;

    public GameObject book;

    public bool active;

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
        {
            if (other.gameObject == book)
            {
                active = true;

                bookPuzzle.puzzleNumCheck++;

                if (bookPuzzle.puzzleNumCheck == 4)
                {
                    bookPuzzle.activate = true;
                }
            }
        }
    }
}
