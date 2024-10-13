using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameDisplay : MonoBehaviour
{
    private float deltaTime = 0f;

    [SerializeField, Range(1, 100)]
    private int size = 20;

    [SerializeField]
    private Color color = Color.white;

    public Font font;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        if (font != null)
        {
            style.font = font;  // 폰트 설정
        }

        Rect rect = new Rect(30, 30, Screen.width, Screen.height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;

        float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS", fps);

        GUI.Label(rect, text, style);
    }
}