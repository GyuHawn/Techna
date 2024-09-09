using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject player; // 플레이어

    public GameObject[] fullStage; // 전체 스테이지

    public GameObject[] stageSetting; // 스테이지 값 설정

    private int previousStage; // 이전 스테이지 값 저장

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();    
    }

    void Start()
    {
        previousStage = playerMovement.currentStage; // 스테이지 값 초기화
        UpdateStage(); // 초기 스테이지 설정
    }

    void Update()
    {
        if (playerMovement.currentStage != previousStage)
        {
            UpdateStage(); // 플레이어 위치에 따른 스테이지 비/활성화
            NextStageSetting(); // 스테이지에 따른 플레이어 스테이지 값 설정 오브젝트 
            previousStage = playerMovement.currentStage; // 이전 스테이지 값 갱신
        }
    }

    void NextStageSetting() // 다음 스테이지 이동시 스테이지 세팅
    {
        if (playerMovement.currentStage == 5)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                Vector3 newPosition = new Vector3(1.5f, 4, 25);
                Quaternion newRotation = Quaternion.Euler(0, 180, 0);
                controller.enabled = false;
                player.transform.position = newPosition;
                player.transform.rotation = newRotation;
                controller.enabled = true; 
            }

            playerMovement.currentStage = 6;
            stageSetting[0].SetActive(false);
            stageSetting[1].SetActive(true);
        }
    }


    void UpdateStage() // 플레이어 위치에 따른 스테이지 비/활성화
    {
        // 모든 스테이지 비활성화
        foreach (GameObject stage in fullStage)
        {
            stage.SetActive(false);
        }

        // 현재 스테이지 값에 따른 활성화 범위 설정
        int start = Mathf.Max(0, playerMovement.currentStage - 1);
        int end = Mathf.Min(fullStage.Length, playerMovement.currentStage + 3);

        for (int i = start; i < end; i++)
        {
            fullStage[i].SetActive(true);
        }

        // currentStage가 15 이상이면 더 이상 업데이트 하지 않도록 (현재 최대값 15)
        if (playerMovement.currentStage >= 15){}
    }
}
