using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSingleton : MonoBehaviour
{
    public static CanvasSingleton Instance { get; private set; }  // �̱��� ����

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
        // Ư�� ������ ������Ʈ �ı�
        if (scene.name == "Main")
        {
            Destroy(gameObject);
        }
    }
}
