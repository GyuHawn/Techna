using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMonsterController : MonoBehaviour
{
    public SummonMonster summonMonster;
    public PlayerMovement player;

    public int maxHealth;
    public int currentHealth;

    public int speed;
    public int damage;

    private string[] collisionBullet = new string[] { "Bullet", "ExpansionBullet", "Penetrate", "Destruction"};

    void Awake()
    {
        summonMonster = GameObject.Find("SummonManager").GetComponent<SummonMonster>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        maxHealth = 1;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        summonMonster.totalMonsterCount--;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (System.Array.Exists(collisionBullet, tag => tag == collision.gameObject.tag))
        {
            currentHealth -= player.damage;
        }
    }
}
