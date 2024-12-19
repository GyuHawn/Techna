using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzlePoint : MonoBehaviour
{
    public BookPuzzle bookPuzzle;

    public GameObject book; // 사용할 책

    public bool active; // 활성화 여부

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
        {
            if (other.gameObject == book) // 위치에 맞는 책을 두었을때
            {
                active = true;

                bookPuzzle.puzzleNumCheck++; // 진행상황 값 변경

                if (bookPuzzle.puzzleNumCheck == 4) // 책 4권이 모두 알맞은 곳에 있을때
                {
                    bookPuzzle.activate = true; // 클리어
                }
            }
        }
    }
}
