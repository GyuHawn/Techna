using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3BossMovenent : MonoBehaviour
{
    public GameObject player;

    public float speed = 5f; // 보스 속도
    public Vector3 areaSize = new Vector3(60f, 50f, 60f); // 움직일 범위 크기
    private Vector3 targetPosition;

    public int maxHealth; // 최대 체력
    public int currentHealth; // 현재 체력
    public GameObject bullet; // 총알
    public float bulletSpeed; // 총알 스피드

    public Image healthBar; // 체력바
    public GameObject dieEffect; // 사망 이펙트
    public bool die; // 사망여부

    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        SetRandomTargetPosition(); // 시작 목표 위치 설정

        bulletSpeed = 50;
        maxHealth = 300;
        currentHealth = maxHealth;

        InvokeRepeating("Attack", 5, 4);
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        
        Move();
        HealthUpdate();
    }

    void Move()
    {
        // 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목표 위치에 도달시 새로운 목표 위치 설정
        if (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            SetRandomTargetPosition();
        }

        // 이동 방향을 바라보도록 회전
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero) // 방향이 0이 아닐 때만 회전
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, 90f, 0); // Y축에 90도 추가
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }

    void SetRandomTargetPosition() // 목표 위치 설정
    {
        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomZ = Random.Range(-areaSize.z / 2, areaSize.z / 2);
        targetPosition = new Vector3(randomX, 50f, randomZ); // Y축은 높이를 고려
    }

    void Attack() // 공격
    {
        Vector3 attackPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 5, gameObject.transform.position.z);
        GameObject attack = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        Destroy(attack, 5);

        Vector3 direction = (player.transform.position - attack.transform.position).normalized;
        attack.transform.rotation = Quaternion.LookRotation(direction);
        attack.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
    }

    void HealthUpdate() // 체력바 업데이트
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }

    void Die() // 사망
    {
        if (currentHealth <= 0 && !die)
        {
            die = true;

            StartCoroutine(DieEffect()); // 사망 이펙트
            BossCrash(); // 보스 추락

            // 제거 및 스크립트 비활성화
            Destroy(gameObject, 10f);
            this.enabled = false;
        }
    }

    // 보스 추락
    void BossCrash()
    {
        anim.SetBool("Die", true);

        // 추락을 위한 프리즈 해제
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;

        // 애니메이션 종료
        anim.enabled = false;
    }

    // 사망 이펙트
    IEnumerator DieEffect()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * 5f;

            // 이펙트 생성
            GameObject effect = Instantiate(dieEffect, randomPosition, Quaternion.identity);
            Destroy(effect, 0.5f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string[] collisionBullet = new string[] { "Bullet", "Expansion", "Penetrate" };

        // 피격
        if (System.Array.Exists(collisionBullet, tag => tag == other.gameObject.tag))
        {
            ProjectileMoveScript bullet = other.gameObject.GetComponent<ProjectileMoveScript>();
            currentHealth -= bullet.damage;
            
            Die(); // 사망 확인
        }
    }
}