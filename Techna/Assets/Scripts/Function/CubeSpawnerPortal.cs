using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnerPortal : MonoBehaviour
{
    public GameObject portalEffect;
    public GameObject spawnerDoor; // ������ �Ա�
    public GameObject checkObj; // �۵� ���� ������Ʈ(���� ��..)
    
    public bool open; // Ȱ��ȭ ����

    public bool plateObj;
    public bool lightObj;
    public bool lEDLine;

    void Start()
    {
        if (plateObj)
        {
            PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                // �� ������ activationChanged �̺�Ʈ ����, Ȱ��ȭ ���� ����
                plateFunction.activationChanged += OpenSpawnerCheck;
            }
        }
        else if (lightObj)
        {
            LightningRod lightingFunction = checkObj.GetComponent<LightningRod>();
            if (lightingFunction != null)
            {
                // �� ������ activationChanged �̺�Ʈ ����, Ȱ��ȭ ���� ����
                lightingFunction.activationChanged += OpenSpawnerCheck;
            }
        }
        else if (lEDLine)
        {
            LEDNode line = checkObj.GetComponent<LEDNode>();
            if(line != null)
            {
                line.activationChanged += OpenSpawnerCheck;
            }
        }
    }
    
    void Update()
    {
        OpenSpawnerCheck(false); // ������ ���� Ȯ��
    }

    void OpenSpawnerCheck(bool dummy)
    {
        bool activated = true;

        if (plateObj)
        {
            PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
            if (!plateFunction.activate)
            {
                // ���� ��Ȱ��ȭ �� ��ü ���� false
                activated = false;
            }
        }
        else if(lightObj)
        {
            LightningRod lightingFunction = checkObj.GetComponent<LightningRod>();
            if (!lightingFunction.activate)
            {
                // ���� ��Ȱ��ȭ �� ��ü ���� false
                activated = false;
            }
        }
        else if(lEDLine)
        {
            LEDNode line = checkObj.GetComponent<LEDNode>();
            if (!line.activate)
            {
                // ���� ��Ȱ��ȭ �� ��ü ���� false
                activated = false;
            }
        }

        if (activated) // ����
        {
            open = true;
            Open();
        }
        
    }

    void Open()
    {
        if(portalEffect != null)
        {
            portalEffect.SetActive(true);
        }
        spawnerDoor.SetActive(false);
    }
}
