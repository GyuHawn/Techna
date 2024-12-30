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
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
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
        anim.SetBool("Move", player != null); // �̵� �ִϸ��̼� ����
        if (player != null)
        {
            Attack();
        }
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

    // ���� �Ѿ� �߻� 
    private IEnumerator BasicShootBullet(Vector3 position)
    {
        yield return ShootBullet(position);
        playerTracking.tracking = false;
        shooting = false;
    }

    // ���� �Ѿ� �߻� 
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

    // �Ѿ� �߻�
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
            }
        }
    }
}
