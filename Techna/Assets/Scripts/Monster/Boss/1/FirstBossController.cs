using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstBossController : MonoBehaviour
{
    public FirstBossAnimationController animationController;
    public FirstBossStage firstBossStage;
    private PlayerMovement playerMovement;

    public GameObject player; // �÷��̾�

    public float speed; // �̵��ӵ�
    public float rotateSpeed; // ȸ���ӵ�
    public float rotateAttackSpeed; // ȸ�� ���ݼӵ�
    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public Image healthBar; // ü�¹�
    public bool dying; // �������

    public bool attackPose; // ���� �غ�

    public int attackNum; // ���� Ƚ��
    public GameObject attackBullet; // �����Ѿ�
    public Transform bulletPosition; // �Ѿ� �߻� ��ġ
    public int bulletSpeed; // �Ѿ˼ӵ�

    public bool onShield; // ��ȣ�� Ȱ��ȭ
    public GameObject shield; // ��ȣ��
    public int shieldCount; // ��ȣ�� ��ü ī��Ʈ 
    public Transform trapPosition; // ���� ���� ��ġ

    public GameObject flooringEffect; // �ٴ� ���� ����Ʈ
    public GameObject explosionEffect; // ���� ����Ʈ

    public GameObject dropCube; // ��ų ť��

    public bool watching; // �÷��̾� �ֽ� ����
    public bool bossCenterPosition; // ���� ��ġ���� - false : �ʱ���ġ(firstBossStage.bossMapPosition[7]), true : �߾���ġ(firstBossStage.bossMapPosition[5])

    public GameObject clearItem; // Ŭ���� ����

    public bool moving; // �����̴���

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
        if (!dying) // ��� x
        {
            if (watching)
            {
                WatchPlayer(); // �÷��̾� �ֽ�
            }

            HealthUpdate(); // ü�¹� ������Ʈ

            if (currentHealth <= 0)
            {
                Die(); // ���
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


    void WatchPlayer() // �÷��̾� �ֽ�
    {
        // �÷��̾� ��ġ ���� ���� ���� ����
        Vector3 playerDirection = player.transform.position - transform.position;
        playerDirection.y = 0; // Y�� ȸ�� ����

        // �ε巴�� ȸ��
        Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    void NormalPose()
    {
        int num = Random.Range(0, 5);
        switch (num)
        {
            case 0:
                BossMoving(); // ���� �̵�
                break;
            case 1: case 3:
                Crouch(); // ��ũ���� (���)
                break;
            case 2: case 4:
                AttackCrouch(); // ��ũ���� (����)
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


    // (��ġ�̵� 1)
    void CenterMove() // �߾����� �̵�
    {
        bossCenterPosition = true;
        animationController.Walk(); // �̵� �ִϸ��̼�
        StartCoroutine(MoveToPosition(4));
    }

    // (��ġ�̵� 2)
    void PositionReset() // �ʱ� ��ġ�� �̵�
    {
        bossCenterPosition = false;
        animationController.Walk(); // �̵� �ִϸ��̼�
        StartCoroutine(MoveToPosition(7));
    }
    
    IEnumerator MoveToPosition(int position)
    {
        // ���� ��ġ�� �̵�
        while (Vector3.Distance(transform.position, firstBossStage.bossMapPositions[position].position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstBossStage.bossMapPositions[position].position, speed * Time.deltaTime);
            yield return null;
        }

        animationController.Halt();
        moving = false;
    }

    // (��ũ���� ���)
    void Crouch() // ��ũ���� (���)
    {
        animationController.Crouch(); // ��ũ���� �ִϸ��̼�

        StartCoroutine(UnCrouch()); // ��ũ���� ����
    }

    IEnumerator TrapAttack(float time)
    {
        while (onShield)
        {
            BulletAttack(trapPosition.transform.position); // ���� ����      
            yield return new WaitForSeconds(time); // ���
        }
    }

    IEnumerator UnCrouch() // �� ���� ����� ����
    {
        yield return new WaitForSeconds(1);

        shield.SetActive(true); // (��ȣ�� Ȱ��ȭ)
        onShield = true; // ��ȣ�� Ȱ��ȭ
        shieldCount = 5; // ���� ī��Ʈ ����

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
    
    // (��ũ���� ����)
    void AttackCrouch() // ��ũ���� (����)
    {
        watching = false;
        StartCoroutine(AttackCrouchRoutine());
    }

    IEnumerator AttackCrouchRoutine() // ��ũ�� �� ȸ���Ͽ� ���� �� ���ư���
    {
        rotateAttackSpeed = 1000f; // ȸ���ӵ� ����
        Quaternion initialRotation = transform.rotation; // �ʱ� ȸ�� �� ����

        animationController.Crouch(); // ��ũ���� �ִϸ��̼� ����
        yield return new WaitForSeconds(2);

        // �÷��̾� ��ġ ã�� (Y�� ����)
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        // �̵� �� ȸ��
        while (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            if (dying)
            {
                break;
            }

            // Y�� ���� ȸ��
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);

            // �÷��̾� ��ġ�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed * 2.5f) * Time.deltaTime);
            yield return null;
        }

        int bossPosition = bossCenterPosition ? 4 : 7; 

        // ���� ��ġ�� ���ư���
        Vector3 originalPosition = new Vector3(firstBossStage.bossMapPositions[bossPosition].position.x, transform.position.y, firstBossStage.bossMapPositions[bossPosition].position.z);
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            if (dying)
            {
                break;
            }

            // ���ư��鼭 ��� ȸ��
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (speed * 2.5f) * Time.deltaTime);
            yield return null;
        }

        // ���� �� õõ�� ȸ�� ���� �� ���� ȸ�������� ��Ȯ�� ���߱�
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

        transform.rotation = finalRotation; // ���������� ���� ȸ�� ������ ����

        // ��ũ���� ����
        rotateAttackSpeed = 0f; // ȸ���ӵ� ����
        animationController.Halt(); // ��ũ���� ����
        watching = true;
        moving = false;
    }

    // (������ ����)
    void Throw() // ������
    {
        animationController.Throw();

        ThrowAttack();
    }

    void ThrowAttack() // ������ ����
    {
        int bossPosition = bossCenterPosition ? 4 : 7; // ������ ���� ��ġ �ε���

        // SkillPositions �迭�� ��ȸ�ϸ� ť�� ����
        for (int i = 0; i < firstBossStage.SkillPositions.Length; i++)
        {
            if (i != bossPosition) // ������ ��ġ�� �ٸ� ��ġ���� ť�� ����
            {
                Vector3 spawnPosition = firstBossStage.SkillPositions[i].position; // ť�� ���� ��ġ
                GameObject cube = Instantiate(dropCube, spawnPosition, Quaternion.identity); // ť�� �ν��Ͻ�ȭ

                Destroy(cube, 12);
            }
        }
        moving = false;
    }

    // �� �Ϲ� �ڼ� ------------ �� �����ڼ�

    // (����)
    void AttackPose() // ���� �ڼ�
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

    // (�⺻ ����)
    IEnumerator BasicAttackReady() // ���� �غ�
    {
        bulletSpeed = 50;
        yield return new WaitForSeconds(2f);
        attackNum = 3;     

        StartCoroutine(RepeatBulletAttack(1f));
    }

    // (���� ����)
    IEnumerator QuickAttackReady() // ���� ���� �غ�
    {
        bulletSpeed = 30;
        yield return new WaitForSeconds(2f);
        attackNum = 10;

        StartCoroutine(RepeatBulletAttack(0.5f));
    }

    IEnumerator RepeatBulletAttack(float time) // �ݺ� �Ѿ� ����
    {
        while (attackNum >= 0)
        {
            if(dying)
            {
                break;
            }

            BulletAttack(bulletPosition.transform.position); // ���� ����      
            yield return new WaitForSeconds(time); // ���
        }
        animationController.Halt(); // ���� ��� ����
        moving = false;
    }

    

    void BulletAttack(Vector3 position) // �Ѿ� ����
    {
        animationController.Attack();
        attackNum--;
        GameObject bullet = Instantiate(attackBullet, position, Quaternion.identity);
        Destroy(bullet, 4f);

        Vector3 direction = (player.transform.position - bullet.transform.position).normalized;
        bullet.transform.rotation = Quaternion.LookRotation(direction);
        bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
    }

    // (���� ����)
    IEnumerator TauntAttackReady() // ���� �غ�
    {
        yield return new WaitForSeconds(2f);

        animationController.Taunt(); // ���� �ִϸ��̼� ����

        yield return new WaitForSeconds(1f);

        StartCoroutine(CreateFlooring()); // �ٴ� ���� ����
    }

    IEnumerator CreateFlooring() // �ٴ� ���� ����
    {
        HashSet<int> selectedPositions = new HashSet<int>(); // �ٴ� ��ġ ����Ʈ

        int bossPosition = bossCenterPosition ? 4 : 7;

        // 4���� �ߺ����� �ʴ� ��ġ�� ����, ������ġ ���� (���� [7])
        while (selectedPositions.Count < 4)
        {
            int randomIndex = Random.Range(0, firstBossStage.bossMapPositions.Length);
            if (randomIndex != bossPosition)
            {
                selectedPositions.Add(randomIndex);
            }
        }

        // ���õ� 4���� ��ġ�� �ٴ����� ����
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPositions[positionIndex].position.x, firstBossStage.bossMapPositions[positionIndex].position.y + 0.2f, firstBossStage.bossMapPositions[positionIndex].position.z);
            GameObject flooring = Instantiate(flooringEffect, effectPosition, Quaternion.identity);
            Destroy(flooring, 7);
        }

        StartCoroutine(MoveSkillWalls(0)); // ��ų�� �̵�
        yield return new WaitForSeconds(6f);

        // ���õ� 4���� ��ġ�� ���� ����
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPositions[positionIndex].position.x, firstBossStage.bossMapPositions[positionIndex].position.y + 0.2f, firstBossStage.bossMapPositions[positionIndex].position.z);
            GameObject explosion = Instantiate(explosionEffect, effectPosition, Quaternion.identity);
            Destroy(explosion, 1);
        }

        StartCoroutine(MoveSkillWalls(-20)); // ��ų�� ����

        animationController.Halt(); // ���� ��� ����
        moving = false;
    }

    // ��ų�� �̵�
    IEnumerator MoveSkillWalls(float targetY)
    {
        yield return new WaitForSeconds(1.5f); // ��� �ð�

        foreach (GameObject skillWall in firstBossStage.SkillWalls)
        {
            if (skillWall == null) continue;

            Vector3 targetPosition = new Vector3(skillWall.transform.position.x, targetY, skillWall.transform.position.z);

            // y ��ǥ���� ������ ������ �̵�
            while (Mathf.Abs(skillWall.transform.position.y - targetY) > 1f) // ���� ���
            {
                skillWall.transform.position = Vector3.MoveTowards(skillWall.transform.position, targetPosition, Time.deltaTime * 200f);
                yield return null;
            }
        }
    }

    void Die() // ���
    {
        dying = true; // ���ó��

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

    // (�ǰ�)
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

    void HealthUpdate() // ü�¹� ������Ʈ
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }
}

    
