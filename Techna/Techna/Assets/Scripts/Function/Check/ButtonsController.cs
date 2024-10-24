using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    public MovingObject movingObject;
    public PlayerMovement player; // 플레이어

    [Header("버튼")]
    public GameObject[] buttons;  // 버튼들

    [Header("확인할 횟수")]
    public int checkCount;  // 확인 수
    public int currentCheckCount;

    private void Start()
    {
        NullObjectFind();
        // 초기 카운트 설정
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

        // 모든 버튼 상태 확인
        foreach (GameObject buttonObj in buttons)
        {
            ButtonInfor buttonInfo = buttonObj.GetComponent<ButtonInfor>();
            if (buttonInfo.trueButton != buttonInfo.currentStatus)
            {
                allMatch = false;
                break;
            }
        }

        // 모든 버튼 상태가 일치하는 경우
        if (allMatch)
        {
            movingObject.activated = true;
        }
        else
        {
            // 상태 불일치 시 카운트 리셋 및 플레이어 체력 감소
            currentCheckCount = checkCount;
            player.currentHealth -= 5;
        }

        // 모든 버튼 상태 false로 초기화
        ResetButtonStates();
    }

    private void ResetButtonStates()
    {
        foreach (GameObject buttonObj in buttons)
        {
            ButtonInfor buttonInfo = buttonObj.GetComponent<ButtonInfor>();
            buttonInfo.currentStatus = false; // 버튼 상태 false로 설정
            buttonInfo.renderer.material = buttonInfo.materials[0]; // 원래 재질로 복원
        }
    }
}