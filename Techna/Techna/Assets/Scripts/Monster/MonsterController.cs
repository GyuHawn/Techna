using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public PlayerMovement player;

    public int damage; // ������

    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��

    public GameObject dropItem; // ��� ������
    public int dropPercent; // ��� Ȯ��

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        currentHealth = maxHealth;
    }

    
    void Update()
    {
        Die(); // ���
    }

    
    void Die() // ���
    {
        if(currentHealth <= 0)
        {
            if (dropItem != null)
            {
                ItemDrop();
            }
            Destroy(gameObject);
        }
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
        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= player.damage;
        }
    }
}
