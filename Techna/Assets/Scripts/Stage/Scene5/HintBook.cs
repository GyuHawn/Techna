using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBook : MonoBehaviour
{
    public GameObject QuillPen; // ����
    public GameObject ink; // ��ũ
    public int hintNum;

    public GameObject hint; // ��Ʈ

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

    void HintOpen() // ��� ��ũ�� ��� ������ ��Ʈ Ȱ��ȭ
    {
        if(hintNum == 2)
        {
            hint.SetActive(true);
        }
    }
}
