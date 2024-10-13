using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnerPortal : MonoBehaviour
{
    public GameObject portalEffect;
    public GameObject spawnerDoor; // 스포너 입구
    public GameObject checkObj; // 작동 실행 오브젝트(발판 등..)
    
    public bool open; // 활성화 여부

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
                // 각 발판의 activationChanged 이벤트 구독, 활성화 상태 감지
                plateFunction.activationChanged += OpenSpawnerCheck;
            }
        }
        else if (lightObj)
        {
            LightningRod lightingFunction = checkObj.GetComponent<LightningRod>();
            if (lightingFunction != null)
            {
                // 각 발판의 activationChanged 이벤트 구독, 활성화 상태 감지
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
        OpenSpawnerCheck(false); // 스포너 오픈 확인
    }

    void OpenSpawnerCheck(bool dummy)
    {
        bool activated = true;

        if (plateObj)
        {
            PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
            if (!plateFunction.activate)
            {
                // 발판 비활성화 시 전체 상태 false
                activated = false;
            }
        }
        else if(lightObj)
        {
            LightningRod lightingFunction = checkObj.GetComponent<LightningRod>();
            if (!lightingFunction.activate)
            {
                // 발판 비활성화 시 전체 상태 false
                activated = false;
            }
        }
        else if(lEDLine)
        {
            LEDNode line = checkObj.GetComponent<LEDNode>();
            if (!line.activate)
            {
                // 발판 비활성화 시 전체 상태 false
                activated = false;
            }
        }

        if (activated) // 오픈
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
