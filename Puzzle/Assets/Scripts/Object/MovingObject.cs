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

    private Vector3 crruntPosition; // 초기 위치
    private Vector3 targetPosition; // 목표 위치
    public float moveDuration; // 도착 시간
    private bool isMoving = false; // 이동 중 여부

    private void Start()
    {
        controlGem = gem.GetComponent<ControlGem>();

        crruntPosition = transform.localPosition; // 초기 위치 저장
        targetPosition = CalculateTargetPosition(); // 목표 위치 설정
    }

    void Update()
    {
        if (controlGem.onControl && !isMoving)
        {
            isMoving = true;
            StartCoroutine(Moving());
        }
    }

    private Vector3 CalculateTargetPosition() // 목표 위치 계산
    {
        Vector3 target = crruntPosition;

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

    IEnumerator Moving() // 이동
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 move = targetPosition - startPosition;
        Vector3 movePerSecond = move / moveDuration; // 3초 동안 이동하도록 속도 설정

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, movePerSecond.magnitude * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition; // 목표 도착
        isMoving = false;
    }
}
