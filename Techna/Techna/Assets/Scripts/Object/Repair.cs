 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
    public bool maximum; // ��ü������ ���� (true - ��ü ������ ����, false - ���� ������ ȸ��)

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SuitManager gauge = GameObject.Find("SuitManager").GetComponent<SuitManager>();

            if (!maximum) // ������ ȸ�� ������
            {
                if(gauge.currnetGauge >= (gauge.maxGauge - 10)) // �ִ뷮 ���� �����ʵ���
                {
                    gauge.currnetGauge = 100;
                }
                else // ������ ȸ��
                {
                    gauge.currnetGauge += 10;
                }
            }
            else // �ִ뷮 ���� ������
            {
                gauge.maxGauge += 10;
                gauge.currnetGauge += 10;
            }

            Destroy(gameObject); // ȹ��� ������ ����
        }   
    }
}
