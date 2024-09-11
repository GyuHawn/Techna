using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ObjectHintDisplay : MonoBehaviour
{
    public string hintText; // 힌트
    public Image hintUI; // 힌트 UI
    public int hintUIWidthSize; // 힌트 UI 너비 사이즈
    public int hintUIHeightSize; // 힌트 UI 높이 사이즈
    public TMP_Text showText; // 표시 텍스트
    public int textSize; // 텍스트 사이즈
    public bool showUI; // 표시 중 여부

    void Start()
    {
        if (!string.IsNullOrEmpty(hintText))
        {
            hintText = hintText.Replace(@"\n", "\n"); // 줄바꿈 수동 설정
        }

        ToggleHintUI(false); // 힌트 UI 비활성화
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !showUI)
        {
            ToggleHintUI(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && showUI)
        {
            ToggleHintUI(false);
        }
    }

    // UI 활성화/비활성화 함수
    private void ToggleHintUI(bool show)
    {
        showUI = show;
        hintUI.gameObject.SetActive(show);
        showText.gameObject.SetActive(show);

        if (show)
        {
            showText.text = hintText; // 힌트 적용
            // UI 크기 설정
            RectTransform hintUIRect = hintUI.GetComponent<RectTransform>();
            hintUIRect.sizeDelta = new Vector2(hintUIWidthSize, hintUIHeightSize);
            showText.fontSize = textSize; // 폰트 크기 설정
        }
        else
        {
            showText.text = ""; // 텍스트 초기화
        }
    }
}
