using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonsterController : MonoBehaviour
{
    public PlayerMovement player;

    public int damage; // ������

    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��

    public GameObject dropItem; // ��� ������
    public int dropPercent; // ��� Ȯ��

    private string[] collisionBullet = new string[] { "Bullet", "ExpansionBullet", "Penetrate", "Destruction" };

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        maxHealth = 5;
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die(); // ���
        }
    }

    void Die() // ���
    {
        if (dropItem != null)
        {
            ItemDrop();
        }
        Destroy(gameObject);
    }

    void ItemDrop() // ������ ���
    {
        int num = Random.Range(0, 100);
        if(num <= dropPercent)
        {
            GameObject key = Instantiate(dropItem, gameObject.transform.position, Quaternion.identity);
            key.name = dropItem.name;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �Ѿ˿� �浹�� �ǰ�
        if (System.Array.Exists(collisionBullet, tag => tag == other.gameObject.tag))
        {
            ProjectileMoveScript bullet = other.gameObject.GetComponent<ProjectileMoveScript>();
            currentHealth -= bullet.damage;
        }
    }
}
