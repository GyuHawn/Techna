using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CharacterController controller;

    public GameObject player; // 플레이어

    public GameObject[] stageSetting; // 스테이지 값 설정

    private int previousStage; // 이전 스테이지 값 저장

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        controller = player.GetComponent<CharacterController>();
    }

    void Start()
    {
        previousStage = playerMovement.currentStage; // 스테이지 값 초기화
    }

    void Update()
    {
        if (playerMovement.currentStage != previousStage)
        {
            NextStageSetting(); // 스테이지에 따른 플레이어 스테이지 값 설정 오브젝트 
            previousStage = playerMovement.currentStage; // 이전 스테이지 값 갱신
        }
    }

    void NextStageSetting() // 다음 스테이지 이동시 스테이지 세팅
    {
        switch (playerMovement.currentStage)
        {
            case 2:
                MovePlayer(new Vector3(-400f, 4, -375), Quaternion.Euler(0, 180, 0));
                playerMovement.currentStage = 0;
                stageSetting[0].SetActive(false);
                stageSetting[1].SetActive(true);
                break;
             // 다음 스테이지 추가 시 값 추가
        }
    }

    void MovePlayer(Vector3 position, Quaternion rotation)
    {
        if (controller != null)
        {
            controller.enabled = false;
            player.transform.position = position;
            player.transform.rotation = rotation;
            controller.enabled = true;
        }
    }
}
