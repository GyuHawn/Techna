using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzle : MonoBehaviour
{
    public GameObject flask; // Ŭ���� ���� 
    public bool activate; // Ȱ��ȭ ����
    public int puzzleNumCheck; // ���� �����Ȳ üũ

    public void Update()
    {
        if (activate)
        {
            flask.SetActive(true);
            gameObject.GetComponent<BookPuzzle>().enabled = false; // ��ũ��Ʈ ����
        }
    }
}
