using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectPosition : MonoBehaviour
{
    public Transform resetPos; // 리셋 위치
    public bool objectRestrictions; // 오브젝트 이동 제한

    private void OnTriggerEnter(Collider other)
    {
        if (objectRestrictions)
        {
            if (other.CompareTag("Player"))
            {  
                GrabObject obj = other.gameObject.GetComponent<GrabObject>();
                if (obj.grabbedObject != null)
                {
                    Rigidbody grabbedRigidbody = obj.grabbedObject.GetComponent<Rigidbody>();
                    if (grabbedRigidbody != null)
                    {
                        grabbedRigidbody.freezeRotation = false; // 회전 고정 해제
                        grabbedRigidbody.isKinematic = false; // 물리 효과 다시 활성화
                    }

                    obj.grab = false;
                    obj.grabbedObject = null;
                }
            }
            else if (other.CompareTag("GrabObject")) // 일부 오브젝트가 던져서 넘어가는 상황 대비
            {
                Rigidbody grabbedRigidbody = other.GetComponent<Rigidbody>();
                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.velocity = Vector3.zero; // 속도 초기화
                    grabbedRigidbody.angularVelocity = Vector3.zero; // 회전 초기화
                }
            }
        }
        else
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
}
