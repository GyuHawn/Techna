using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingManager : MonoBehaviour
{
    public string currentScene;

    public TMP_Text settingText; // ���� �ؽ�Ʈ
    public GameObject gameLogo; // ���� �ΰ�
    public GameObject settingUI; // ���� �޴�
    public bool onSettingUI;  // ���� UI Ȱ��ȭ ����

    public GameObject audioUI; // ����� �޴�
    public GameObject controlsUI; // ��Ʈ�� �޴�
    public Scrollbar controlScrollbar; // ��Ʈ�� �޴� ��ũ�� ��

    public void GameSetting() // ���� �޴� ��/Ȱ��ȭ
    {
        if (!onSettingUI) // ��Ȱ��ȭ��
        {
            currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "Main")
            {
                gameLogo.SetActive(false); // ���� �ΰ� ��Ȱ��ȭ
                settingText.color = Color.yellow; // �ؽ�Ʈ �� ����
                StartCoroutine(ResetTextColor(settingText)); // �ؽ�Ʈ �� �ʱ�ȭ
            }
            

            onSettingUI = true; // Ȱ��ȭ
            settingUI.SetActive(true); // ���� �޴� ����
        }
        else
        {
            CloseGameSetting();
        }
    }

    public void CloseGameSetting() // ���� �޴� �ݱ�
    {
        currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene == "Main")
        {
            gameLogo.SetActive(true); // ���� �ΰ� Ȱ��ȭ
        }

        onSettingUI = false; // ���� Ȱ��ȭ ���� ����
        settingUI.SetActive(false); // ���� �޴� ��Ȱ��ȭ
        audioUI.SetActive(false); // ����� �޴� ��Ȱ��ȭ
        controlsUI.SetActive(false); // ��Ʈ�� �޴� Ȱ��ȭ
    }

    IEnumerator ResetTextColor(TMP_Text text) // �ؽ�Ʈ �� �ʱ�ȭ
    {
        yield return new WaitForSeconds(1f);

        text.color = Color.white;
    }

    public void AudioSetting() // ����� �޴� Ȱ��ȭ
    {
        audioUI.SetActive(true);
        controlsUI.SetActive(false);
    }

    public void ControlsSetting() // ��Ʈ�� �޴� Ȱ��ȭ
    {
        audioUI.SetActive(false);
        controlsUI.SetActive(true);

        controlScrollbar.value = 1f;
    }  
}
