using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzlePoint : MonoBehaviour
{
    public BookPuzzle bookPuzzle;

    public GameObject book; // ����� å

    public bool active; // Ȱ��ȭ ����

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
        {
            if (other.gameObject == book) // ��ġ�� �´� å�� �ξ�����
            {
                active = true;

                bookPuzzle.puzzleNumCheck++; // �����Ȳ �� ����

                if (bookPuzzle.puzzleNumCheck == 4) // å 4���� ��� �˸��� ���� ������
                {
                    bookPuzzle.activate = true; // Ŭ����
                }
            }
        }
    }
}
