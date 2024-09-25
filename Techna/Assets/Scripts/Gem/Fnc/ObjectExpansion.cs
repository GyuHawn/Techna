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
        if (collision.gameObject.CompareTag("ExpansionBullet"))
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

        // 위치, 회전 저장
        Vector3 originalPosition = gameObject.transform.position;
        Quaternion originalRotation = gameObject.transform.rotation;

        CheckObjectInfor check = gameObject.GetComponent<CheckObjectInfor>();
        // 크기 증감
        if (gun.plus)
        {
            if (check.currentValue < check.expansValue)
            {
                check.currentValue++;
                isScaling = true; // 크기 변화 시작
                StartCoroutine(ScaleOverTime(gameObject, gameObject.transform.localScale * 2));
                check.weight = check.weight * 2;
            }
        }
        else
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

    IEnumerator ScaleOverTime(GameObject obj, Vector3 targetScale) // 크기 증감
    {
        Vector3 initialScale = obj.transform.localScale;
        float elapsed = 0f;

        while (elapsed < scaleChangeDuration)
        {
            float progress = elapsed / scaleChangeDuration;
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 최종 스케일 설정
        obj.transform.localScale = targetScale;
        isScaling = false; // 크기 변화 완료
    }

    IEnumerator FixedPostion() // 포지션 고정
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        // 포지션과 회전을 고정
        if (rb != null)
        {
            // 기존 속도, 회전력 저장
            Vector3 originalVelocity = rb.velocity;
            Vector3 originalAngularVelocity = rb.angularVelocity;

            // 포지션, 회전 고정 (속도와 회전력을 0으로 설정)
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 일정 시간 동안 포지션, 회전을 고정
            yield return new WaitForSeconds(freezeDuration);

            // 원래 속도, 회전력 복원
            rb.velocity = originalVelocity;
            rb.angularVelocity = originalAngularVelocity;
        }
        else
        {
            // Kinematic 상태일 경우
            yield return new WaitForSeconds(freezeDuration);
        }
    }
}
