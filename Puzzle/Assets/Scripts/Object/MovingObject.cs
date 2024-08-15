using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private ActivateGem controlGem;

    // 이동관련
    public float moveNum; // 이동 거리
    public bool x; // x축 이동 여부
    public bool y; // y축 이동 여부
    public bool z; // z축 이동 여부

    public GameObject gem; // 보석 확인

    public GameObject checkObj; // 체크할 오브젝트
    public bool plateObj; // 발판 인지
    public bool lightObj; // 라이트 인지
    public bool electrictyObj; // 전기 인지

    private Vector3 currentPosition; // 초기 위치
    private Vector3 targetPosition; // 목표 위치
    public float moveDuration; // 도착 시간
    private bool isMoving = false; // 이동 중 여부

    // 물체이동 관련
    public GameObject movingObject; // 이동시킬 물체
    public Transform objectMovePos; // 이동시킬 위치

    public bool autoMoving; // 계속 이동 (반복 이동)
    public bool controlMoving; // 제어 이동 (한번만 움직이도록)
    public bool controlAutoMoving; // 특정위치 제어 반복 이동(특정위치로 반복이동)
    public bool objectMoving; // 물체 이동 (다른 오브젝트를 이동시키도록)

    private void Start()
    {
        if(gem != null)
        {
            controlGem = gem.GetComponent<ActivateGem>();
        }

        currentPosition = transform.localPosition; // 초기 위치 저장
        targetPosition = CalculateTargetPosition(); // 목표 위치 설정
    }

    void Update()
    {
        // 제어이동, 보석o, 자신제어, 이동 중 여부
        if (gem != null && controlGem.activate && !isMoving)
        {
            if (controlMoving) // 제어 이동
            {
                isMoving = true;
                StartCoroutine(MovePosition(gameObject, targetPosition));
            }
            else if (objectMoving) // 물체 이동
            {
                if (objectMoving && gem != null && controlGem.activate && !isMoving)
                {
                    isMoving = true;
                    StartCoroutine(MovePosition(movingObject, objectMovePos.position));
                }
            }
        }

        // 물체이동, 오브젝트 체크, 이동 중 여부
        if(objectMoving && checkObj != null && !isMoving)
        {
            bool activate = CheckObject();

            if (activate)
            {
                isMoving = true;
                StartCoroutine(MovePosition(gameObject, targetPosition));
            }
        }

        // 반복이동, 이동 중 여부
        if (autoMoving && !isMoving)
        {     
            isMoving = true;
            StartCoroutine(RepeatMove(targetPosition));
        }

        // 특정위치 제어 반복 이동, 이동 중 여부
        if (controlAutoMoving && checkObj != null && !isMoving)
        {
            bool activate = CheckObject();
            
            if (activate)
            {
                isMoving = true;
                StartCoroutine(RepeatMove(objectMovePos.position));
            }
        }
    }

    bool CheckObject() // 특정 오브젝트의 활성화 상태 확인
    {
        if (plateObj)
        {
            PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                return plateFunction.activate;
            }
        }
        else if (lightObj)
        {
            LightningRod lightingFunction = checkObj.GetComponent<LightningRod>();
            if (lightingFunction != null)
            {
                return lightingFunction.activate;
            }
        }
        else if (electrictyObj)
        {
            CheckElectricity electricityFunction = checkObj.GetComponent<CheckElectricity>();
            if (electricityFunction != null)
            {
                return electricityFunction.activate;
            }
        }
        return false;
    }


    public void MoveObject() // 타스크립트 사용 이동코드
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject, targetPosition));
        }
    }

    private Vector3 CalculateTargetPosition() // 목표 위치 계산
    {
        return currentPosition + new Vector3(x ? moveNum : 0, y ? moveNum : 0, z ? moveNum : 0);
    }

    IEnumerator RepeatMove(Vector3 targetPosition) // 반복이동
    {
        while (true)
        {
            yield return MovePosition(gameObject, targetPosition);
            yield return MovePosition(gameObject, currentPosition);
        }
    }

    IEnumerator MovePosition(GameObject obj, Vector3 targetPos) // 이동
    {
        Vector3 startPosition = obj.transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration) // 천천히 이동
        {
            obj.transform.localPosition = Vector3.MoveTowards(startPosition, targetPos, (targetPos - startPosition).magnitude * (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localPosition = targetPos; // 목표 도착
        isMoving = false;
    }
}
