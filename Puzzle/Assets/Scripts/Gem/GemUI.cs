using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemUI : MonoBehaviour
{
    private GemCombination gemCombination;

    public GameObject bulletUI; // 총알 보석 UI  
    public GameObject bulletMenuUI; // 총알 메뉴 UI  
    public GameObject attributeUI; // 속성 보석
    public GameObject attributeMenuUI; // 속성 메뉴 UI
    public GameObject functionUI; // 기능 보석
    public GameObject functionMenuUI; // 기능 메뉴 UI

    // 각 활성화 여부
    private bool onBulletUI;
    private bool onAttributeUI;
    private bool onFunctionUI;

    private void Awake()
    {
        if (!gemCombination)
            gemCombination = FindObjectOfType<GemCombination>();
    }

    void Update()
    {
        // 키 입력에 따른 UI 열고 닫기
        OpenGemUI("1", ref onBulletUI, bulletMenuUI);
        OpenGemUI("2", ref onAttributeUI, attributeMenuUI);
        OpenGemUI("3", ref onFunctionUI, functionMenuUI);
    }

    private void OpenGemUI(string KeyNum, ref bool uiState, GameObject menuUI) // 키 입력에 따른 UI 열고 닫기
    {
        if (Input.GetButtonDown(KeyNum)) // 키입력 시
        {
            if (!uiState) // 활성화 상태 확인
            {
                ResetUIStates(); // 모든 UI 상태 초기화 및 닫기
                uiState = true; // 상태 활성화
                menuUI.SetActive(true); // 메뉴 열기
            }
            else
            {
                uiState = false;
                menuUI.SetActive(false);
            }
        }
    }

    private void ResetUIStates()  // 모든 UI 상태 초기화 및 닫기
    {
        onBulletUI = false; 
        bulletMenuUI.SetActive(false);

        onAttributeUI = false;
        attributeMenuUI.SetActive(false);

        onFunctionUI = false;
        functionMenuUI.SetActive(false);
    }
}
