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

    [Header("��ġ")]
    public int maxHealth;
    public int currentHealth;
    public int speed;
    public int damage;

    [Header("�Ѿ�")]
    public GameObject bullet;
    public float bulletSpeed;
    public bool shooting;

    [Header("���� ����")]
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

        Attack();
    }

    // ����
    void Attack()
    {
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= 30f) // Ư�� �Ÿ� �̳� �ִϸ��̼� ����
        {
            AttackPlayerCheck();
        }
    }

    // ���� �� �÷��̾� Ȯ��
    void AttackPlayerCheck()
    {
        // ������ ���� ��ġ, ���� ����
        Vector3 shotPosition = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z + 1);
        Vector3 rayOrigin = shotPosition;
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;

        // ���� ���
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 50f)) // 30f�� ������ ����
        {
            playerTracking.tracking = true;

            if (hit.collider.CompareTag("Player") && !shooting)
            {
                shooting = true; // ��� ����
                
                anim.SetTrigger("Attack"); // ���� �ִϸ��̼� ����

                if (!special)
                {
                    StartCoroutine(BasicShootBullet(shotPosition)); // ���
                }
                else
                {
                    StartCoroutine(ContinuousShootBullet(shotPosition)); // ���
                }
            }
        }
    }

    // ���� �Ѿ� �߻� 
    IEnumerator BasicShootBullet(Vector3 position)
    {
        // �Ѿ��� ����
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

    // ���� �Ѿ� �߻� 
    IEnumerator ContinuousShootBullet(Vector3 position)
    {
        for (int i = 0; i < 6; i++)
        {
            // �Ѿ��� ����
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
}
