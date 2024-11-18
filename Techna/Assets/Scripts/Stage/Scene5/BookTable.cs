using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTable : MonoBehaviour
{
    public GameObject book;

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
