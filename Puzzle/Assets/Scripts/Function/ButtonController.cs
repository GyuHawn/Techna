using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public MovingObject movingObject;
    public PlayerMovement player; // 플레이어

    public GameObject[] buttons;  // 버튼들
    public int checkCount;  // 확인 수
    public int currentCheckCount;

    private void Start()
    {
        currentCheckCount = checkCount;
    }

    private void Update()
    {
        if (currentCheckCount == 0)
        {
            bool allMatch = true;

            foreach (GameObject buttonObj in buttons)
            {
                ButtonInfor buttonInfo = buttonObj.GetComponent<ButtonInfor>();
                if (buttonInfo.trueButton != buttonInfo.currentStatus)
                {
                    allMatch = false;
                    break;
                }
            }

            if (allMatch)
            {
                movingObject.activated = true;
            }
            else
            {
                currentCheckCount = checkCount;
                player.currentHealth -= 5;
            }

            // 모든 버튼의 currentStatus를 false로 초기화
            foreach (GameObject buttonObj in buttons)
            {
                ButtonInfor buttonInfo = buttonObj.GetComponent<ButtonInfor>();
                buttonInfo.currentStatus = false;
                buttonInfo.renderer.material = buttonInfo.materials[0];
            }
        }
    }
}
