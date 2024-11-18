using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    public MovingObject movingObject;
    public PlayerMovement player; // �÷��̾�

    [Header("��ư")]
    public GameObject[] buttons;  // ��ư��

    [Header("Ȯ���� Ƚ��")]
    public int checkCount;  // Ȯ�� ��
    public int currentCheckCount;

    private void Start()
    {
        NullObjectFind();
        // �ʱ� ī��Ʈ ����
        currentCheckCount = checkCount;
    }

    private void Update()
    {
        if (currentCheckCount == 0)
        {
            CheckButtons();
        }
    }

    void NullObjectFind()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }
    }

    private void CheckButtons()
    {
        bool allMatch = true;

        // ��� ��ư ���� Ȯ��
        foreach (GameObject buttonObj in buttons)
        {
            ButtonInfor buttonInfo = buttonObj.GetComponent<ButtonInfor>();
            if (buttonInfo.trueButton != buttonInfo.currentStatus)
            {
                allMatch = false;
                break;
            }
        }

        // ��� ��ư ���°� ��ġ�ϴ� ���
        if (allMatch)
        {
            movingObject.activated = true;
        }
        else
        {
            // ���� ����ġ �� ī��Ʈ ���� �� �÷��̾� ü�� ����
            currentCheckCount = checkCount;
            player.currentHealth -= 5;
        }

        // ��� ��ư ���� false�� �ʱ�ȭ
        ResetButtonStates();
    }

    private void ResetButtonStates()
    {
        foreach (GameObject buttonObj in buttons)
        {
            ButtonInfor buttonInfo = buttonObj.GetComponent<ButtonInfor>();
            buttonInfo.currentStatus = false; // ��ư ���� false�� ����
            buttonInfo.GetComponent<Renderer>().material = buttonInfo.materials[0]; // ���� ������ ����
        }
    }
}