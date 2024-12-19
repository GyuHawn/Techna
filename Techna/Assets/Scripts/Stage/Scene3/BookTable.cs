using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTable : MonoBehaviour
{
    public GameObject book; // 힌트 책 UI


    // 근처에 있을때 힌트 오픈 / 멀어지면 오프
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
