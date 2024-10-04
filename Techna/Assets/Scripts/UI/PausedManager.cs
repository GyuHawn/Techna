using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedManager : MonoBehaviour
{
    public SettingManager settingManager; // 세팅 매니저
    public MouseManager mouseManager; // 마우스 매니저

    public GameObject pausedUI; // 정지 메뉴
    public bool onPaused; // 정지 여부

    private void Update()
    {
        if (Input.GetButtonDown("Pause")) // ESC
        {
            pausedUI.SetActive(!onPaused); // 정지 여부에 따른 메뉴 비/활성화
            settingManager.settingUI.SetActive(false); // 세팅 메뉴 닫기
            onPaused = !onPaused; // 정지 여부 변경

            mouseManager.SetCursorState(!mouseManager.isCursorVisible); // 커서 활성화

            PauseGame(); // 게임 정지
        }
    }

    public void Continue() // 계속하기
    {
        pausedUI.SetActive(false); // 메뉴 닫기
        settingManager.settingUI.SetActive(false); // 세팅 메뉴 닫기
        onPaused = !onPaused; // 정지 여부 변경

        mouseManager.SetCursorState(!mouseManager.isCursorVisible); // 커서 비활성화

        PauseGame(); // 게임 진행
    }

    void PauseGame() // 게임 정지/진행
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

    public void MainMenu() // 메인 메뉴 이동
    {
        SceneManager.LoadScene("Main");
    }
}
