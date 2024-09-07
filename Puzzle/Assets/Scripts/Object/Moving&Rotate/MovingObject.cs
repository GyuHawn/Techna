using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public IMovingState currentState;
    private ICommand command;
    public ActivateGem gem; // 보석 확인

    // 이동관련
    public float moveNum; // 이동 거리
    public bool x; // x축 이동 여부
    public bool y; // y축 이동 여부
    public bool z; // z축 이동 여부

    public bool activated; // 그냥 이동 준비
    public GameObject checkObj; // 체크할 오브젝트

    public bool plateObj; // 발판 인지
    public bool lightObj; // 라이트 인지
    public bool electrictyObj; // 전기 인지
    public bool digitalLockObj; // 전자 잠금장치 인지
    public bool keyLockObj; // 키 잠금장치 인지

    private Vector3 currentPosition; // 초기 위치
    private Vector3 targetPosition; // 목표 위치
    public float moveDuration; // 도착 시간
    public bool isMoving = false; // 이동 중 여부

    // 물체이동 관련
    public GameObject movingObject; // 이동시킬 물체
    public Transform objectMovePos; // 이동시킬 위치

    public bool autoMoving; // 계속 이동 (반복 이동)
    public bool controlMoving; // 제어 이동 (한번만 움직이도록)
    public bool controlAutoMoving; // 특정위치 제어 반복 이동(특정위치로 반복이동)
    public bool objectMoving; // 물체 이동 (다른 오브젝트를 이동시키도록)

    private void Start()
    {
        currentState = new IdleState();
        currentPosition = transform.localPosition; // 초기 위치 저장
        targetPosition = CalculateTargetPosition(); // 목표 위치 설정
    }

    private void Update()
    {
        currentState.Update(this);
    }
    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    private void OnEnable()
    {
        if(gem != null)
        {
            gem.activationChanged += HandleGemActivated;
        }
    }

    private void OnDisable()
    {
        if(gem != null)
        {
            gem.activationChanged -= HandleGemActivated;
        }
    }

    private void HandleGemActivated(bool activate)
    {
        if (activate)
        {
            if (controlMoving)
            {
                StartMoving();
            }
            else if (objectMoving)
            {
                if (CheckObject())
                {
                    StartMoving();
                }
            }
            else if (autoMoving)
            {
                StartRepeatingMove();
            }
            else if (controlAutoMoving && CheckObject())
            {
                StartRepeatingMoveAtPosition(objectMovePos.position);
            }
        }
    }


    public void StartMoving()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject, targetPosition));
        }
        if (command != null)
        {
            command.Execute(this);
        }
    }

    // 다른 물체를 이동
    public void StartMovingObject()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(movingObject, objectMovePos.position));
        }
    }

    // 반복 이동 시작
    public void StartRepeatingMove()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(RepeatMove(targetPosition));
        }
    }

    // 특정 위치로 반복 이동 시작
    public void StartRepeatingMoveAtPosition(Vector3 position)
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(RepeatMove(position));
        }
    }

    // 특정 오브젝트의 활성화 상태 확인
    public bool CheckObject()
    {
        if (plateObj)
        {
            PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
            return plateFunction != null && plateFunction.activate;
        }
        else if (lightObj)
        {
            LightningRod lightingFunction = checkObj.GetComponent<LightningRod>();
            return lightingFunction != null && lightingFunction.activate;
        }
        else if (electrictyObj)
        {
            CheckElectricity electricityFunction = checkObj.GetComponent<CheckElectricity>();
            return electricityFunction != null && electricityFunction.activate;
        }
        else if (digitalLockObj)
        {
            DigitalLock digitalLockFunction = checkObj.GetComponent<DigitalLock>();
            return digitalLockFunction != null && digitalLockFunction.activate;
        }
        else if (keyLockObj)
        {
            KeyLock keyLockFunction = checkObj.GetComponent<KeyLock>();
            return keyLockFunction != null && keyLockFunction.activate;
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

    public IEnumerator MovePosition(GameObject obj, Vector3 targetPos) // 이동
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
