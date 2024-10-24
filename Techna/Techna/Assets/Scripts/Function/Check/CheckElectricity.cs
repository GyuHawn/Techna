using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckElectricity : MonoBehaviour
{
    public GameObject[] electricityGems;
    public bool activate; // Ȱ��ȭ ����

    void Start()
    {
        foreach (GameObject check in electricityGems)
        {
            ActivateGem electricity = check.GetComponent<ActivateGem>();
            if (electricity != null)
            {
                // �� ������ activationChanged �̺�Ʈ ����, Ȱ��ȭ ���� ����
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
                // �̺�Ʈ ���� ����
                electricity.activationChanged -= CheckElectricityActivated;
            }
        }
    }

    void Update()
    {
        CheckElectricityActivated(false);
    }

    void CheckElectricityActivated(bool dummy) // ��� ���� Ȱ��ȭ �� �� ����
    {
        bool allElectricityActivated = true;

        foreach (GameObject check in electricityGems) // ��� ���� Ȱ��ȭ Ȯ��
        {
            ActivateGem electricity = check.GetComponent<ActivateGem>();
            if (electricity != null)
            {
                if (!electricity.activate)
                {
                    // ���� ��Ȱ��ȭ �� ��ü ���� false
                    allElectricityActivated = false;
                    break;
                }
            }
        }

        if (allElectricityActivated) // ��� ���� Ȱ��ȭ �� �� ����
        {
            activate = true;
        }
    }
}
