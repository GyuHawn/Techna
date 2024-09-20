using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Transform grabbedObject; // 현재 잡고 있는 물체
    private Vector3 lastValidPosition; // 마지막 유효한 위치 저장
    public Transform holdPosition; // 물체를 잡을 위치
    public bool grab; // 잡았는지 여부
    public float grabRange; // 잡을 수 있는 최대 거리
    public float throwForce; // 던질 때 힘

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
                lastValidPosition = grabbedObject.position; // 초기 위치 저장
                grabbedObject.position = holdPosition.position; // 물체 위치 설정

                // 회전 고정
                Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.freezeRotation = true; // 회전 고정
                }

                grab = true; // 잡은 상태로 변경
            }
        }

        // 잡은 물건의 무게가 2 이상일 때 바로 놓기
        if (grabbedObject != null)
        {
            CheckObjectInfor cube = grabbedObject.GetComponent<CheckObjectInfor>();
            if (cube.weight > 2)
            {
                ReleaseObject();
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
            }

            grabbedObject = null; // 잡은 물체 초기화
            grab = false; // 잡지 않은 상태로 변경
        }
    }

    void TryThrowObject() // 던지기 시도
    {
        if (grabbedObject != null) // 잡은 물체가 있을 때
        {
            CheckObjectInfor cube = grabbedObject.GetComponent<CheckObjectInfor>();

            // 충돌 중이면 던지지 않음
            if (!cube.colliding)
            {
                ThrowObject(); // 던지기
            }
        }
    }

    void ThrowObject() // 실제 던지기 실행
    {
        Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.freezeRotation = false; // 던지기 전에 회전 고정 해제
            grabbedRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse); // 던지기
        }

        grabbedObject = null; // 잡은 물체 초기화
        grab = false; // 잡지 않은 상태로 변경
    }

    void UpdateGrabObjectPosition() // 물체 위치 업데이트
    {
        if (grabbedObject != null)
        {
            CheckObjectInfor cube = grabbedObject.GetComponent<CheckObjectInfor>();

            // 충돌 여부에 따라 위치 업데이트
            if (!cube.colliding) // 충돌하지 않는 경우
            {
                lastValidPosition = grabbedObject.position; // 마지막 유효한 위치 저장
                grabbedObject.position = holdPosition.position; // 물체 위치 업데이트
            }
            else // 충돌 중일 경우
            {
                grabbedObject.position = lastValidPosition; // 마지막 유효한 위치로 되돌림
                cube.colliding = false; // colliding을 false로 변경
            }
        }
    }
}
