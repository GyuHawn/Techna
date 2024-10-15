using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCheckFunction : MonoBehaviour
{
    [Header("체크할 오브젝트")]
    public GameObject[] checkObjects; // 확인할 발판

    [Header("활성화 여부")]
    public bool activate; // 활성화 여부
    public bool on; // 오픈 여부 

    [Header("확인할 타입")]
    public bool plate; // 발판

    [Header("제어할 타입")]
    public bool obj; // 오브젝트
    public bool stairs; // 계단
    public bool controller; // 버튼

    [Header("제어할 오브젝트")]
    public GameObject targetObj;
    public bool target; // 자신인지

    void Start()
    {
        if (plate)
        {
            foreach (GameObject plate in checkObjects)
            {
                PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
                if (plateFunction != null)
                {
                    // 각 발판의 activationChanged 이벤트 구독, 활성화 상태 감지
                    plateFunction.activationChanged += CheckAllPlatesActivated;
                }
            }
        }
    }

    void OnDestroy()
    {
        if (plate)
        {
            foreach (GameObject plate in checkObjects)
            {
                PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
                if (plateFunction != null)
                {
                    // 이벤트 구독 해지
                    plateFunction.activationChanged -= CheckAllPlatesActivated;
                }
            }
        }
    }

    void Update()
    {
        CheckAllPlatesActivated(false);
    }

    void CheckAllPlatesActivated(bool dummy) // 모든 발판 활성화 시 문 열기
    {
        bool allPlatesActivated = true;

        if (plate)
        {
            foreach (GameObject plate in checkObjects) // 모든 발판 활성화 확인
            {
                PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
                if (plateFunction != null)
                {
                    if (!plateFunction.activate)
                    {
                        // 발판 비활성화 시 전체 상태 false
                        allPlatesActivated = false;
                        break;
                    }
                }
            }
        }

        if (allPlatesActivated) // 모든 발판 활성화 시 문 열기
        {
            activate = true;
            CheckObject();
        }
    }

    void CheckObject()
    {
        if (obj) // 문 - 문열기
        {
            if (!target)
            {
                MovingObject(gameObject);
            }
            else
            {
                MovingObject(targetObj);
            }
        }
        else if (stairs) // 계단 - 계단 활성화
        {
            ActivatedStairs();
        }
        else if (controller)
        {
            ActivatedController();
        }
    }

    void ActivatedStairs() // 계단 활성화
    {
        if (!on)
        {
            on = true;
            ActivatedStairs stairsObj = gameObject.GetComponent<ActivatedStairs>();
            stairsObj.activated = true;
        }
    }
    
    void ActivatedController()
    {
        if (!on)
        {
            on = true;
            ActivatedController buttonsObj = gameObject.GetComponent<ActivatedController>();
            buttonsObj.activated = true;
        }
    }

    void MovingObject(GameObject obj) // 오브젝트 이동
    {
        activate = false;
        if (!on)
        {
            on = true;        
            MovingObject move = obj.GetComponent<MovingObject>();
            move.activated = true;
        }
    }
}
