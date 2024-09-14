using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    public GemManager gemManager;

    public string infor; // 정보 
    public GameObject inforUI; // 정보 UI
    public int inforUIWidthSize; // 정보 UI 너비 사이즈
    public int inforUIHeightSize; // 정보 UI 높이 사이즈
    public TMP_Text inforText; // 정보 텍스트
    public int textSize; // 텍스트 사이즈
    public Color textColor; // 색 
    public bool showUI; // 표시 중 여부

    public bool gemInfor; // 보석 정보 인지
    public bool gem; // 보석 인지

    private void Start()
    {
        // 줄바꿈 및 색상 설정
        if (!string.IsNullOrEmpty(infor))
        {
            infor = infor.Replace(@"\n", "\n");
        }

        if (inforText != null)
        {
            inforText.color = textColor; // 색상 설정
            inforText.gameObject.SetActive(false); // 시작 시 비활성화
        }

        if (inforUI != null)
        {
            RectTransform inforUIRect = inforUI.GetComponent<RectTransform>();
            inforUIRect.sizeDelta = new Vector2(inforUIWidthSize, inforUIHeightSize); // UI 크기 설정
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gemInfor)
            {
                if (!showUI)
                {
                    showUI = true;
                    DisplayInfoUI(); // 정보 표시
                }
            }

            if (gem)
            {
                gemManager.CollectGem(gameObject.name); // 보석 이름 확인
                Destroy(gameObject); // 보석 제거
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gemInfor && showUI)
        {
            showUI = false; 
            HideInfoUI(); // 정보 숨기기
        }
    }

    private void DisplayInfoUI() // 정보 표시
    {
        inforUI.SetActive(true);
        inforText.gameObject.SetActive(true);
        inforText.text = infor;
        inforText.fontSize = textSize;
    }

    private void HideInfoUI() // 정보 숨기기
    {
        inforUI.SetActive(false);
        inforText.gameObject.SetActive(false);
        inforText.text = "";
    }
}
