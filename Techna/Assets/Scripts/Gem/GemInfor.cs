using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    public GemManager gemManager;

    [Header("����")]
    public string infor; // ���� 
    public TMP_Text inforText; // ���� �ؽ�Ʈ

    [Header("�ؽ�Ʈ ����")]
    public int textSize; // �ؽ�Ʈ ������
    public Color textColor; // �� 
    public bool showUI; // ǥ�� �� ����

    [Header("���� or ����")]
    public bool gem; // ���� ����
    public bool gemInfor; // ���� ���� ����

    private void Start()
    {
        FindNullObject(); // ���� ������Ʈ ã��

        // �ٹٲ� �� ���� ����
        if (!string.IsNullOrEmpty(infor))
        {
            infor = infor.Replace(@"\n", "\n");
        }
    }

    void FindNullObject() // ���� ������Ʈ ã��
    {
        if (gemManager == null)
        {
            gemManager = FindObjectOfType<GemManager>();
        }

        if (gemInfor)
        {
            if (inforText == null)
            {
                inforText = GameObject.Find("GemInforText").GetComponent<TMP_Text>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gemInfor)
            {
                if (!showUI)
                {
                    showUI = true;
                    DisplayInfoUI(); // ���� ǥ��
                }
            }

            if (gem)
            {
                gemManager.CollectGem(gameObject.name); // ���� �̸� Ȯ��
                Destroy(gameObject); // ���� ����
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gemInfor && showUI)
        {
            showUI = false; 
            HideInfoUI(); // ���� �����
        }
    }

    private void DisplayInfoUI() // ���� ǥ��
    {
        inforText.color = textColor; // ���� ����
        inforText.text = infor;
        inforText.fontSize = textSize;
    }

    private void HideInfoUI() // ���� �����
    {
        inforText.color = Color.white;
        inforText.text = "";
    }
}
