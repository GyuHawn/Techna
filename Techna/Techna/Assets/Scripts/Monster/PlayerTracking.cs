using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTracking : MonoBehaviour
{
    public DetectionSize boxSize;

    public GameObject player; // �÷��̾�
    
    public float wanderRadius = 20f; // �׺�޽� ���� ������ ���ƴٴϴ� �ݰ�
    public float wanderTimer = 5f; // ���ƴٴϴ� Ÿ�̸�

    private NavMeshAgent agent;  // �׺�޽� ������Ʈ
    private float timer; // Ÿ�̸�

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

        timer = wanderTimer;
        SetNewDestination();
    }

    private void Update()
    {
        FindDetectionBox();

        // �÷��̾ ���� ���� ���� �ִ��� Ȯ��
        if (IsPlayerInDetectionBox())
        {
            // �÷��̾ ���� ���� ���� ���� ��
            agent.SetDestination(player.transform.position);
        }
        else
        {
            // �÷��̾ ���� ������ ����� ��
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                SetNewDestination();
                timer = 0f;
            }
        }
    }

    void FindDetectionBox() // Ž�� �ڽ� ã��
    {
        if (boxSize == null)
        {
            boxSize = GameObject.Find("DetectionBox").GetComponent<DetectionSize>();
        }
    }

    private bool IsPlayerInDetectionBox()
    {
        // ������ü ������ ����
        Vector3 center = boxSize.gameObject.transform.position; // detectionPosition�� �߽����� ���
        Vector3 min = center - boxSize.detectionBoxSize / 2;
        Vector3 max = center + boxSize.detectionBoxSize / 2;

        return player.transform.position.x >= min.x && player.transform.position.x <= max.x &&
               player.transform.position.y >= min.y && player.transform.position.y <= max.y &&
               player.transform.position.z >= min.z && player.transform.position.z <= max.z;
    }

    private void SetNewDestination()
    {
        // �׺�޽� ���� ������ ������ ��ġ�� �̵�
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        Vector3 finalPosition = hit.position;

        agent.SetDestination(finalPosition);
    }

   
}
