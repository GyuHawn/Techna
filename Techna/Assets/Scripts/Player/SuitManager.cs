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

    private bool isDecreasingGauge = false; // 코루틴이 실행 중인지 확인하는 플래그

    void Start()
    {
        maxGauge = 100;
        currnetGauge = maxGauge;
    }

    void Update()
    {
        GaugeUpdate(); // 게이지바 업데이트

        if (currnetGauge > 0 && !isDecreasingGauge)
        {
            StartCoroutine(DecreaseGauge()); // 초당 게이지 감소
        }
        else if (currnetGauge <= 0) // 게이지 0 도달 시 체력 감소 후 게이지 소량 회복
        {
            playerMovement.currentHealth -= 5;
            currnetGauge += 5;
        }
    }

    IEnumerator DecreaseGauge()
    {
        isDecreasingGauge = true;
        yield return new WaitForSeconds(1f);

        currnetGauge--;
        isDecreasingGauge = false;
    }

    void GaugeUpdate()  // 게이지바 업데이트
    {
        float healthPercentage = (float)currnetGauge / maxGauge;
        gaugeBar.fillAmount = healthPercentage;
    }
}
