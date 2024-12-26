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

    [Header("��ġ")]
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
            anim.SetBool("Move", true); // �̵� �ִϸ��̼� ����
        }
        else
        {
            anim.SetBool("Move", false); // �̵� �ִϸ��̼� ����
        }

        Attack();
    }

    // ����
    void Attack()
    {
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f) // Ư�� �Ÿ� �̳� �ִϸ��̼� ����
        {
            anim.SetTrigger("Attack"); // ���� �ִϸ��̼� ����
        }
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
        NavMeshAgent nav = GetComponent<NavMeshAgent>();
        nav.enabled = false;
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
}
