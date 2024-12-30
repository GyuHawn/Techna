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
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
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
        anim.SetBool("Move", player != null); // 이동 애니메이션 설정
        if (player != null)
        {
            Attack();
        }
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
    private void AttackPlayerCheck()
    {
        Vector3 shotPosition = transform.position + new Vector3(0, 3f, 1f);
        Ray ray = new Ray(shotPosition, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f))
        {
            playerTracking.tracking = true;

            if (hit.collider.CompareTag("Player") && !shooting)
            {
                shooting = true;
                anim.SetTrigger("Attack");

                if (special)
                {
                    StartCoroutine(ContinuousShootBullet(shotPosition));
                }
                else
                {
                    StartCoroutine(BasicShootBullet(shotPosition));
                }
            }
        }
    }

    // 단일 총알 발사 
    private IEnumerator BasicShootBullet(Vector3 position)
    {
        yield return ShootBullet(position);
        playerTracking.tracking = false;
        shooting = false;
    }

    // 연속 총알 발사 
    private IEnumerator ContinuousShootBullet(Vector3 position)
    {
        for (int i = 0; i < 6; i++)
        {
            yield return ShootBullet(position);
        }
        yield return new WaitForSeconds(1f);
        playerTracking.tracking = false;
        shooting = false;
    }

    // 총알 발사
    private IEnumerator ShootBullet(Vector3 position)
    {
        GameObject bulletInstance = Instantiate(bullet, position + transform.forward, Quaternion.identity);
        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = transform.forward * bulletSpeed;
        }

        Destroy(bulletInstance, 5);
        yield return new WaitForSeconds(special ? 0.5f : 2f);
    }

    // 사망
    IEnumerator Die()
    {
        summonMonster.currentMonsterCount--;
        anim.SetTrigger("Die");

        DisableScripts();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    // 플레이어 추적 끝
    void DisableScripts()
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
