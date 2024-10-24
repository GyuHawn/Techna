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

            if (!maximum) // 게이지 회복 아이템
            {
                if(gauge.currnetGauge >= (gauge.maxGauge - 10)) // 최대량 보다 넘지않도록
                {
                    gauge.currnetGauge = 100;
                }
                else // 게이지 회복
                {
                    gauge.currnetGauge += 10;
                }
            }
            else // 최대량 증가 아이템
            {
                gauge.maxGauge += 10;
                gauge.currnetGauge += 10;
            }

            Destroy(gameObject); // 획득시 아이템 삭제
        }   
    }
}
