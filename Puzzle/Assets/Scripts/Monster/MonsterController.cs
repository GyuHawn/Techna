using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public PlayerMovement player;

    public int damage; // 데미지

    public int maxHealth; // 최대 체력
    public int currentHealth; // 현재 체력
   
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= player.damage;
        }
    }
}
