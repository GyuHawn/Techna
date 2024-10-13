using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public TMP_Text continueText; // ����ϱ� �ؽ�Ʈ
    public TMP_Text newGameText; // ������ �ؽ�Ʈ
   
    public TMP_Text exitText; // ���� �ؽ�Ʈ   

    public void NewGameStart() // ������ ����
    {
        newGameText.color = Color.yellow; // ������
        SceneManager.LoadScene("Stage1"); // �� �̵�
    }

    public void ContinueGame() // ����ϱ� (�̱���)
    {
        continueText.color = Color.yellow;
        // ���̺� ���� ������ �о�ͼ� ����
    }

    public void GameExit() // ���� ����
    {
        exitText.color = Color.yellow;
        Application.Quit();
    }
}
