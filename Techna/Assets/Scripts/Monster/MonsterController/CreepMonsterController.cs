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

    [Header("��ġ")]
    public int maxHealth;
    public int currentHealth;
    public int speed;
    public int damage;

    public bool attacked; // ���� ��� ����

    public BoxCollider attackCollider;

    private string[] collisionBullet = new string[] { "Bullet", "ExpansionBullet", "Penetrate", "Destruction" };

    public Animator anim;
    public CapsuleCollider collider;

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
    }

    IEnumerator StartPlayerTracking() // NavMeshAgent�� ���� ��ȯ��ġ ������ ����� Ȱ��ȭ
    {
        yield return new WaitForSeconds(0.5f);
        navMeshAgent.enabled = true;
    }

    void Update()
    {
        anim.SetBool("Move", player != null); // �̵� �ִϸ��̼� ����
        if (player != null)
        {
            AttackReady();
        }
    }

    // ����
    void AttackReady()
    {
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f && !attacked) // Ư�� �Ÿ� �̳� �ִϸ��̼� ����
        {
            attacked = true;
            anim.SetTrigger("Attack"); // ���� �ִϸ��̼� ����
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

    // ���
    IEnumerator Die()
    {
        summonMonster.currentMonsterCount--;
        anim.SetTrigger("Die");
        DisableScripts();

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    // �÷��̾� ���� ��
    void DisableScripts()
    {
        navMeshAgent.enabled = false;
        playerTracking.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �Ѿ� Ȯ���� �ǰ�
        if (System.Array.Exists(collisionBullet, tag => tag == collision.gameObject.tag))
        {
            ProjectileMoveScript bullet = collision.gameObject.GetComponent<ProjectileMoveScript>();
            currentHealth -= bullet.damage;

            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
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
}
