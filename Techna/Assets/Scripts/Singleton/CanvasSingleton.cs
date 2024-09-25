using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSingleton : MonoBehaviour
{
    public static CanvasSingleton Instance { get; private set; }  // ½Ì±ÛÅæ Àû¿ë

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
