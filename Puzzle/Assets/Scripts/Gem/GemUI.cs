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

    public GameObject attributeUI; // 속성 보석
    public GameObject attributeMenuUI; // 속성 메뉴 UI
    public GameObject[] attributeGemUI; // 개별 속성 보석

    public GameObject functionUI; // 기능 보석
    public GameObject functionMenuUI; // 기능 메뉴 UI
    public GameObject[] functionGemUI; // 개별 기능 보석

    public int selectGemNum;

    // 각 활성화 여부
    private bool selectBullet;

    private void Awake()
    {
        if (!gemCombination)
            gemCombination = FindObjectOfType<GemCombination>();
        if (!gemManager)
            gemManager = FindObjectOfType<GemManager>();
    }

    void Update()
    {
        if (!selectBullet) // 총알, 속성, 기능 메뉴 중 선택
        {
            // 키 입력에 따른 UI 열고 닫기
            OpenGemUI("1", bulletMenuUI, 1); // 1번키, 메뉴, 선택값
            OpenGemUI("2", attributeMenuUI, 2);
            OpenGemUI("3", functionMenuUI, 3);
        }
        else if(selectBullet) // 선택한 메뉴 안에서 선택지 선택
        {
            SelectFunction(); // 기능 선택
        }
    }

    public void ActivateGemUI() // 상태에 따른 UI 활성화
    {
        Debug.Log("활성화");
        bulletGemUI.SetActive(gemManager.onLarge);

        attributeGemUI[0].SetActive(gemManager.onControl);
        attributeGemUI[1].SetActive(gemManager.onFire);
        attributeGemUI[2].SetActive(gemManager.onWater);
        attributeGemUI[3].SetActive(gemManager.onElectricity);

        functionGemUI[0].SetActive(gemManager.onDestruction);
        functionGemUI[1].SetActive(gemManager.onPenetrate);
        functionGemUI[2].SetActive(gemManager.onDiffusion);
        functionGemUI[3].SetActive(gemManager.onUpgrade);
        functionGemUI[4].SetActive(gemManager.onQuick);
    }

    private void OpenGemUI(string KeyNum, GameObject menuUI, int menuNum) // 키 입력에 따른 UI 열고 닫기
    {
        if (Input.GetButtonDown(KeyNum)) // 키입력 시
        {
            selectBullet = true;
            ResetUIStates(); // 모든 UI 상태 초기화 및 닫기
            menuUI.SetActive(true); // 메뉴 열기
            selectGemNum = menuNum; // 선택한 메뉴 값
        }
    }

    private void SelectFunction() // 기능 선택
    {
        if (selectGemNum == 1) // 총알 메뉴
        {
            SelectFunctionValue(1, ref gemCombination.selectBulletNum, 2); // 선택 메뉴, 전달할 변수, 선택지
        }
        else if (selectGemNum == 2) // 속성 메뉴
        {
            SelectFunctionValue(2, ref gemCombination.selectAttributeNum, 4);
        }
        else if (selectGemNum == 3) // 기능 메뉴
        {
            SelectFunctionValue(3, ref gemCombination.selectFunctionNum, 5);
        }
    }

    private void SelectFunctionValue(int menuNum, ref int selectedFunction, int choiceNum) // 메뉴 내 선택지 처리
    {
        if (menuNum == 1)
        {
            for (int i = 1; i <= choiceNum; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
                {
                    selectedFunction = i;
                    ConfirmSelection();
                    return;
                }
            }
        }
        else if(menuNum == 2)
        {
            for (int i = 1; i <= choiceNum; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
                {
                    selectedFunction = i;
                    ConfirmSelection();
                    return;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1 + choiceNum)) // 마지막 선택지를 0으로 처리
            {
                selectedFunction = 0;
                ConfirmSelection();
            }
        }
        else if(menuNum == 3) 
        {
            for (int i = 1; i <= choiceNum; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
                {
                    selectedFunction = i;
                    ConfirmSelection();
                    return;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1 + choiceNum)) // 마지막 선택지를 0으로 처리
            {
                selectedFunction = 0;
                ConfirmSelection();
            }
        }
    }

    private void ConfirmSelection() // 선택 확인 처리
    {
        selectBullet = false;
        ResetUIStates();
    }

    private void ResetUIStates()  // 모든 UI 상태 초기화 및 닫기
    {
        bulletMenuUI.SetActive(false);
        attributeMenuUI.SetActive(false);
        functionMenuUI.SetActive(false);
    }
}
