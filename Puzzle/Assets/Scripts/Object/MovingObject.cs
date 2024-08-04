using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private ControlGem controlGem;

    public float moveNum; // 이동 거리
    public bool x; // x축 이동 여부
    public bool y; // y축 이동 여부
    public bool z; // z축 이동 여부

    public GameObject gem;

    private Vector3 currentPosition; // 초기 위치
    private Vector3 targetPosition; // 목표 위치
    public float moveDuration; // 도착 시간
    private bool isMoving = false; // 이동 중 여부

    public bool autoMoving; // 계속 이동
    public bool controlMoving; // 제어 이동

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
        // 제어이동, 보석o, 제어 확인, 이동 중 여부
        if (controlMoving && gem != null && controlGem.onControl && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(targetPosition, false));
        }
        // 반복이동, 이동 중 여부
        if (autoMoving && !isMoving)
        {     
            isMoving = true;
            StartCoroutine(RepeatMove());
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
            yield return MovePosition(targetPosition, false);
            yield return MovePosition(currentPosition, false);
        }
    }

    IEnumerator MovePosition(Vector3 targetPos, bool resetMovingFlag) // 이동
    {
        Vector3 startPosition = transform.localPosition; // 시작위치 설정
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration) // 천천히 이동
        {
            transform.localPosition = Vector3.MoveTowards(startPosition, targetPos, (targetPos - startPosition).magnitude * (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos; // 목표 도착

        if (resetMovingFlag) // 이동 여부
        {
            isMoving = false;
        }
    }
}
