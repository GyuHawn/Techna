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
        if (hintText != null)
        {
            hintText = hintText.Replace(@"\n", "\n"); // 줄바꿈 수동 설정
        }

        showText.gameObject.SetActive(false); // 힌트 비활성화
    }

    private void OnTriggerStay(Collider other)
    {
        // 플레이어 충돌 시 힌트 출력
        if (other.gameObject.CompareTag("Player"))
        {
            if (!showUI)
            {
                showUI = true;
                hintUI.gameObject.SetActive(true);
                showText.gameObject.SetActive(true);
                showText.text = hintText; // 힌트 적용

                // UI 사이즈 조절
                RectTransform hintUIRect = hintUI.GetComponent<RectTransform>();
                hintUIRect.sizeDelta = new Vector2(hintUIWidthSize, hintUIHeightSize);

                // 폰트 크기 조절
                showText.fontSize = textSize;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어 충돌 종료시 충돌 종료
        if (other.gameObject.CompareTag("Player"))
        {
            if (showUI)
            {
                showUI = false;
                hintUI.gameObject.SetActive(false);
                showText.gameObject.SetActive(false);
                showText.text = ""; // 텍스트 초기화
            }
        }
    }
}
