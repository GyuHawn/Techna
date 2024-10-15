using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossAttackDamage : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            StartCoroutine(player.HitDamage(damage));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            StartCoroutine(player.HitDamage(damage));
        }
    }
}
