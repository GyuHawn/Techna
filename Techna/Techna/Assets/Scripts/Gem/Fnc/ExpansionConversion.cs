using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionConversion : MonoBehaviour
{
    public GemCombination gem; // ���� ���� ���� Ȯ�ο�
    public GameObject[] state; // ���� ������Ʈ
    public bool plus; // ���� ����

    void Start()
    {
        plus = true;
        SetState(plus); // �ʱ� ���� ����
    }

    void Update()
    {
        // ���� ���°� Ȯ�� ����(1.3f)�� ���� ���� ��ȯ ����
        if (gem.currentGemNum == 1.3f && Input.GetButtonDown("Expansion")) //
        {
            plus = !plus; // ���¸� ����
            SetState(plus); // ���� ����
        }
    }

    // ���¿� ���� ������Ʈ Ȱ��/��Ȱ��ȭ
    private void SetState(bool isPlus)
    {
        state[0].SetActive(isPlus);
        state[1].SetActive(!isPlus);
    }
}
