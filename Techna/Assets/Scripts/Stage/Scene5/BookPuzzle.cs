using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzle : MonoBehaviour
{
    public GameObject flask; // 클리어 보상 
    public bool activate; // 활성화 여부
    public int puzzleNumCheck; // 퍼즐 진행상황 체크

    public void Update()
    {
        if (activate)
        {
            flask.SetActive(true);
            gameObject.GetComponent<BookPuzzle>().enabled = false; // 스크립트 종료
        }
    }
}
