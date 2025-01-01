    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CharacterController controller;

    public GameObject player; // �÷��̾�

    private int previousStage; // ���� �������� �� ����

    public GameObject loadingUI; // �ε� UI
    public Image loadingBar; // �ε���

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        controller = player.GetComponent<CharacterController>();
    }

    void Start()
    {
        previousStage = playerMovement.currentStage; // �������� �� �ʱ�ȭ
    }

    private void Update()
    {
        if (playerMovement.currentStage != previousStage)
        {
            NextStageSetting(); // �������� �̵�
        }
    }

    // ���� �ε�, �ε��� �Ϸ� �� �÷��̾� �̵�
    void NextStageSetting()
    {
        switch (playerMovement.currentStage) // �� �̵�, �÷��̾� ��ġ ����
        {
            case 2:
                StartCoroutine(LoadNextStageAsync("Stage1_1", new Vector3(1f, 27f, 35f), Quaternion.Euler(0, 180, 0)));
                previousStage = playerMovement.currentStage;
                break;
            case 3:
                StartCoroutine(LoadNextStageAsync("Stage1_2", new Vector3(0f, 10f, 0f), Quaternion.Euler(0, 0, 0)));
                previousStage = playerMovement.currentStage;
                break;
            case 4:
                StartCoroutine(LoadNextStageAsync("Stage2", new Vector3(0f, 16f, 20f), Quaternion.Euler(0, 180, 0)));
                previousStage = playerMovement.currentStage;
                break;
            case 5:
                StartCoroutine(LoadNextStageAsync("Stage3", new Vector3(92f, 40f, -36.5f), Quaternion.Euler(0, 90, 0)));
                previousStage = playerMovement.currentStage;
                break;
            case 6:
                StartCoroutine(LoadNextStageAsync("Stage4", new Vector3(70f, 3f, 7f), Quaternion.Euler(0, -45f, 0)));
                previousStage = playerMovement.currentStage;
                break;
        }
    }

    // �񵿱� �� �ε� �ڷ�ƾ
    IEnumerator LoadNextStageAsync(string sceneName, Vector3 position, Quaternion rotation)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // �� ��ȯ ��Ȱ��ȭ
        loadingUI.SetActive(true);
        float timer = 0f;

        // �� �ε��� �Ϸ�� ������ ���
        while (!asyncLoad.isDone)
        {
            yield return null;

            if (asyncLoad.progress < 0.9f)
            {
                loadingBar.fillAmount = asyncLoad.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                if (loadingBar.fillAmount >= 1f)
                {
                    asyncLoad.allowSceneActivation = true; // �� ��ȯ ���
                    yield return new WaitForSeconds(0.5f); // �ణ�� ���� �� �̵�
                    MovePlayer(position, rotation); // �̵� ��ġ ����
                    loadingUI.SetActive(false); // UI ��Ȱ��ȭ
                    yield break;
                }
            }
        }
    }

    void MovePlayer(Vector3 position, Quaternion rotation)
    {
        if (controller != null)
        {
            controller.enabled = false; // �̵� ���� CharacterController ��Ȱ��ȭ
            player.transform.position = position; // ���ο� ��ġ ����
            player.transform.rotation = rotation; // ���ο� ȸ�� ����
            controller.enabled = true; // ��ġ ���� �� CharacterController �ٽ� Ȱ��ȭ
        }
    }
}
