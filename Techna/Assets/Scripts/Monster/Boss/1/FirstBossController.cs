using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstBossController : MonoBehaviour
{
    public FirstBossAnimationController animationController;
    public FirstBossStage firstBossStage;
    private PlayerMovement playerMovement;

    public GameObject player; // 플레이어

    public float speed; // 이동속도
    public float rotateSpeed; // 회전속도
    public float rotateAttackSpeed; // 회전 공격속도
    public int maxHealth; // 최대 체력
    public int currentHealth; // 현재 체력
    public Image healthBar; // 체력바
    public bool dying; // 사망여부

    public bool attackPose; // 공격 준비

    public int attackNum; // 공격 횟수
    public GameObject attackBullet; // 공격총알
    public Transform bulletPosition; // 총알 발사 위치
    public int bulletSpeed; // 총알속도

    public bool onShield; // 보호막 활성화
    public GameObject shield; // 보호막
    public int shieldCount; // 보호막 해체 카운트 
    public Transform trapPosition; // 함정 공격 위치

    public GameObject flooringEffect; // 바닥 장판 이펙트
    public GameObject explosionEffect; // 폭발 이펙트

    public GameObject dropCube; // 스킬 큐브

    public bool watching; // 플레이어 주시 여부
    public bool bossCenterPosition; // 보스 위치정보 - false : 초기위치(firstBossStage.bossMapPosition[7]), true : 중앙위치(firstBossStage.bossMapPosition[5])

    public GameObject clearItem; // 클리어 보상

    public bool moving; // 움직이는중

    private void Awake()
    {
        animationController = GetComponent<FirstBossAnimationController>();
    }

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        speed = 10;
        rotateSpeed = 10;
        
        maxHealth = 300;
        currentHealth = maxHealth;

        watching = true;
    }

    private void Update()
    {
        if (!dying) // 사망 x
        {
            if (watching)
            {
                WatchPlayer(); // 플레이어 주시
            }

            HealthUpdate(); // 체력바 업데이트

            if (currentHealth <= 0)
            {
                Die(); // 사망
            }
        }

        if (!moving)
        {
            moving = true;
            StartCoroutine(StartPattern());
        }
    }

    IEnumerator StartPattern()
    {
        yield return new WaitForSeconds(2f);

        int num = Random.Range(0, 2);
        switch (num)
        {
            case 0:
                NormalPose();
                break;
            case 1:
                AttackPose();
                break;
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

    void NormalPose()
    {
        int num = Random.Range(0, 5);
        switch (num)
        {
            case 0:
                BossMoving(); // 보스 이동
                break;
            case 1: case 3:
                Crouch(); // 웅크리기 (방어)
                break;
            case 2: case 4:
                AttackCrouch(); // 웅크리기 (공격)
                break;
        }
    }

    void BossMoving()
    {
        if (bossCenterPosition)
        {
            PositionReset();
        }
        else
        {
            CenterMove();
        }
    }


    // (위치이동 1)
    void CenterMove() // 중앙으로 이동
    {
        bossCenterPosition = true;
        animationController.Walk(); // 이동 애니메이션
        StartCoroutine(MoveToPosition(4));
    }

    // (위치이동 2)
    void PositionReset() // 초기 위치로 이동
    {
        bossCenterPosition = false;
        animationController.Walk(); // 이동 애니메이션
        StartCoroutine(MoveToPosition(7));
    }
    
    IEnumerator MoveToPosition(int position)
    {
        // 지정 위치로 이동
        while (Vector3.Distance(transform.position, firstBossStage.bossMapPositions[position].position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstBossStage.bossMapPositions[position].position, speed * Time.deltaTime);
            yield return null;
        }

        animationController.Halt();
        moving = false;
    }

    // (웅크리기 방어)
    void Crouch() // 웅크리기 (방어)
    {
        animationController.Crouch(); // 웅크리기 애니메이션

        StartCoroutine(UnCrouch()); // 웅크리기 해제
    }

    IEnumerator TrapAttack(float time)
    {
        while (onShield)
        {
            BulletAttack(trapPosition.transform.position); // 공격 실행      
            yield return new WaitForSeconds(time); // 대기
        }
    }

    IEnumerator UnCrouch() // 방어막 생성 잠시후 해제
    {
        yield return new WaitForSeconds(1);

        shield.SetActive(true); // (보호막 활성화)
        onShield = true; // 보호막 활성화
        shieldCount = 5; // 쉴드 카운트 설정

        bulletSpeed = 30;
        StartCoroutine(TrapAttack(2f));

        yield return new WaitForSeconds(20);

        if (onShield)
        {
            animationController.Halt();
            shield.SetActive(false);
            onShield = false;
            moving = false;
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

        // 플레이어 위치 찾기 (Y축 고정)
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        // 이동 및 회전
        while (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            if (dying)
            {
                break;
            }

            // Y축 기준 회전
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);

            // 플레이어 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed * 2.5f) * Time.deltaTime);
            yield return null;
        }

        int bossPosition = bossCenterPosition ? 4 : 7; 

        // 원래 위치로 돌아가기
        Vector3 originalPosition = new Vector3(firstBossStage.bossMapPositions[bossPosition].position.x, transform.position.y, firstBossStage.bossMapPositions[bossPosition].position.z);
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            if (dying)
            {
                break;
            }

            // 돌아가면서 계속 회전
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (speed * 2.5f) * Time.deltaTime);
            yield return null;
        }

        // 도착 후 천천히 회전 감소 및 원래 회전값으로 정확히 멈추기
        float decelerationDuration = 2f;
        float decelerationStartTime = Time.time;
        Quaternion finalRotation = initialRotation;

        while (Quaternion.Angle(transform.rotation, finalRotation) > 0.01f)
        {
            if (dying)
            {
                break;
            }

            float t = (Time.time - decelerationStartTime) / decelerationDuration;
            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, t);
            yield return null;
        }

        transform.rotation = finalRotation; // 마지막으로 원래 회전 값으로 설정

        // 웅크리기 해제
        rotateAttackSpeed = 0f; // 회전속도 설정
        animationController.Halt(); // 웅크리기 해제
        watching = true;
        moving = false;
    }

    // (던지기 공격)
    void Throw() // 던지기
    {
        animationController.Throw();

        ThrowAttack();
    }

    void ThrowAttack() // 던지기 공격
    {
        int bossPosition = bossCenterPosition ? 4 : 7; // 보스의 현재 위치 인덱스

        // SkillPositions 배열을 순회하며 큐브 생성
        for (int i = 0; i < firstBossStage.SkillPositions.Length; i++)
        {
            if (i != bossPosition) // 보스의 위치와 다른 위치에만 큐브 생성
            {
                Vector3 spawnPosition = firstBossStage.SkillPositions[i].position; // 큐브 생성 위치
                GameObject cube = Instantiate(dropCube, spawnPosition, Quaternion.identity); // 큐브 인스턴스화

                Destroy(cube, 12);
            }
        }
        moving = false;
    }

    // ↑ 일반 자세 ------------ ↓ 공격자세

    // (공격)
    void AttackPose() // 공격 자세
    {      
        animationController.AttackPose();
        attackPose = true;

        int num = Random.Range(0, 3);
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
        bulletSpeed = 50;
        yield return new WaitForSeconds(2f);
        attackNum = 3;     

        StartCoroutine(RepeatBulletAttack(1f));
    }

    // (빠른 공격)
    IEnumerator QuickAttackReady() // 빠른 공격 준비
    {
        bulletSpeed = 30;
        yield return new WaitForSeconds(2f);
        attackNum = 10;

        StartCoroutine(RepeatBulletAttack(0.5f));
    }

    IEnumerator RepeatBulletAttack(float time) // 반복 총알 공격
    {
        while (attackNum >= 0)
        {
            if(dying)
            {
                break;
            }

            BulletAttack(bulletPosition.transform.position); // 공격 실행      
            yield return new WaitForSeconds(time); // 대기
        }
        animationController.Halt(); // 공격 모드 해제
        moving = false;
    }

    

    void BulletAttack(Vector3 position) // 총알 공격
    {
        animationController.Attack();
        attackNum--;
        GameObject bullet = Instantiate(attackBullet, position, Quaternion.identity);
        Destroy(bullet, 4f);

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

        int bossPosition = bossCenterPosition ? 4 : 7;

        // 4개의 중복되지 않는 위치를 선택, 보스위치 제외 (현재 [7])
        while (selectedPositions.Count < 4)
        {
            int randomIndex = Random.Range(0, firstBossStage.bossMapPositions.Length);
            if (randomIndex != bossPosition)
            {
                selectedPositions.Add(randomIndex);
            }
        }

        // 선택된 4개의 위치에 바닥장판 생성
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPositions[positionIndex].position.x, firstBossStage.bossMapPositions[positionIndex].position.y + 0.2f, firstBossStage.bossMapPositions[positionIndex].position.z);
            GameObject flooring = Instantiate(flooringEffect, effectPosition, Quaternion.identity);
            Destroy(flooring, 7);
        }

        StartCoroutine(MoveSkillWalls(0)); // 스킬벽 이동
        yield return new WaitForSeconds(6f);

        // 선택된 4개의 위치에 폭발 생성
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPositions[positionIndex].position.x, firstBossStage.bossMapPositions[positionIndex].position.y + 0.2f, firstBossStage.bossMapPositions[positionIndex].position.z);
            GameObject explosion = Instantiate(explosionEffect, effectPosition, Quaternion.identity);
            Destroy(explosion, 1);
        }

        StartCoroutine(MoveSkillWalls(-20)); // 스킬벽 리셋

        animationController.Halt(); // 공격 모드 해제
        moving = false;
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
            while (Mathf.Abs(skillWall.transform.position.y - targetY) > 1f) // 오차 허용
            {
                skillWall.transform.position = Vector3.MoveTowards(skillWall.transform.position, targetPosition, Time.deltaTime * 200f);
                yield return null;
            }
        }
    }

    void Die() // 사망
    {
        dying = true; // 사망처리

        animationController.Die();

        StartCoroutine(DestroyBoss());
    }

    IEnumerator DestroyBoss()
    {
        firstBossStage.clearWall.SetActive(true);

        yield return new WaitForSeconds(4f);

        clearItem.SetActive(true);  

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    // (피격)
    private void OnTriggerEnter(Collider other)
    {
        string[] collisionBullet = new string[] { "Bullet", "Expansion", "Penetrate" };

        if (onShield)
        {
            if (other.gameObject.CompareTag("Penetrate"))
            {
                if (shieldCount > 0)
                {
                    shieldCount--;

                    if (shieldCount == 0)
                    {
                        shield.SetActive(false);

                        animationController.Halt();
                        shield.SetActive(false);
                        onShield = false;
                        moving = false;
                    }
                }
            }
        }
        else
        {
            if (System.Array.Exists(collisionBullet, tag => tag == other.gameObject.tag))
            {
                ProjectileMoveScript bullet = other.gameObject.GetComponent<ProjectileMoveScript>();
                currentHealth -= bullet.damage;
            }
        }
    }

    void HealthUpdate() // 체력바 업데이트
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }
}

    
