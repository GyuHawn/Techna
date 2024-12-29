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
    IEnumerator StartPlayerTracking() // NavMeshAgent�� ���� ��ȯ��ġ ������ ����� Ȱ��ȭ
    {
        yield return new WaitForSeconds(0.5f);
        navMeshAgent.enabled = true;
    }

    void Update()
    {
        if (player != null)
        {
            anim.SetBool("Move", true); // �̵� �ִϸ��̼� ����
        }
        else
        {
            anim.SetBool("Move", false); // �̵� �ִϸ��̼� ����
        }

        AttackReady();
    }

    // ����
    void AttackReady()
    {
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f) // Ư�� �Ÿ� �̳� �ִϸ��̼� ����
        {
            if (!attacked)
            {
                Debug.Log("3");
                attacked = true;
                anim.SetTrigger("Attack"); // ���� �ִϸ��̼� ����
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

    // ���
    IEnumerator Die()
    {
        summonMonster.currentMonsterCount--;
        anim.SetTrigger("Die");

        EnabledScripts();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    // �÷��̾� ���� ��
    void EnabledScripts()
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
