using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckElectricity : MonoBehaviour
{
    public GameObject[] electricityGems;
    public bool activate; // 활성화 여부

    void Start()
    {
        foreach (GameObject check in electricityGems)
        {
            ActivateGem electricity = check.GetComponent<ActivateGem>();
            if (electricity != null)
            {
                // 각 발판의 activationChanged 이벤트 구독, 활성화 상태 감지
                electricity.activationChanged += CheckElectricityActivated;
            }
        }
    }
    void OnDestroy()
    {
        foreach (GameObject check in electricityGems)
        {
            ActivateGem electricity = check.GetComponent<ActivateGem>();
            if (electricity != null)
            {
                // 이벤트 구독 해지
                electricity.activationChanged -= CheckElectricityActivated;
            }
        }
    }

    void Update()
    {
        CheckElectricityActivated(false);
    }

    void CheckElectricityActivated(bool dummy) // 모든 발판 활성화 시 문 열기
    {
        bool allElectricityActivated = true;

        foreach (GameObject check in electricityGems) // 모든 발판 활성화 확인
        {
            ActivateGem electricity = check.GetComponent<ActivateGem>();
            if (electricity != null)
            {
                if (!electricity.activate)
                {
                    // 발판 비활성화 시 전체 상태 false
                    allElectricityActivated = false;
                    break;
                }
            }
        }

        if (allElectricityActivated) // 모든 발판 활성화 시 문 열기
        {
            activate = true;
        }
    }
}
