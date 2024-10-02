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

    public bool attackPose; // 공격 준비

    public int attackNum; // 공격 횟수
    public GameObject attackBullet; // 공격총알
    public Transform bulletPosition; // 총알 발사 위치
    public int bulletSpeed; // 총알속도

    public GameObject shield; // 보호막

    public GameObject flooringEffect; // 바닥 장판 이펙트
    public GameObject explosionEffect; // 폭발 이펙트

    public bool watching; // 플레이어 주시 여부
    public bool bossCenterPosition; // 보스 위치정보 - false : 초기위치(firstBossStage.bossMapPosition[7]), true : 중앙위치(firstBossStage.bossMapPosition[5])

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

        watching = true;

        AttackCrouch();
    }

    private void Update()
    {
        if (watching)
        {
            WatchPlayer(); // 플레이어 주시
        }
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

    // (위치이동 1)
    void CenterMove() // 중앙으로 이동
    {
        StartCoroutine(MoveToPosition(4));
    }

    // (위치이동 2)
    void PositionReset() // 초기 위치로 이동
    {
        StartCoroutine(MoveToPosition(7));
    }
    
    IEnumerator MoveToPosition(int position)
    {
        animationController.Walk(); // 이동 애니메이션

        // 지정 위치로 이동
        while (Vector3.Distance(transform.position, firstBossStage.bossMapPosition[position].position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstBossStage.bossMapPosition[position].position, speed * Time.deltaTime);
            yield return null;
        }

        animationController.Halt();
    }

    // (웅크리기 방어)
    void Crouch() // 웅크리기 (방어)
    {
        animationController.Crouch(); // 웅크리기 애니메이션

        StartCoroutine(UnCrouch()); // 웅크리기 해제
    }
    
    IEnumerator UnCrouch() // 방어막 생성 잠시후 해제
    {
        yield return new WaitForSeconds(1);
        shield.SetActive(true); // (보호막 활성화)
        yield return new WaitForSeconds(5);

        if (animationController.isCrouch) // 웅크리기 중 일시 해제
        {
            animationController.Crouch();
            shield.SetActive(false);
        }
    }

    // (웅크리기 공격)
    void AttackCrouch() // 웅크리기 (공격)
    {
        watching = false;
        StartCoroutine(AttackCrouchRoutine());
    }

    IEnumerator AttackCrouchRoutine() // 웅크린 후 회전하여 돌진 후 돌아가기
    {
        rotateAttackSpeed = 1000f; // 회전속도 설정
        Quaternion initialRotation = transform.rotation; // 초기 회전 값 저장

        animationController.Crouch(); // 웅크리기 애니메이션 시작
        yield return new WaitForSeconds(2);

        // 이동 및 회전
        while (Vector3.Distance(transform.position, player.transform.position) > 1f)
        {
            // Y축 고정한 상태에서 이동
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            // Y축 기준 회전
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);

            // 플레이어 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed * 2f) * Time.deltaTime);
            yield return null;
        }

        // 원래 위치로 돌아가기
        Vector3 originalPosition = new Vector3(firstBossStage.bossMapPosition[7].position.x, transform.position.y, firstBossStage.bossMapPosition[7].position.z);
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            // 돌아가면서 계속 회전
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (speed * 2f) * Time.deltaTime);
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
        watching = true;
    }


    // (공격)
    void AttackPose() // 공격 자세
    {      
        animationController.AttackPose();
        attackPose = true;

        //int num = Random.Range(0, 3);
        int num = 1;
        switch (num)
        {
            case 0:
                StartCoroutine(BasicAttackReady());
                break;
            case 1:
                StartCoroutine(QuickAttackReady());
                break;
            case 2:
                StartCoroutine(TauntAttackReady());
                break;
        }
    }

    // (기본 공격)
    IEnumerator BasicAttackReady() // 공격 준비
    {
        yield return new WaitForSeconds(2f);
        attackNum = 3;     

        StartCoroutine(RepeatBulletAttack(1f));
    }

    // (빠른 공격)
    IEnumerator QuickAttackReady() // 빠른 공격 준비
    {
        yield return new WaitForSeconds(2f);
        attackNum = 10;

        StartCoroutine(RepeatBulletAttack(0.5f));
    }

    IEnumerator RepeatBulletAttack(float time) // 반복 총알 공격
    {
        while (attackNum >= 0)
        {
            BulletAttack(); // 공격 실행
            
            yield return new WaitForSeconds(time); // 대기
        }
        animationController.AttackPose(); // 공격 모드 해제
    }

    void BulletAttack() // 총알 공격
    {
        animationController.Attack();
        attackNum--;
        GameObject bullet = Instantiate(attackBullet, bulletPosition.position, Quaternion.identity);
        Vector3 direction = (player.transform.position - bullet.transform.position).normalized;
        bullet.transform.rotation = Quaternion.LookRotation(direction);
        bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
    }

    // (조롱 공격)
    IEnumerator TauntAttackReady() // 공격 준비
    {
        yield return new WaitForSeconds(2f);

        animationController.Taunt(); // 조롱 애니메이션 실행

        yield return new WaitForSeconds(1f);

        StartCoroutine(CreateFlooring()); // 바닥 장판 생성
    }

    IEnumerator CreateFlooring() // 바닥 장판 생성
    {
        HashSet<int> selectedPositions = new HashSet<int>(); // 바닥 위치 리스트

        // 4개의 중복되지 않는 위치를 선택, 보스위치 제외 (현재 [7])
        while (selectedPositions.Count < 4)
        {
            int randomIndex = Random.Range(0, firstBossStage.bossMapPosition.Length);
            if (randomIndex != 7)
            {
                selectedPositions.Add(randomIndex);
            }
        }

        // 선택된 4개의 위치에 바닥장판 생성
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPosition[positionIndex].position.x, firstBossStage.bossMapPosition[positionIndex].position.y + 0.2f, firstBossStage.bossMapPosition[positionIndex].position.z);
            GameObject flooring = Instantiate(flooringEffect, effectPosition, Quaternion.identity);
            Destroy(flooring, 7);
        }

        StartCoroutine(MoveSkillWalls(0)); // 스킬벽 이동
        yield return new WaitForSeconds(6f);

        // 선택된 4개의 위치에 폭발 생성
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPosition[positionIndex].position.x, firstBossStage.bossMapPosition[positionIndex].position.y + 0.2f, firstBossStage.bossMapPosition[positionIndex].position.z);
            GameObject explosion = Instantiate(explosionEffect, effectPosition, Quaternion.identity);
            Destroy(explosion, 1);
        }

        StartCoroutine(MoveSkillWalls(-20)); // 스킬벽 리셋

        animationController.AttackPose(); // 공격 모드 해제
    }

    // 스킬벽 이동
    IEnumerator MoveSkillWalls(float targetY)
    {
        yield return new WaitForSeconds(1.5f); // 대기 시간

        foreach (GameObject skillWall in firstBossStage.SkillWalls)
        {
            if (skillWall == null) continue;

            Vector3 targetPosition = new Vector3(skillWall.transform.position.x, targetY, skillWall.transform.position.z);

            // y 목표값에 도달할 때까지 이동
            while (Mathf.Abs(skillWall.transform.position.y - targetY) > 0.01f) // 오차 허용
            {
                skillWall.transform.position = Vector3.MoveTowards(skillWall.transform.position, targetPosition, Time.deltaTime * 200f);
                yield return null;
            }
        }
    }

    // (피격)
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

    
