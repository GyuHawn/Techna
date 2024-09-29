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
            NextStageSetting();
        }
    }

    // 비동기적으로 씬을 로드하고, 로딩이 완료된 후 플레이어 이동
    void NextStageSetting()
    {
        switch (playerMovement.currentStage)
        {
            case 2:
               // StartCoroutine(LoadNextStageAsync("Stage2", new Vector3(1f, 27f, 35f), Quaternion.Euler(0, 180, 0)));
                StartCoroutine(LoadNextStageAsync("Stage2", new Vector3(-64f, 16f, 88f), Quaternion.Euler(0, 180, 0)));
                previousStage = playerMovement.currentStage;
                break;
            case 3:
                StartCoroutine(LoadNextStageAsync("Stage3", new Vector3(0f, 4f, 0f), Quaternion.Euler(0, 180, 0)));
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
