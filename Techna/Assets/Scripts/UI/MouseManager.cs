using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public bool isCursorVisible; // 마우스 커서 활성화 여부
    
    public Sprite[] crossHairs; // 조준점 배열
    public GameObject crossHair; // 조준점

    void Start()
    {
        // 커서 숨김 및 잠금 상태 설정
        SetCursorState(false);
    }

    void Update()
    {
        // 마우스 커서 활성화 여부를 토글
        if (Input.GetButtonDown("CursorHide"))
        {
            SetCursorState(!isCursorVisible);
        }
    }

    // 커서 활성화 상태 설정 함수
    private void SetCursorState(bool isVisible)
    {
        isCursorVisible = isVisible;
        Cursor.visible = isCursorVisible;
        Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;

        crossHair.SetActive(!isVisible);
    }
}