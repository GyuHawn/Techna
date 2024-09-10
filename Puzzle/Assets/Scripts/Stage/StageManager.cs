using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject player; // 플레이어

    public GameObject[] stageSetting; // 스테이지 값 설정

    private int previousStage; // 이전 스테이지 값 저장

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();    
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
        if (playerMovement.currentStage == 2)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                Vector3 newPosition = new Vector3(-400f, 4, -375);
                Quaternion newRotation = Quaternion.Euler(0, 180, 0);
                controller.enabled = false;
                player.transform.position = newPosition;
                player.transform.rotation = newRotation;
                controller.enabled = true; 
            }

            playerMovement.currentStage = 0;
            stageSetting[0].SetActive(false);
            stageSetting[1].SetActive(true);
        }
    }
}
