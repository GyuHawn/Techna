using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectPosition : MonoBehaviour
{
    public Transform resetPos; // 리셋 위치

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabObject") || other.CompareTag("Player"))
        {     
            if (other.CompareTag("Player"))  // 플레이어
            {
                CharacterController characterController = other.GetComponent<CharacterController>();
                characterController.enabled = false;
                other.transform.position = resetPos.position; 
                characterController.enabled = true;
            }
            else // 오브젝트
            {
                // 오브젝트 이동
                other.transform.position = resetPos.position;
            }
        }
    }
}
