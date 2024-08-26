using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTracking : MonoBehaviour
{
    public GameObject player;

    private NavMeshAgent agent;  // 네비메시 에이전트

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);     
    }
}
