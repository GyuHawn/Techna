using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    public bool isCursorVisible; // 마우스 커서 활성화 여부
    public Sprite[] crossHairs; // 조준점 배열
    public GameObject crossHair; // 조준점

    private PlayerInputActions inputActions; // Input Actions 

    void Awake()
    {
        inputActions = new PlayerInputActions(); // Input Actions 초기화
    }

    private void Start()
    {
        SetCursorState(isCursorVisible); // 시작 시 커서 상태 설정
    }

    void OnEnable()
    {
        inputActions.Enable(); // Input Actions 활성화
        inputActions.UI.Cursor.performed += ctx => ToggleCursor(); // CursorHide 액션과 메서드 연결
    }

    void OnDisable()
    {
        inputActions.Disable(); // Input Actions 비활성화
    }

    // 커서 활성화 상태 토글 함수
    private void ToggleCursor()
    {
        SetCursorState(!isCursorVisible);
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
