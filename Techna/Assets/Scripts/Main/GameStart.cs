using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public TMP_Text continueText; // 계속하기 텍스트
    public TMP_Text newGameText; // 새게임 텍스트
   
    public TMP_Text exitText; // 종료 텍스트   

    public void NewGameStart() // 새게임 시작
    {
        newGameText.color = Color.yellow; // 색변경
        SceneManager.LoadScene("Stage1"); // 씬 이동
    }

    public void ContinueGame() // 계속하기
    {
        continueText.color = Color.yellow;
        // 세이브 파일 읽어와서 실행
    }

    public void GameExit() // 게임 종료
    {
        exitText.color = Color.yellow;
        Application.Quit();
    }
}
