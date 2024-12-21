using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonsterController : MonoBehaviour
{
    public PlayerMovement player;

    public int damage; // 데미지

    public int maxHealth; // 최대 체력
    public int currentHealth; // 현재 체력

    public GameObject dropItem; // 드롭 아이템
    public int dropPercent; // 드롭 확률

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
        Die(); // 사망
    }

    
    void Die() // 사망
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

    void ItemDrop() // 아이템 드롭
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
        // 총알에 충돌시 피격
        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= player.damage;
        }
    }
}
