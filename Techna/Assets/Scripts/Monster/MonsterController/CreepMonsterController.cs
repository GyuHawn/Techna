using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepMonsterController : MonoBehaviour
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

    public bool attacked; // 공격 대기 여부

    public BoxCollider attackCollider;

    private string[] collisionBullet = new string[] { "Bullet", "ExpansionBullet", "Penetrate", "Destruction" };

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

        AttackReady();
    }

    // 공격
    void AttackReady()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f) // 특정 거리 이내 애니메이션 시작
        {
            if (!attacked)
            {
                Debug.Log("3");
                attacked = true;
                anim.SetTrigger("Attack"); // 공격 애니메이션 시작
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        attacked = false;
        Debug.Log("4");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("5");
            StartCoroutine(playerMovement.HitDamage(damage));
        }
    }
}
