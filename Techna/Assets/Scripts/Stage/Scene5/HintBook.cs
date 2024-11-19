using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBook : MonoBehaviour
{
    public GameObject QuillPen; // ±êÆæ
    public GameObject ink; // À×Å©
    public int hintNum;

    public GameObject hint; // ÈùÆ®

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == QuillPen)
        {
            Destroy(QuillPen);
            hintNum++;

            HintOpen();
        }
        if (other.gameObject == ink)
        {
            Destroy(ink);
            hintNum++;

            HintOpen();
        }
    }

    void HintOpen() // Ææ°ú À×Å©¸¦ ¸ðµÎ ¸ðÀ»½Ã ÈùÆ® È°¼ºÈ­
    {
        if(hintNum == 2)
        {
            hint.SetActive(true);
        }
    }
}
