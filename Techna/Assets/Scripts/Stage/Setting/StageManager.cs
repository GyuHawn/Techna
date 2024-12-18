using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CharacterController controller;

    public GameObject player; // 플레이어

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

    private void Update()
    {
        if (playerMovement.currentStage != previousStage)
        {
            NextStageSetting(); // 스테이지 이동
        }
    }

    // 씬을 로드, 로딩이 완료 후 플레이어 이동
    void NextStageSetting()
    {
        switch (playerMovement.currentStage) // 씬 이동, 플레이어 위치 설정
        {
            case 2:
                StartCoroutine(LoadNextStageAsync("Stage2", new Vector3(1f, 27f, 35f), Quaternion.Euler(0, 180, 0)));
                previousStage = playerMovement.currentStage;
                break;
            case 3:
                StartCoroutine(LoadNextStageAsync("Stage3", new Vector3(0f, 10f, 0f), Quaternion.Euler(0, 0, 0)));
                previousStage = playerMovement.currentStage;
                break;
            case 4:
                StartCoroutine(LoadNextStageAsync("Stage4", new Vector3(0f, 16f, 20f), Quaternion.Euler(0, 180, 0)));
                previousStage = playerMovement.currentStage;
                break;
            case 5:
                StartCoroutine(LoadNextStageAsync("Stage5", new Vector3(92f, 40f, -36.5f), Quaternion.Euler(0, 90, 0)));
                previousStage = playerMovement.currentStage;
                break;
            case 6:
                StartCoroutine(LoadNextStageAsync("Stage6", new Vector3(70f, 3f, 7f), Quaternion.Euler(0, -45f, 0)));
                previousStage = playerMovement.currentStage;
                break;
        }
    }

    // 비동기 씬 로딩 코루틴
    IEnumerator LoadNextStageAsync(string sceneName, Vector3 position, Quaternion rotation)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 씬 로딩이 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 씬 로딩 완료 후 플레이어 이동
        MovePlayer(position, rotation);
    }

    void MovePlayer(Vector3 position, Quaternion rotation)
    {
        if (controller != null)
        {
            controller.enabled = false; // 이동 전에 CharacterController 비활성화
            player.transform.position = position; // 새로운 위치 설정
            player.transform.rotation = rotation; // 새로운 회전 설정
            controller.enabled = true; // 위치 설정 후 CharacterController 다시 활성화
        }
    }
}
