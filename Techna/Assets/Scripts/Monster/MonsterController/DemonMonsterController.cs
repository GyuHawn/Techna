using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DemonMonsterController : MonoBehaviour
{
    public SummonMonster summonMonster;
    public PlayerMovement playerMovement;
    private PlayerTracking playerTracking;

    public GameObject player;
    public NavMeshAgent navMeshAgent;

    [Header("수치")]
    public int maxHealth;
    public int currentHealth;
    public int speed;
    public int damage;

    [Header("총알")]
    public GameObject bullet;
    public float bulletSpeed;
    public bool shooting;

    [Header("몬스터 종류")]
    public bool special;

    private string[] collisionBullet = new string[] { "Bullet", "ExpansionBullet", "Penetrate", "Destruction"};

    public Animator anim;

    void Awake()
    {
        summonMonster = GameObject.Find("SummonManager").GetComponent<SummonMonster>();
        playerTracking = GetComponent<PlayerTracking>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        StartCoroutine(StartPlayerTracking());

        bulletSpeed = 20f;
    }

    IEnumerator StartPlayerTracking() // NavMeshAgent로 인한 소환위치 문제로 잠시후 활성화
    {
        yield return new WaitForSeconds(0.5f);
        navMeshAgent.enabled = true;
    }

    void Update()
    {
        if (player != null)
        {
            anim.SetBool("Move", true); // 이동 애니메이션 시작
        }
        else
        {
            anim.SetBool("Move", false); // 이동 애니메이션 정지
        }

        Attack();
    }

    // 공격
    void Attack()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= 30f) // 특정 거리 이내 애니메이션 시작
        {
            AttackPlayerCheck();
        }
    }

    // 공격 전 플레이어 확인
    void AttackPlayerCheck()
    {
        // 레이의 시작 위치, 방향 설정
        Vector3 shotPosition = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z + 1);
        Vector3 rayOrigin = shotPosition;
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;

        // 레이 쏘기
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 50f)) // 30f는 레이의 길이
        {
            playerTracking.tracking = true;

            if (hit.collider.CompareTag("Player") && !shooting)
            {
                shooting = true; // 사격 상태
                
                anim.SetTrigger("Attack"); // 공격 애니메이션 시작

                if (!special)
                {
                    StartCoroutine(BasicShootBullet(shotPosition)); // 사격
                }
                else
                {
                    StartCoroutine(ContinuousShootBullet(shotPosition)); // 사격
                }
            }
        }
    }

    // 단일 총알 발사 
    IEnumerator BasicShootBullet(Vector3 position)
    {
        // 총알을 생성
        GameObject bulletInstance = Instantiate(bullet, position + transform.forward, Quaternion.identity);

        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = transform.forward * bulletSpeed;
        }

        Destroy(bulletInstance, 5);

        yield return new WaitForSeconds(2f);

        playerTracking.tracking = false;
        shooting = false;
    }

    // 연속 총알 발사 
    IEnumerator ContinuousShootBullet(Vector3 position)
    {
        for (int i = 0; i < 6; i++)
        {
            // 총알을 생성
            GameObject bulletInstance = Instantiate(bullet, position + transform.forward, Quaternion.identity);

            Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = transform.forward * bulletSpeed;
            }

            Destroy(bulletInstance, 5);

            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);

        playerTracking.tracking = false;
        shooting = false;
    }

    // 사망
    IEnumerator Die()
    {
        summonMonster.currentMonsterCount--;
        anim.SetTrigger("Die");

        EnabledScripts();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    // 플레이어 추적 끝
    void EnabledScripts()
    {
        navMeshAgent.enabled = false;    
        playerTracking.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 총알 확인후 피격
        if (System.Array.Exists(collisionBullet, tag => tag == collision.gameObject.tag))
        {
            ProjectileMoveScript bullet = collision.gameObject.GetComponent<ProjectileMoveScript>();
            currentHealth -= bullet.damage;

            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }
}
