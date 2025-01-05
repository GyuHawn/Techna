using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Stage3BossMovenent : MonoBehaviour
{
    public Stage4Manager stage4Manager;
    public GameObject player;

    public float speed = 5f; // ���� �ӵ�
    public Vector3 areaSize = new Vector3(60f, 50f, 60f); // ������ ���� ũ��
    private Vector3 targetPosition;

    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public GameObject bullet; // �Ѿ�
    public float bulletSpeed; // �Ѿ� ���ǵ�

    public Image healthBar; // ü�¹�
    public GameObject dieEffect; // ��� ����Ʈ
    public bool die; // �������

    void Start()
    {
        SetRandomTargetPosition(); // ���� ��ǥ ��ġ ����

        bulletSpeed = 50;
        maxHealth = 300;
        currentHealth = maxHealth;

        InvokeRepeating("Attack", 5, 3);
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        HealthUpdate();
    }

    void SetRandomTargetPosition() // ��ǥ ��ġ ���� �� ȸ��
    {
        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomZ = Random.Range(-areaSize.z / 2, areaSize.z / 2);
        targetPosition = new Vector3(randomX, 50f, randomZ); // Y���� ���̸� ���

        // �̵� ���� ���
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero) // ������ 0�� �ƴ� ���� ȸ��
        {
            // Y�� ȸ���� ����
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion lookRotation = Quaternion.Euler(270, 180, angle + 270); // Z�� ȸ��

            // ȸ�� ���� (��� ����)
            transform.rotation = lookRotation; // Quaternion.Slerp ��� ��� ����
        }

        // ��ǥ ��ġ�� �̵�
        StartCoroutine(MoveToTarget());
    }


    private IEnumerator MoveToTarget() // �̵�
    {
        while (Vector3.Distance(transform.position, targetPosition) >= 1f) // ��ǥ ��ġ���� �̵�
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // ��ǥ ��ġ�� ������ �� ���ο� ��ǥ ��ġ ����
        SetRandomTargetPosition();
    }

    void Attack() // ����
    {
        Vector3 attackPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 5, gameObject.transform.position.z);
        GameObject attack = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        Destroy(attack, 5);

        Vector3 direction = (player.transform.position - attack.transform.position).normalized;
        attack.transform.rotation = Quaternion.LookRotation(direction);
        attack.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
    }

    void HealthUpdate() // ü�¹� ������Ʈ
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }

    void Die() // ���
    {
        if (currentHealth <= 0 && !die)
        {
            die = true;

            StartCoroutine(DieEffect()); // ��� ����Ʈ
            BossCrash(); // ���� �߶�

            stage4Manager.portal.SetActive(true);

            // ���� �� ��ũ��Ʈ ��Ȱ��ȭ
            Destroy(gameObject, 10f);
            this.enabled = false;  
        }
    }

    // ���� �߶�
    void BossCrash()
    {
        // �߶��� ���� ������ ����
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
    }

    // ��� ����Ʈ
    IEnumerator DieEffect()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * 5f;

            // ����Ʈ ����
            GameObject effect = Instantiate(dieEffect, randomPosition, Quaternion.identity);
            Destroy(effect, 0.5f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string[] collisionBullet = new string[] { "Bullet", "Expansion", "Penetrate" };

        // �ǰ�
        if (System.Array.Exists(collisionBullet, tag => tag == other.gameObject.tag))
        {
            ProjectileMoveScript bullet = other.gameObject.GetComponent<ProjectileMoveScript>();
            currentHealth -= bullet.damage;
            
            Die(); // ��� Ȯ��
        }
    }
}