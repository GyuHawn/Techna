using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTable : MonoBehaviour
{
    public GameObject book; // ��Ʈ å UI


    // ��ó�� ������ ��Ʈ ���� / �־����� ����
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            book.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            book.SetActive(false);
        }
    }
}
