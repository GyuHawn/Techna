using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Transform grabbedObject; // 현재 잡고 있는 물체
    public bool grab; // 잡았는지 여부
    public float grabRange; // 잡을 수 있는 최대 거리
    public float throwForce; // 던질 때 힘
    private Vector3 holdOffset = new Vector3(0, 4, 0); // 물체를 잡는 높이 오프셋

    void Start()
    {
        grab = false;
        grabRange = 8.0f;
        throwForce = 20f;
    }

    void Update()
    {
        if (Input.GetButtonDown("Grab")) // F키를 눌러 잡기 (기본 F키로 설정) 
        {
            if (grab) // 이미 물체를 잡았을 때
            {
                ReleaseObject(); // 물체를 놓음
            }
            else
            {
                TryGrabObject(); // 물체를 잡기 시도
            }
        }

        if (Input.GetButtonDown("Throw") && grab)  // 잡은 상태에서 E키를 눌러 던지기 (기본 E키로 설정)
        {
            TryThrowObject(); // 던지기 시도
        }

        if (grab && grabbedObject != null) // 물체를 잡는 중일 때
        {
            UpdateGrabObjectPosition(); // 잡은 물체 위치 업데이트
        }
    }

    void TryGrabObject() // 잡기 시도
    {
        RaycastHit hit;
        // 카메라 전방으로 물체 감지
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, grabRange))
        {
            if (hit.collider.CompareTag("GrabObject")) // 태그 확인
            {
                grabbedObject = hit.transform; // 잡은 물체 설정
                SetObjectPosition(grabbedObject, holdOffset); // 물체 위치 설정

                // 회전 고정
                Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.freezeRotation = true; // 회전 고정
                    grabbedRigidbody.isKinematic = true; // 물리 효과 비활성화 (잡은 동안)
                }

                grab = true; // 잡은 상태로 변경
            }
        }
    }

    void ReleaseObject() // 놓기
    {
        if (grabbedObject != null) // 물체를 잡고 있을 때
        {
            Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
            if (grabbedRigidbody != null)
            {
                grabbedRigidbody.freezeRotation = false; // 회전 고정 해제
                grabbedRigidbody.isKinematic = false; // 물리 효과 다시 활성화
            }

            grabbedObject = null; // 잡은 물체 초기화
            grab = false; // 잡지 않은 상태로 변경
        }
    }

    void TryThrowObject() // 던지기 시도
    {
        if (grabbedObject != null) // 잡은 물체가 있을 때
        {
            ThrowObject(); // 던지기
        }
    }

    void ThrowObject() // 실제 던지기 실행
    {
        Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.freezeRotation = false; // 던지기 전에 회전 고정 해제
            grabbedRigidbody.isKinematic = false; // 물리 효과 다시 활성화
            grabbedRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse); // 카메라가 바라보는 방향으로 던지기
        }

        grabbedObject = null; // 잡은 물체 초기화
        grab = false; // 잡지 않은 상태로 변경
    }

    void UpdateGrabObjectPosition() // 물체 위치 업데이트
    {
        if (grabbedObject != null)
        {
            SetObjectPosition(grabbedObject, holdOffset); // 잡은 물체의 위치를 항상 업데이트
        }
    }

    void SetObjectPosition(Transform obj, Vector3 offset) // 물체 위치 설정 메서드
    {
        obj.position = gameObject.transform.position + offset;
    }
}
