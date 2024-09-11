using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectPosition : MonoBehaviour
{
    public Transform resetPos; // 리셋 위치

    private void OnTriggerEnter(Collider other)
    {
        // 잡은 오브젝트, 플레이어가 충돌시 리셋 위치로 이동
        if (other.CompareTag("GrabObject") || other.CompareTag("Player"))
        {
            // 플레이어가 CharacterController를 사용하고 있는 경우
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false; // CharacterController 비활성화
                other.transform.position = resetPos.position; // 위치 이동
                characterController.enabled = true; // CharacterController 재활성화
            }
            else
            {
                // 일반 오브젝트는 transform.position으로 이동
                other.transform.position = resetPos.position;
            }
        }
    }
}
