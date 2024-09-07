using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // 싱글톤 적용

    public PlayerMovement playerMovement;

    public GameObject[] fullStage; // 전체 스테이지

    public GameObject[] stageSetting; // 스테이지 값 설정

    private int previousStage; // 이전 스테이지 값 저장


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
            StageSetting(); // 스테이지에 따른 플레이어 스테이지 값 설정 오브젝트 
            previousStage = playerMovement.currentStage; // 이전 스테이지 값 갱신
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

        // 만약 currentStage가 4 이상이면 더 이상 업데이트하지 않음
        if (playerMovement.currentStage >= 4){}
    }

    void StageSetting() // 스테이지에 따른 플레이어 스테이지 값 설정 오브젝트 
    {
        ResetStageSetting();

        if (playerMovement.currentStage >= 0 && playerMovement.currentStage <= 5)
        {
            stageSetting[0].gameObject.SetActive(true);
        }
        else if(playerMovement.currentStage >= 5 && playerMovement.currentStage <= 13)
        {
            stageSetting[1].gameObject.SetActive(true);
        }
    }
    void ResetStageSetting() // 스테이지 값 설정 오브젝트 초기화
    {
        foreach (GameObject stage in stageSetting)
        {
            stage.SetActive(false);
        }
    }
}
