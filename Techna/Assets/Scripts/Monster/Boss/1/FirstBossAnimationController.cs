using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class FirstBossAnimationController : MonoBehaviour
{
    public Animator character;

    public bool isWalking = false;
    public bool isCrouch = false;
    public bool isAttacking = false;

    public void Walk() // 이동
    {
        isWalking = !isWalking;
        character.SetBool("isWalking", isWalking);
    }

    public void Crouch() // 웅크리기
    {
        isCrouch = !isCrouch;
        character.SetBool("isCrouch", isCrouch);
    }

    public void Halt() // 정지
    {
        character.SetBool("isWalking", false);
        character.SetBool("isAttacking", false);
    }

    public void AttackPose() // 공격 모드
    {
        isAttacking = !isAttacking;
        character.SetBool("isAttacking", isAttacking);
    }

    public void Attack() // 공격
    {
        character.SetTrigger("attack");
    }

    public void TakeDamage() // 피격
    {
        character.SetTrigger("takeDamage");
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
