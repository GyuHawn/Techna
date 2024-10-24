using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public bool isCursorVisible; // ���콺 Ŀ�� Ȱ��ȭ ����
    
    public Sprite[] crossHairs; // ������ �迭
    public GameObject crossHair; // ������

    void Start()
    {
        // Ŀ�� ���� �� ��� ���� ����
        SetCursorState(isCursorVisible);
    }

    void Update()
    {
        // ���콺 Ŀ�� Ȱ��ȭ ���θ� ���
        if (Input.GetButtonDown("CursorHide"))
        {
            SetCursorState(!isCursorVisible);
        }
    }

    // Ŀ�� Ȱ��ȭ ���� ���� �Լ�
    public void SetCursorState(bool isVisible)
    {
        isCursorVisible = isVisible;
        Cursor.visible = isCursorVisible;
        Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;

        crossHair.SetActive(!isVisible);
    }
}