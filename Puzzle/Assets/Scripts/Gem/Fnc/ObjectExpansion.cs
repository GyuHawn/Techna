using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExpansion : MonoBehaviour
{
    public ExpansionConversion gem; // 보석 상태 확인
    public float scaleChangeDuration = 5.0f; // 스케일 변화 시간
    public float freezeDuration = 5.0f; // 오브젝트 고정 시간

    private void Start()
    {
        gem = GameObject.Find("Gun").GetComponent<ExpansionConversion>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string[] checkTags = { "GrabObject" }; // 필요시 추가

        if (collision.gameObject.CompareTag("GrabObject"))
        {
            CheckCubeInfor check = collision.gameObject.GetComponent<CheckCubeInfor>();

            if (check.expansion)
            {
                // 오브젝트의 크기 증감
                StartCoroutine(HandleCollision(collision.gameObject));
            }
            else { }
        }
    }

    void ScaleOverTime(GameObject obj, Vector3 targetScale, float duration) // 크기 증감
    {
        Vector3 initialScale = obj.transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
        }

        // 최종 스케일 설정
        obj.transform.localScale = targetScale;
    }

    private IEnumerator HandleCollision(GameObject hitObject) // 크기 증감중 포지션 및 회전 고정, 오브젝트 최대 증감 값 확인
    {
        Rigidbody rb = hitObject.GetComponent<Rigidbody>();

        // 위치, 회전 저장
        Vector3 originalPosition = hitObject.transform.position;
        Quaternion originalRotation = hitObject.transform.rotation;

        CheckCubeInfor check = hitObject.GetComponent<CheckCubeInfor>();
        // 크기 증감
        if (gem.plus)
        {
            if (check.currentValue < check.expansValue)
            {
                check.currentValue++;
                ScaleOverTime(hitObject, hitObject.transform.localScale * 2, scaleChangeDuration);
                check.weight = check.weight * 2;
            }
        }
        else
        {
            if (check.currentValue > check.reducedValue)
            {
                check.currentValue--;
                ScaleOverTime(hitObject, hitObject.transform.localScale * 0.5f, scaleChangeDuration);
                check.weight = check.weight * 0.5f;
            }
        }

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

        // 저장된 위치, 회전으로 복원
        hitObject.transform.position = originalPosition;
        hitObject.transform.rotation = originalRotation;
    }
}
