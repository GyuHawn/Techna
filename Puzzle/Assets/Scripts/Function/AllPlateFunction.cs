using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlateFunction : MonoBehaviour
{
    public GameObject[] plates; // 확인할 발판

    public bool activate; // 활성화 여부
    public bool open; // 오픈 여부 

    public bool door; // 문
    public bool stairs; // 계단

    void Start()
    {
        foreach (GameObject plate in plates)
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                // 각 발판의 activationChanged 이벤트 구독, 활성화 상태 감지
                plateFunction.activationChanged += CheckAllPlatesActivated;
            }
        }
    }

    void OnDestroy()
    {
        foreach (GameObject plate in plates)
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                // 이벤트 구독 해지
                plateFunction.activationChanged -= CheckAllPlatesActivated;
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

        foreach (GameObject plate in plates) // 모든 발판 활성화 확인
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

        if (allPlatesActivated) // 모든 발판 활성화 시 문 열기
        {
            activate = true;
            CheckObject();
        }
    }

    void CheckObject()
    {
        if (door) // 문 - 문열기
        {
            OpenDoor();
        }
        else if (stairs) // 계단 - 계단 활성화
        {
            ActivatedStairs();
        }
    }

    void ActivatedStairs() // 계단 활성화
    {
        if (!open)
        {
            open = true;
            ActivatedStairs stairsObj = gameObject.GetComponent<ActivatedStairs>();
            stairsObj.activated = true;
        }
    }

    void OpenDoor() // 문 열기
    {
        activate = false;
        if (!open)
        {
            open = true;
            StartCoroutine(MovingDoor());
        }

        IEnumerator MovingDoor()
        {
            float elapsedTime = 0f;
            float duration = 3f;
            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + new Vector3(0, 16, 0);

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
        }
    }
}
