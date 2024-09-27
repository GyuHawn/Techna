using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GemUI : MonoBehaviour
{
    private GemCombination gemCombination;
    private GemManager gemManager;

    public GameObject bulletUI; // 총알 보석 UI  
    public GameObject bulletMenuUI; // 총알 메뉴 UI  
    public GameObject bulletGemUI; // 개별 총알 보석 UI  
    public GameObject[] currentBullet; // 현재 총알 UI

    public GameObject attributeUI; // 속성 보석
    public GameObject attributeMenuUI; // 속성 메뉴 UI
    public GameObject[] attributeGemUI; // 개별 속성 보석
    public GameObject[] currentAttribute; // 현재 속성 UI

    public GameObject functionUI; // 기능 보석
    public GameObject functionMenuUI; // 기능 메뉴 UI
    public GameObject[] functionGemUI; // 개별 기능 보석
    public GameObject[] currentFunction; // 현재 기능 UI

    public int selectGemNum;

    // 각 활성화 여부
    private bool selectBullet;


    private void Awake()
    {
        gemCombination = FindObjectOfType<GemCombination>();
        gemManager = FindObjectOfType<GemManager>();
    }

    void Update()
    {
        if (!selectBullet) // 총알, 속성, 기능 메뉴 중 선택
        {
            // 키 입력에 따른 UI 열고 닫기
            OpenGemUI();
        }
        else if (selectBullet) // 선택한 메뉴 안에서 선택지 선택
        {
            SelectFunction(); // 기능 선택
        }
    }

    void OpenGemUI() // 1번키, 메뉴, 선택값
    {
        if (Input.GetButtonDown("1"))
            ToggleMenu(bulletMenuUI, 1);
        if (Input.GetButtonDown("2"))
            ToggleMenu(attributeMenuUI, 2);
        if (Input.GetButtonDown("3"))
            ToggleMenu(functionMenuUI, 3);
    }
    private void ToggleMenu(GameObject menuUI, int menuNum)
    {
        selectBullet = true;
        ResetUIStates(); // 모든 UI 상태 초기화 및 닫기
        menuUI.SetActive(true); // 메뉴 열기
        selectGemNum = menuNum; // 선택한 메뉴 값
    }

    public void ActivateGemUI() // 상태에 따른 UI 활성화
    {
        bulletGemUI.SetActive(gemManager.onLarge);

        attributeGemUI[0].SetActive(gemManager.onControl);
        attributeGemUI[1].SetActive(gemManager.onElectricity);
        attributeGemUI[2].SetActive(gemManager.onExpansion);
        attributeGemUI[3].SetActive(gemManager.onGravity);

        functionGemUI[0].SetActive(gemManager.onPenetrate);
        functionGemUI[1].SetActive(gemManager.onDestruction);
        functionGemUI[2].SetActive(gemManager.onDiffusion);
        functionGemUI[3].SetActive(gemManager.onUpgrade);
        functionGemUI[4].SetActive(gemManager.onQuick);
    }

    private void SelectFunction() // 기능 선택
    {
        if (selectGemNum == 1)
        {
            SelectOption(ref gemCombination.selectBulletNum, 2); // 선택 메뉴, 전달할 변수, 선택지
        }
        else if (selectGemNum == 2)
        {
            SelectOption(ref gemCombination.selectAttributeNum, 4);
        }
        else if (selectGemNum == 3)
        {
            SelectOption(ref gemCombination.selectFunctionNum, 5);
        }
    }

    private void SelectOption(ref int selectedFunction, int choiceNum) // 메뉴 내 선택지 처리
    {
        for (int i = 1; i <= choiceNum; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
            {
                selectedFunction = i;
                CurrentGemUI(selectedFunction - 1);
                CheckCurrentGem();
                ConfirmSelection();
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1 + choiceNum)) // 마지막 선택지를 0으로 처리
        {
            selectedFunction = 0;
            ResetCurrentGemUI();
            ConfirmSelection();
        }
    }

    void CurrentGemUI(int num) // 현재 보석 UI
    {
        if (selectGemNum == 1)
        {
            for (int i = 0; i < currentBullet.Length; i++)
            {
                currentBullet[i].SetActive(false);
            }
            currentBullet[num].SetActive(true);
        }
        else if (selectGemNum == 2)
        {
            for (int i = 0; i < currentAttribute.Length; i++)
            {
                currentAttribute[i].SetActive(false);
            }
            currentAttribute[num].SetActive(true);
        }
        else if (selectGemNum == 3)
        {
            for (int i = 0; i < currentFunction.Length; i++)
            {
                currentFunction[i].SetActive(false);
            }
            currentFunction[num].SetActive(true);
        }
    }
    void ResetCurrentGemUI() // 현재 보석 초기화
    {
        if (selectGemNum == 1)
        {
            for (int i = 0; i < currentBullet.Length; i++)
            {
                currentBullet[i].SetActive(false);
            }
            currentBullet[0].SetActive(true);
        }
        else if (selectGemNum == 2)
        {
            for (int i = 0; i < currentAttribute.Length; i++)
            {
                currentAttribute[i].SetActive(false);
            }
        }
        else if (selectGemNum == 3)
        {
            for (int i = 0; i < currentFunction.Length; i++)
            {
                currentFunction[i].SetActive(false);
            }
        }
    }

    void CheckCurrentGem() // 현재 보석 확인
    {
        if (!gemManager.onLarge)
        {
            currentBullet[1].SetActive(false);
            currentBullet[0].SetActive(true);
        }

        if (!gemManager.onControl) currentAttribute[0].SetActive(false);
        if (!gemManager.onElectricity) currentAttribute[1].SetActive(false);
        if (!gemManager.onExpansion) currentAttribute[2].SetActive(false);
        if (!gemManager.onGravity) currentAttribute[3].SetActive(false);

        if (!gemManager.onPenetrate) currentFunction[0].SetActive(false);
        if (!gemManager.onDestruction) currentFunction[1].SetActive(false);
        if (!gemManager.onDiffusion) currentFunction[2].SetActive(false);
        if (!gemManager.onUpgrade) currentFunction[3].SetActive(false);
        if (!gemManager.onQuick) currentFunction[4].SetActive(false);
    }

    private void ConfirmSelection() // 선택 확인 처리
    {
        selectBullet = false;
        ResetUIStates();
    }

    private void ResetUIStates()  // 모든 UI 상태 초기화 및 닫기
    {
        // 메뉴 초기화
        bulletMenuUI.SetActive(false);
        attributeMenuUI.SetActive(false);
        functionMenuUI.SetActive(false);
    }

    public void CombinationFailedUI() // 조합 실패시 UI 상태 처리
    {
        currentBullet[0].SetActive(true);
        currentBullet[1].SetActive(false);

        foreach (var attribute in currentAttribute)
        {
            attribute.SetActive(false);
        }
        foreach (var function in currentFunction)
        {
            function.SetActive(false);
        }
    }
}