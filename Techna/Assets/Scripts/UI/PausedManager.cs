using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedManager : MonoBehaviour
{
    public SettingManager settingManager;

    public GameObject pausedUI;
    public bool onPaused;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            pausedUI.SetActive(!onPaused);
            settingManager.settingUI.SetActive(onPaused);
            onPaused = !onPaused;
        }
    }

    public void Continue()
    {
        pausedUI.SetActive(!onPaused);
        settingManager.settingUI.SetActive(false);
        onPaused = !onPaused;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
