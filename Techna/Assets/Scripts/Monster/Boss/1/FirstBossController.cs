using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossController : MonoBehaviour
{
    public FirstBossAnimationController animationController;
    public FirstBossStage firstBossStage;

    public GameObject player; // 플레이어

    public float speed; // 이동속도
    public float rotateSpeed; // 회전속도
    public int maxHealth; // 최대 체력
    public int currentHealth; // 현재 체력

    public GameObject shield; // 보호막

    private void Awake()
    {
        animationController = GetComponent<FirstBossAnimationController>();
    }

    private void Start()
    {
        speed = 10;

        // animationController.Rest();

        AttackPose();
    }

    void CenterMove() // 중앙으로 이동
    {
        StartCoroutine(MoveToPosition(4));
    }

    void PositionReset() // 초기 위치로 이동
    {
        StartCoroutine(MoveToPosition(7));
    }
    
    IEnumerator MoveToPosition(int position)
    {
        animationController.Walk(); // 이동 애니메이션

        // 지정 위치로 이동
        while (Vector3.Distance(transform.position, firstBossStage.bossMovePosition[position].position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstBossStage.bossMovePosition[position].position, speed * Time.deltaTime);
            yield return null;
        }

        animationController.Halt();
    }

    void Crouch() // 웅크리기 (방어)
    {
        animationController.Crouch(); // 웅크리기 애니메이션
        shield.SetActive(true); // (보호막 활성화)

        StartCoroutine(UnCrouch()); // 웅크리기 해제
    }
    
    IEnumerator UnCrouch() // 웅크리기 해제
    {
        yield return new WaitForSeconds(5);

        if (animationController.isCrouch) // 웅크리기 중 일시 해제
        {
            animationController.Crouch();
            shield.SetActive(false);
        }
    }

    void AttackCrouch() // 웅크리기 (공격)
    {
        StartCoroutine(AttackCrouchRoutine());
    }

    IEnumerator AttackCrouchRoutine() // 웅크린후 회전하여 돌진 후 돌아가기
    {
        rotateSpeed = 1000f; // 회전속도 설정
        Quaternion initialRotation = transform.rotation; // 초기 회전 값 저장

        animationController.Crouch(); // 웅크리기 애니메이션 시작
        yield return new WaitForSeconds(2);

        // 이동 및 회전
        while (Vector3.Distance(transform.position, player.transform.position) > 0.1f)
        {
            // Y축 기준 회전
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

            // 플레이어 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (speed * 1.5f) * Time.deltaTime);
            yield return null;
        }

        // 원래 위치로 돌아가기
        Vector3 originalPosition = firstBossStage.bossMovePosition[7].position;
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            // 돌아가면서 계속 회전
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (speed * 1.5f) * Time.deltaTime);
            yield return null;
        }

        // 도착 후 천천히 회전 감소 및 원래 회전값으로 정확히 멈추기
        float decelerationDuration = 2f;
        float decelerationStartTime = Time.time;
        Quaternion finalRotation = initialRotation;

        while (Quaternion.Angle(transform.rotation, finalRotation) > 0.01f)
        {
            float t = (Time.time - decelerationStartTime) / decelerationDuration;
            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, t);
            yield return null;
        }

        transform.rotation = finalRotation; // 마지막으로 원래 회전 값으로 설정

        // 웅크리기 해제
        rotateSpeed = 0f; // 회전속도 설정
        animationController.Crouch(); // 웅크리기 해제
    }

    void AttackPose() // 공격 자세
    {
        animationController.AttackPose();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string[] collisionBullet = new string[] { "Bullet", "Expansion", "Penetrate" };

        if (System.Array.Exists(collisionBullet, tag => tag == collision.gameObject.tag))
        {
            currentHealth -= 5;

            animationController.TakeDamage(); // 피격 (확인필요)
        }
    }
}

    
