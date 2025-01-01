using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2PortalSingleton : MonoBehaviour
{
    public static Stage2PortalSingleton Instance { get; private set; }  // ½Ì±ÛÅæ Àû¿ë

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Æ¯Á¤ ¾À¿¡¼­ ¿ÀºêÁ§Æ® ÆÄ±«
        if (scene.name == "Main" || scene.name == "Stage8")
        {
            Destroy(gameObject);
        }
    }
}
