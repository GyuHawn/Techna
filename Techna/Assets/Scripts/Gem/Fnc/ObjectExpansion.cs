using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExpansion : MonoBehaviour
{
    public ExpansionConversion gun; // 보석 상태 확인
    public float scaleChangeDuration; // 스케일 변화 시간
    public float freezeDuration; // 오브젝트 고정 시간
    private bool isScaling = false; // 크기 변화 중인지 여부를 추적하는 변수

    private void Start()
    {
        gun = GameObject.Find("Gun").GetComponent<ExpansionConversion>();
        scaleChangeDuration = 2f;
        freezeDuration = 3f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ExpansionBullet")) // 증감 속성 총알
        {
            CheckObjectInfor cube = gameObject.GetComponent<CheckObjectInfor>();
            if (cube.expansion)
            {
                // 오브젝트의 크기 증감
                HandleCollision();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ExpansionBullet")) // 증감 속성 총알
        {
            CheckObjectInfor cube = gameObject.GetComponent<CheckObjectInfor>();
            if (cube.expansion)
            {
                // 오브젝트의 크기 증감
                HandleCollision();
            }
        }
    }

    void HandleCollision() // 크기 증감 중 포지션 및 회전 고정, 오브젝트 최대 증감 값 확인
    {
        if (isScaling) return; // 이미 크기 변화 중이면 함수 종료

        gameObject.tag = "Untagged"; // 플레이어가 잡을수 없도록 태그 변경

         // 위치, 회전 저장
        Vector3 originalPosition = gameObject.transform.position;
        Quaternion originalRotation = gameObject.transform.rotation;

        CheckObjectInfor check = gameObject.GetComponent<CheckObjectInfor>();

        if (gun.plus) // 크기 증가
        {
            if (check.currentValue < check.expansValue)
            {
                check.currentValue++;
                isScaling = true; // 크기 변화 시작
                StartCoroutine(ScaleOverTime(gameObject, gameObject.transform.localScale * 2));
                check.weight = check.weight * 2;
            }
        }
        else // 크기 감소
        {
            if (check.currentValue > check.reducedValue)
            {
                check.currentValue--;
                isScaling = true; // 크기 변화 시작
                StartCoroutine(ScaleOverTime(gameObject, gameObject.transform.localScale * 0.5f));
                check.weight = check.weight * 0.5f;
            }
        }

        StartCoroutine(FixedPostion()); // 포지션 고정

        // 저장된 위치, 회전으로 복원
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
    }

    IEnumerator ScaleOverTime(GameObject obj, Vector3 targetScale)
    {
        Vector3 initialScale = obj.transform.localScale;  // 오브젝트 초기 스케일 저장
        float initialYPos = obj.transform.position.y;  // 오브젝트 초기 Y 위치값 저장
        float elapsed = 0f;  // 경과 시간을 추적

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // 물리 상호작용을 멈춤
        }

        while (elapsed < scaleChangeDuration)
        {
            float progress = elapsed / scaleChangeDuration;  //스케일 변화 진행도 계산

            // 스케일을 초기 스케일에서 목표 스케일까지 보간하여 점진적으로 변화
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);

           // 현재 스케일의 Y값을 가져와서 스케일 변화량을 계산
            float currentScaleY = obj.transform.localScale.y;
            float scaleChangeY = currentScaleY - initialScale.y;

            // Y축 위치를 현재 스케일 변화에 맞춰서 절반+1 만큼 올림
            obj.transform.position = new Vector3(obj.transform.position.x, initialYPos + ((scaleChangeY / 2) + 1), obj.transform.position.z);

            elapsed += Time.deltaTime;  // 경과 시간 업데이트
            yield return null;
        }

        // 최종 스케일을 목표 스케일로 설정
        obj.transform.localScale = targetScale;

        // 최종 스케일 변화량을 기반으로 Y 위치를 조정
        float finalScaleChangeY = targetScale.y - initialScale.y;
        obj.transform.position = new Vector3(obj.transform.position.x, initialYPos + (finalScaleChangeY / 2), obj.transform.position.z);

        // 물리 상호작용을 복원
        if (rb != null)
        {
            rb.isKinematic = false;  // 물리 상호작용을 다시 활성화
        }

        gameObject.tag = "GrabObject"; // 태그 복구
        isScaling = false;  // 스케일 변화가 완료 표시
    }



    IEnumerator FixedPostion() // 포지션 고정
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        // 포지션과 회전을 고정
        if (rb != null)
        {
            // 기존 속도, 회전력 저장
            Vector3 originalVelocity = rb.linearVelocity;
            Vector3 originalAngularVelocity = rb.angularVelocity;

            // 포지션, 회전 고정 (속도와 회전력을 0으로 설정)
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 일정 시간 동안 포지션, 회전을 고정
            yield return new WaitForSeconds(freezeDuration);

            // 원래 속도, 회전력 복원
            rb.linearVelocity = originalVelocity;
            rb.angularVelocity = originalAngularVelocity;
        }
        else
        {
            // Kinematic ������ ���
            yield return new WaitForSeconds(freezeDuration);
        }
    }
}
