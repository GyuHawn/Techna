using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class FirstBossAnimationController : MonoBehaviour
{
    public Animator character;

    public bool isAttacking = false;

    public void Walk() // �̵�
    {
        Debug.Log("1");
        character.SetBool("isWalking", true);
    }

    public void Crouch() // ��ũ����
    {
        character.SetBool("isCrouch", true);
    }

    public void Halt() // ����
    {
        character.SetBool("isWalking", false);
        character.SetBool("isCrouch", false);
        character.SetBool("isAttacking", false);
    }

    public void AttackPose() // ���� ���
    {
        character.SetBool("isAttacking", true);
    }

    public void Attack() // ����
    {
        character.SetTrigger("attack");
    }

    public void Taunt() // ����
    {
        character.SetTrigger("taunt");
    }

    public void Throw() // ������
    {
        character.SetTrigger("throw");
    }

    public void Die() // ���
    {
        character.SetTrigger("die");
    }
}
