using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitManager : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Image gaugeBar; // ������ ��
    [Header("������")]
    public int maxGauge; // �ִ� ������
    public int currnetGauge; // ���� ������

    private bool isChangingGauge = false; // ������ ��ȭ ��

    [Header("������ ���� ����")]
    public bool progress; // ���� ���� ����

    void Start()
    {
        maxGauge = 100;
        currnetGauge = maxGauge;
    }

    void Update()
    {
        if (progress && !isChangingGauge)
        {
            GaugeUpdate(); // �������� ������Ʈ

            if (currnetGauge < maxGauge && !playerMovement.isMove) // ������ ������
            {
                StartCoroutine(IncreaseGauge()); // ������ ����
            }
            else if(currnetGauge > 0 && playerMovement.isMove) // �̵� ��
            {
                StartCoroutine(DecreaseGauge(1)); // ������ ����
            }
            else if (currnetGauge <= 0) // ������ 0 ���� �� ü�� ���� �� ������ �ҷ� ȸ��
            {
                playerMovement.currentHealth -= 5;
                currnetGauge += 5;
            }
        }
    }

    public IEnumerator DecreaseGauge(int num) // ������ ����
    {
        isChangingGauge = true;
        currnetGauge -= num;

        yield return new WaitForSeconds(1f);
        isChangingGauge = false;
    }

    IEnumerator IncreaseGauge() // ������ ����
    {
        isChangingGauge = true;
        currnetGauge++;

        yield return new WaitForSeconds(0.2f);
        isChangingGauge = false;
    }

    void GaugeUpdate()  // �������� ������Ʈ
    {
        float healthPercentage = (float)currnetGauge / maxGauge;
        gaugeBar.fillAmount = healthPercentage;
    }
}
