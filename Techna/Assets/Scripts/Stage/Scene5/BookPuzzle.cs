using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzle : MonoBehaviour
{
    public GameObject flask;
    public bool activate;
    public int puzzleNumCheck;

    public void Update()
    {
        if (activate)
        {
            flask.SetActive(true);
            gameObject.GetComponent<BookPuzzle>().enabled = false; // 스크립트 종료
        }
    }
}
