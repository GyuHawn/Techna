using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3BossMovenent : MonoBehaviour
{
    public GameObject player;

    public float speed = 5f; // ���� �ӵ�
    public Vector3 areaSize = new Vector3(60f, 50f, 60f); // ������ ���� ũ��
    private Vector3 targetPosition;

    public GameObject bullet; // �Ѿ�
    public float bulletSpeed; // �Ѿ� ���ǵ�

    public GameObject dieEffect; // ��� ����Ʈ

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        SetRandomTargetPosition(); // ���� ��ǥ ��ġ ����

        bulletSpeed = 40;
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        
        Move();
    }

    void Move()
    {
        // ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ��ǥ ��ġ�� ���޽� ���ο� ��ǥ ��ġ ����
        if (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            SetRandomTargetPosition();
        }

        // �̵� ������ �ٶ󺸵��� ȸ��
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero) // ������ 0�� �ƴ� ���� ȸ��
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, 90f, 0); // Y�࿡ 90�� �߰�
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }

    void SetRandomTargetPosition() // ��ǥ ��ġ ����
    {
        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomZ = Random.Range(-areaSize.z / 2, areaSize.z / 2);
        targetPosition = new Vector3(randomX, 50f, randomZ); // Y���� ���̸� ���
    }

    IEnumerator AttackReady() // ���� �غ�
    {
        yield return new WaitForSeconds(5f);
        Attack();
    }
    void Attack() // ����
    {
        Vector3 attackPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 5, gameObject.transform.position.z);
        GameObject attack = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        Destroy(attack, 5);

        Vector3 direction = (player.transform.position - bullet.transform.position).normalized;
        bullet.transform.rotation = Quaternion.LookRotation(direction);
        bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
    }

    void Die() // ���
    {
        // �߶��� ���� ������ ����
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;

        // �ִϸ��̼� ����
        anim.enabled = false;

        // ���� �� ��ũ��Ʈ ��Ȱ��ȭ
        Destroy(gameObject, 10f);
        this.enabled = false;
    }
}