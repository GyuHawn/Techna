using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitManager : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Image gaugeBar;
    public int maxGauge;
    public int currnetGauge;

    private bool isChangingGauge = false; // 게이지 변화 중

    public bool progress; // 진행 가능 여부

    void Start()
    {
        maxGauge = 100;
        currnetGauge = maxGauge;
    }

    void Update()
    {
        if (progress && !isChangingGauge)
        {
            GaugeUpdate(); // 게이지바 업데이트

            if (currnetGauge < maxGauge && !playerMovement.isMove)
            {
                StartCoroutine(IncreaseGauge()); // 게이지 증가
            }
            else if(currnetGauge > 0 && playerMovement.isMove)
            {
                StartCoroutine(DecreaseGauge(1)); // 게이지 감소
            }
            else if (currnetGauge <= 0) // 게이지 0 도달 시 체력 감소 후 게이지 소량 회복
            {
                playerMovement.currentHealth -= 5;
                currnetGauge += 5;
            }
        }
    }

    public IEnumerator DecreaseGauge(int num) // 게이지 감소
    {
        isChangingGauge = true;
        currnetGauge -= num;

        yield return new WaitForSeconds(1f);
        isChangingGauge = false;
    }

    IEnumerator IncreaseGauge() // 게이지 증가
    {
        isChangingGauge = true;
        currnetGauge++;

        yield return new WaitForSeconds(0.2f);
        isChangingGauge = false;
    }

    void GaugeUpdate()  // 게이지바 업데이트
    {
        float healthPercentage = (float)currnetGauge / maxGauge;
        gaugeBar.fillAmount = healthPercentage;
    }
}
