using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTracking : MonoBehaviour
{
    public DetectionSize boxSize;

    public GameObject player; // 플레이어

    public float wanderRadius = 20f; // 네비메시 범위 내에서 돌아다니는 반경
    public float wanderTimer = 5f; // 돌아다니는 타이머

    private NavMeshAgent agent;  // 네비메시 에이전트
    private float timer; // 타이머

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (boxSize == null)
        {
            boxSize = GameObject.Find("DetectionBox").GetComponent<DetectionSize>();
        }

        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        FindDetectionBox();
        timer = wanderTimer;
    }

    private void Update()
    {
        // 플레이어가 감지 범위 내에 있는지 확인
        if (IsPlayerInDetectionBox())
        {
            // 플레이어가 감지 범위 내에 있을 때
            //agent.SetDestination(player.transform.position);
        }
        else
        {
            // 플레이어가 감지 범위를 벗어났을 때
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                SetNewDestination();
                timer = 0f;
            }
        }
    }

    void FindDetectionBox() // 탐지 박스 찾기
    {
        if (boxSize == null)
        {
            boxSize = GameObject.Find("DetectionBox").GetComponent<DetectionSize>();
        }
    }

    private bool IsPlayerInDetectionBox()
    {
        // 정육면체 범위를 감지
        Vector3 center = boxSize.gameObject.transform.position; // detectionPosition을 중심으로 사용
        Vector3 min = center - boxSize.detectionBoxSize / 2;
        Vector3 max = center + boxSize.detectionBoxSize / 2;

        return player.transform.position.x >= min.x && player.transform.position.x <= max.x &&
               player.transform.position.y >= min.y && player.transform.position.y <= max.y &&
               player.transform.position.z >= min.z && player.transform.position.z <= max.z;
    }

    private void SetNewDestination()
    {
        // 네비메시 범위 내에서 랜덤한 위치로 이동
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        Vector3 finalPosition = hit.position;

        agent.SetDestination(finalPosition);
    }
}
