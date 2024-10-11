 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
    public bool maximum; // 전체량증가 여부 (true - 전체 게이지 증가, false - 현재 게이지 회복)

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
