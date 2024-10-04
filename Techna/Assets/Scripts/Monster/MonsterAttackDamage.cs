using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackDamage : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.currentHealth -= damage;
        }
    }
}
