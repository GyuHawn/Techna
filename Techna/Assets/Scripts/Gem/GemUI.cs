using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GemUI : MonoBehaviour
{
    private GemCombination gemCombination;
    private GemManager gemManager;

    [Header("�Ѿ� UI")]
    public GameObject bulletUI; // �Ѿ� ���� UI  
    public GameObject bulletMenuUI; // �Ѿ� �޴� UI  
    public GameObject bulletGemUI; // ���� �Ѿ� ���� UI  
    public GameObject[] currentBullet; // ���� �Ѿ� UI

    [Header("�Ӽ� UI")]
    public GameObject attributeUI; // �Ӽ� ����
    public GameObject attributeMenuUI; // �Ӽ� �޴� UI
    public GameObject[] attributeGemUI; // ���� �Ӽ� ����
    public GameObject[] currentAttribute; // ���� �Ӽ� UI

    [Header("��� UI")]
    public GameObject functionUI; // ��� ����
    public GameObject functionMenuUI; // ��� �޴� UI
    public GameObject[] functionGemUI; // ���� ��� ����
    public GameObject[] currentFunction; // ���� ��� UI

    [Header("������ ����")]
    public int selectGemNum; // ������ ��

    private bool selectBullet;


    private void Awake()
    {
        gemCombination = FindObjectOfType<GemCombination>();
        gemManager = FindObjectOfType<GemManager>();
    }

    void Update()
    {
        if (!selectBullet) // �Ѿ�, �Ӽ�, ��� �޴� �� ����
        {
            // Ű �Է¿� ���� UI ���� �ݱ�
            OpenGemUI();
        }
        else if (selectBullet) // ������ �޴� �ȿ��� ������ ����
        {
            SelectFunction(); // ��� ����
        }
    }

    void OpenGemUI() // 1��Ű, �޴�, ���ð�
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
        ResetUIStates(); // ��� UI ���� �ʱ�ȭ �� �ݱ�
        menuUI.SetActive(true); // �޴� ����
        selectGemNum = menuNum; // ������ �޴� ��
    }

    public void ActivateGemUI() // ���¿� ���� UI Ȱ��ȭ
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

    private void SelectFunction() // ��� ����
    {
        if (selectGemNum == 1)
        {
            SelectOption(ref gemCombination.selectBulletNum, 2); // ���� �޴�, ������ ����, ������
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

    private void SelectOption(ref int selectedFunction, int choiceNum) // �޴� �� ������ ó��
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
        if (Input.GetKeyDown(KeyCode.Alpha1 + choiceNum)) // ������ �������� 0���� ó��
        {
            selectedFunction = 0;
            ResetCurrentGemUI();
            ConfirmSelection();
        }
    }

    void CurrentGemUI(int num) // ���� ���� UI
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
    void ResetCurrentGemUI() // ���� ���� �ʱ�ȭ
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

    void CheckCurrentGem() // ���� ���� Ȯ��
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

    private void ConfirmSelection() // ���� Ȯ�� ó��
    {
        selectBullet = false;
        ResetUIStates();
    }

    private void ResetUIStates()  // ��� UI ���� �ʱ�ȭ �� �ݱ�
    {
        // �޴� �ʱ�ȭ
        bulletMenuUI.SetActive(false);
        attributeMenuUI.SetActive(false);
        functionMenuUI.SetActive(false);
    }

    public void CombinationFailedUI() // ���� ���н� UI ���� ó��
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