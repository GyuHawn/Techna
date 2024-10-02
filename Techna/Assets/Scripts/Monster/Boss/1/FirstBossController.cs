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
    public float rotateAttackSpeed; // 회전 공격속도
    public int maxHealth; // 최대 체력
    public int currentHealth; // 현재 체력
    public int damage; // 데이지

    public bool attackPose;

    public GameObject attackBullet; // 공격총알
    public Transform bulletPosition; // 총알 발사 위치
    public int bulletSpeed; // 총알속도

    public GameObject shield; // 보호막

    private void Awake()
    {
        animationController = GetComponent<FirstBossAnimationController>();
    }

    private void Start()
    {
        speed = 10;
        rotateSpeed = 10;
        bulletSpeed = 30;
        
        maxHealth = 50;
        currentHealth = maxHealth;
        damage = 10;
    }

    private void Update()
    {
        WatchPlayer(); // 플레이어 주시
    }

    void WatchPlayer() // 플레이어 주시
    {
        // 플레이어 위치 기준 보스 방향 설정
        Vector3 playerDirection = player.transform.position - transform.position;
        playerDirection.y = 0; // Y축 회전 제외

        // 부드럽게 회전
        Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
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
        rotateAttackSpeed = 1000f; // 회전속도 설정
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
        rotateAttackSpeed = 0f; // 회전속도 설정
        animationController.Crouch(); // 웅크리기 해제
    }

    void AttackPose() // 공격 자세
    {      
        animationController.AttackPose();
        StartCoroutine(AttackReady());
    }

    IEnumerator AttackReady() // 공격 준비
    {
        yield return new WaitForSeconds(2f);
        attackPose = true;

        StartCoroutine(RepeatBulletAttack());
    }
    IEnumerator RepeatBulletAttack() // 반복 총알 공격
    {
        while (attackPose)
        {
            BulletAttack(); // 공격 실행

            yield return new WaitForSeconds(2f); // 대기
        }
    }

    void BulletAttack() // 총알 공격
    {
        if (attackPose)
        {
            GameObject bullet = Instantiate(attackBullet, bulletPosition.position, Quaternion.identity);
            Vector3 direction = (player.transform.position - bullet.transform.position).normalized;
            bullet.transform.rotation = Quaternion.LookRotation(direction);
            bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
        }
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

    
