using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedManager : MonoBehaviour
{
    public SettingManager settingManager; // ���� �Ŵ���
    public MouseManager mouseManager; // ���콺 �Ŵ���

    public GameObject pausedUI; // ���� �޴�
    public bool onPaused; // ���� ����

    private void Update()
    {
        if (Input.GetButtonDown("Pause")) // ESC
        {
            pausedUI.SetActive(!onPaused); // ���� ���ο� ���� �޴� ��/Ȱ��ȭ
            settingManager.settingUI.SetActive(false); // ���� �޴� �ݱ�
            onPaused = !onPaused; // ���� ���� ����

            mouseManager.SetCursorState(!mouseManager.isCursorVisible); // Ŀ�� Ȱ��ȭ

            PauseGame(); // ���� ����
        }
    }

    public void Continue() // ����ϱ�
    {
        pausedUI.SetActive(false); // �޴� �ݱ�
        settingManager.settingUI.SetActive(false); // ���� �޴� �ݱ�
        onPaused = !onPaused; // ���� ���� ����

        mouseManager.SetCursorState(!mouseManager.isCursorVisible); // Ŀ�� ��Ȱ��ȭ

        PauseGame(); // ���� ����
    }

    void PauseGame() // ���� ����/����
    {
        if (!onPaused)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void MainMenu() // ���� �޴� �̵�
    {
        SceneManager.LoadScene("Main");
    }
}
