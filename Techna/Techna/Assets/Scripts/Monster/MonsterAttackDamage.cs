using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackDamage : MonoBehaviour
{
    public int damage; // 데미지

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // 플레이어 충돌시 공격
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.TakeDamage(damage);
        }
    }
}
