using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingManager : MonoBehaviour
{
    private string currentScene;

    public TMP_Text settingText; // 세팅 텍스트
    public GameObject gameLogo; // 게임 로고
    public GameObject settingUI; // 세팅 메뉴
    public bool onSettingUI;  // 세팅 UI 활성화 여부

    public GameObject audioUI; // 오디오 메뉴
    public GameObject controlsUI; // 컨트롤 메뉴
    public Scrollbar controlScrollbar; // 컨트롤 메뉴 스크롤 뷰

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void GameSetting() // 세팅 메뉴 비/활성화
    {
        if (!onSettingUI) // 비활성화시
        {
            if (currentScene == "Main")
            {
                gameLogo.SetActive(false); // 게임 로고 비활성화
                settingText.color = Color.yellow; // 텍스트 색 변경
                StartCoroutine(ResetTextColor(settingText)); // 텍스트 색 초기화
            }
            

            onSettingUI = true; // 활성화
            settingUI.SetActive(true); // 세팅 메뉴 열기
        }
        else
        {
            CloseGameSetting();
        }
    }

    public void CloseGameSetting() // 세팅 메뉴 닫기
    {
        if (currentScene == "Main")
        {
            gameLogo.SetActive(true); // 게임 로고 활성화
        }

        onSettingUI = false; // 세팅 활성화 여부 변경
        settingUI.SetActive(false); // 세팅 메뉴 비활성화
        audioUI.SetActive(false); // 오디오 메뉴 비활성화
        controlsUI.SetActive(false); // 컨트롤 메뉴 활성화
    }

    IEnumerator ResetTextColor(TMP_Text text) // 텍스트 색 초기화
    {
        yield return new WaitForSeconds(1f);

        text.color = Color.white;
    }

    public void AudioSetting() // 오디오 메뉴 활성화
    {
        audioUI.SetActive(true);
        controlsUI.SetActive(false);
    }

    public void ControlsSetting() // 컨트롤 메뉴 활성화
    {
        audioUI.SetActive(false);
        controlsUI.SetActive(true);

        controlScrollbar.value = 1f;
    }  
}
