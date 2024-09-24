using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public TMP_Text continueText;
    public TMP_Text newGameText;
    public TMP_Text settingText;
    public TMP_Text exitText;

    public GameObject settingUI;

    public void NewGameStart()
    {
        newGameText.color = Color.yellow;
        SceneManager.LoadScene("Game");
    }

    public void ContinueGame()
    {
        continueText.color = Color.yellow;
        // 세이브 파일 읽어와서 실행
    }

    public void GameSetting()
    {
        settingText.color = Color.yellow;
        settingUI.SetActive(true);
        StartCoroutine(ResetTextColor(settingText));
    }

    public void CloseGameSetting()
    {
        settingUI.SetActive(false);
    }

    public void GameExit()
    {
        exitText.color = Color.yellow;
        Application.Quit();
    }

    IEnumerator ResetTextColor(TMP_Text text)
    {
        yield return new WaitForSeconds(1f);

        text.color = Color.white;
    }

}
