using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Transform grabbedObject; // 현재 잡고 있는 물체
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
            if (grab) // 잡았을때
            {
                ReleaseObject(); // 놓기
            }
            else 
            {
                TryGrabObject(); // 잡기
            }
        }

        if (Input.GetButtonDown("Throw") && grab)  // 잡은 상태에서 E키를 눌러 던지기 (기본 E키로 설정) 
        {
            ThrowObject(); // 던지기
        }

        if (grab && grabbedObject != null) // 물체를 잡는 중일때
        {
            UpdateGrabObjectPosition(); // 잡은 물체 위치 업데이트
        }
    }

    void TryGrabObject() // 잡기
    {
        RaycastHit hit;
        // 카메라 전방으로 물체 감지
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, grabRange))
        {
            if (hit.collider.CompareTag("GrabObject")) // 태그 확인
            {
                grabbedObject = hit.transform; // 잡은 물체 설정
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true; // 움직임 중지
                grabbedObject.position = holdPosition.position; // 물체 위치 설정
                //grabbedObject.parent = holdPosition; // 물체의 부모 설정
                grab = true; // 잡은 상태로 변경
            }
        }
        
        // 잡은 물건의 무게가 2이상일때 바로 놓기
        if (grabbedObject != null)
        {

            CheckCubeInfor cube = grabbedObject.GetComponent<CheckCubeInfor>();
            if (cube.weight > 2)
            {
                ReleaseObject();
            }
        }

    }

    void ReleaseObject() // 놓기
    {
        if (grabbedObject != null) // 잡고있지 않을때
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false; // 움직임 재개
            //grabbedObject.parent = null; // 물체의 부모 설정 해제
            grabbedObject = null; // 잡은 물체 초기화
            grab = false; // 잡지 않은 상태로 변경
        }
    }

    void ThrowObject() // 던지기
    {
        if (grabbedObject != null) // 잡은 몰체가 있을때
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false; // 움직임 재게
            //grabbedObject.parent = null; // 부모 설정 해제
            // 전방으로 던지기
            grabbedObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
            grabbedObject = null; // 잡은 물체 초기화
            grab = false; // 잡지않은 상태로 변경
        }
    }

    void UpdateGrabObjectPosition()
    {
        if (grabbedObject != null)
        {
            grabbedObject.position = holdPosition.position; // 물체의 위치 계속 업데이트
        }
    }  
}
