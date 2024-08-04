using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private ControlGem controlGem;

    // 이동관련
    public float moveNum; // 이동 거리
    public bool x; // x축 이동 여부
    public bool y; // y축 이동 여부
    public bool z; // z축 이동 여부

    public GameObject gem;

    private Vector3 currentPosition; // 초기 위치
    private Vector3 targetPosition; // 목표 위치
    public float moveDuration; // 도착 시간
    private bool isMoving = false; // 이동 중 여부

    // 물체이동 관련
    public GameObject movingObject; // 이동시킬 물체
    public Transform objectMovePos; // 이동시킬 위치

    public bool autoMoving; // 계속 이동 (반복적인 움직임)
    public bool controlMoving; // 제어 이동 (한번만 움직이도록)
    public bool objectMoving; // 물체 이동 (다른 오브젝트를 이동시키도록)

    private void Start()
    {
        if(gem != null)
        {
            controlGem = gem.GetComponent<ControlGem>();
        }

        currentPosition = transform.localPosition; // 초기 위치 저장
        targetPosition = CalculateTargetPosition(); // 목표 위치 설정
    }

    void Update()
    {
        // 제어이동, 보석o, 자신제어, 이동 중 여부
        if (controlMoving && gem != null && controlGem.onControl && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject, targetPosition, false));
        }
        // 물체이동, 보석o, 물체제어, 이동 중 여부
        if (objectMoving && gem != null && controlGem.onControl && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(movingObject, objectMovePos.position, false));
        }
        // 반복이동, 이동 중 여부
        if (autoMoving && !isMoving)
        {     
            isMoving = true;
            StartCoroutine(RepeatMove());
        }
    }

    public void MoveObject() // 타스크립트 사용 이동코드
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject,targetPosition, false));
        }
    }

    private Vector3 CalculateTargetPosition() // 목표 위치 계산
    {
        Vector3 target = currentPosition;

        if (x) // x방향으로 이동
        {
            target.x += moveNum;
        }
        if (y) // y방향으로 이동 
        {
            target.y += moveNum;
        }
        if (z) // z방향으로 이동
        {
            target.z += moveNum;
        }

        return target;
    }

    IEnumerator RepeatMove() // 반복이동
    {
        while (true)
        {
            yield return MovePosition(gameObject, targetPosition, false);
            yield return MovePosition(gameObject, currentPosition, false);
        }
    }

    IEnumerator MovePosition(GameObject obj, Vector3 targetPos, bool resetMovingFlag) // 이동
    {
        Vector3 startPosition = Vector3.zero;

        // 시작위치 설정
        if (autoMoving || controlMoving)
        {
            startPosition = transform.localPosition; 
        }
        else if (objectMoving)
        {
            startPosition = obj.transform.localPosition;
        }
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration) // 천천히 이동
        {
            obj.transform.localPosition = Vector3.MoveTowards(startPosition, targetPos, (targetPos - startPosition).magnitude * (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localPosition = targetPos; // 목표 도착

        if (resetMovingFlag) // 이동 여부
        {
            isMoving = false;
        }
    }
}
