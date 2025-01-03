using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameDisplay : MonoBehaviour
{
    private float deltaTime = 0f;

    [SerializeField, Range(1, 100)]
    private int size = 20;

    [SerializeField]
    private Color color = Color.white;

    public Font font;

    public TMP_Text fpsText;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // FPS ���� fpsText�� ����
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);
        fpsText.color = color;  // ���� ����
        fpsText.fontSize = size;  // ��Ʈ ũ�� ����
    }
}