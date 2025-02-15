using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public Image healthBar;

    public bool attacked; // 공격 대기 여부
    public bool hit; // 피격 가능 여부

    public BoxCollider attackCollider;

    private string[] collisionBullet = new string[] { "Bullet", "ExpansionBullet", "Penetrate", "Destruction" };

    public Animator anim;
    public CapsuleCollider collider;
    public Renderer renderer;
    private Color originalColor;

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

        originalColor = renderer.material.color;
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
            AttackReady();
        }
    }

    // 공격
    void AttackReady()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f && !attacked) // 특정 거리 이내 애니메이션 시작
        {
            attacked = true;
            anim.SetTrigger("Attack"); // 공격 애니메이션 시작
            StartCoroutine(Attack());
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
            StartCoroutine(HitDamage(bullet.damage));
            UpdateHealthUI(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
                StartCoroutine(ChangeColor());
                collider.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(playerMovement.HitDamage(damage));
        }
    }

    public IEnumerator HitDamage(int damage) // 피격
    {
        if (!hit)
        {
            hit = true;
            currentHealth -= damage;
            yield return new WaitForSeconds(0.3f);

            hit = false;
        }
    }

    public IEnumerator ChangeColor() // 피격시 색변경
    {
        renderer.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        renderer.material.color = originalColor;
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth; // 체력 바 업데이트
    }
}
