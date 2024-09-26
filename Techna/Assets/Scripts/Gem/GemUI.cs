using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GemUI : MonoBehaviour
{
    private GemCombination gemCombination;
    private GemManager gemManager;

    public GameObject bulletUI;
    public GameObject bulletMenuUI;
    public GameObject bulletGemUI;
    public GameObject[] currentBullet;

    public GameObject attributeUI;
    public GameObject attributeMenuUI;
    public GameObject[] attributeGemUI;
    public GameObject[] currentAttribute;

    public GameObject functionUI;
    public GameObject functionMenuUI;
    public GameObject[] functionGemUI;
    public GameObject[] currentFunction;

    public int selectGemNum;

    private bool selectBullet;

    private PlayerInputActions inputActions; // Input System 액션

    private void Awake()
    {
        gemCombination = FindObjectOfType<GemCombination>();
        gemManager = FindObjectOfType<GemManager>();

        // Input System 초기화
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        // UI 액션 맵 내의 액션들과 연결
        inputActions.UI.Menu1.performed += ctx => ToggleMenu(bulletMenuUI, 1);
        inputActions.UI.Menu2.performed += ctx => ToggleMenu(attributeMenuUI, 2);
        inputActions.UI.Menu3.performed += ctx => ToggleMenu(functionMenuUI, 3);
    }


    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        if (!selectBullet) // 총알, 속성, 기능 메뉴 중 선택
        {
            // UI 열고 닫기
        }
        else if (selectBullet) // 선택한 메뉴 안에서 선택지 선택
        {
            SelectFunction(); // 기능 선택
        }
    }

    private void ToggleMenu(GameObject menuUI, int menuNum)
    {
        selectBullet = true;
        ResetUIStates(); // 모든 UI 상태 초기화 및 닫기
        menuUI.SetActive(true); // 메뉴 열기
        selectGemNum = menuNum; // 선택한 메뉴 값
    }

    public void ActivateGemUI()
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

    private void SelectFunction()
    {
        if (selectGemNum == 1)
        {
            SelectOption(ref gemCombination.selectBulletNum, 2);
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

    private void SelectOption(ref int selectedFunction, int choiceNum)
    {
        for (int i = 1; i <= choiceNum; i++)
        {
            if (inputActions.UI.SelectOption.triggered)
            {
                selectedFunction = i;
                CurrentGemUI(selectedFunction - 1);
                CheckCurrentGem();
                ConfirmSelection();
                return;
            }
        }
    }

    void CurrentGemUI(int num)
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

    void CheckCurrentGem()
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

    private void ConfirmSelection()
    {
        selectBullet = false;
        ResetUIStates();
    }

    private void ResetUIStates()
    {
        bulletMenuUI.SetActive(false);
        attributeMenuUI.SetActive(false);
        functionMenuUI.SetActive(false);
    }

    public void CombinationFailedUI()
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
