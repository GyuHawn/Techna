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

    [Header("수치")]
    public int maxHealth;
    public int currentHealth;
    public int speed;
    public int damage;

    private string[] collisionBullet = new string[] { "Bullet", "ExpansionBullet", "Penetrate", "Destruction" };

    public Animator anim;

    void Awake()
    {
        summonMonster = GameObject.Find("SummonManager").GetComponent<SummonMonster>();
        playerTracking = GetComponent<PlayerTracking>();
    }

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        maxHealth = 9;
        currentHealth = maxHealth;
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
        if (distanceToPlayer < 10f) // 특정 거리 이내 애니메이션 시작
        {
            anim.SetTrigger("Attack"); // 공격 애니메이션 시작
        }
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
        NavMeshAgent nav = GetComponent<NavMeshAgent>();
        nav.enabled = false;
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
