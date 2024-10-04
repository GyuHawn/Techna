using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class FirstBossAnimationController : MonoBehaviour
{
    public Animator character;

    public bool isAttacking = false;

    public void Walk() // 이동
    {
        Debug.Log("1");
        character.SetBool("isWalking", true);
    }

    public void Crouch() // 웅크리기
    {
        character.SetBool("isCrouch", true);
    }

    public void Halt() // 정지
    {
        character.SetBool("isWalking", false);
        character.SetBool("isCrouch", false);
        character.SetBool("isAttacking", false);
    }

    public void AttackPose() // 공격 모드
    {
        character.SetBool("isAttacking", true);
    }

    public void Attack() // 공격
    {
        character.SetTrigger("attack");
    }

    public void Taunt() // 조롱
    {
        character.SetTrigger("taunt");
    }

    public void Throw() // 던지기
    {
        character.SetTrigger("throw");
    }

    public void Die() // 사망
    {
        character.SetTrigger("die");
    }
}
