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
            if (!maximum)
            {
                if(gauge.currnetGauge >= 90)
                {
                    gauge.currnetGauge = 100;
                }
                else
                {
                    gauge.currnetGauge += 10;
                }
            }
            else
            {
                gauge.maxGauge += 10;
                gauge.currnetGauge += 10;
            }

            Destroy(gameObject);
        }   
    }
}
