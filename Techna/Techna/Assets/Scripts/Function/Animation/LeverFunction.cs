using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverFunction : MonoBehaviour
{
    private MonsterSummon monsterSummon; // ���� ��ȯ ����

    public bool activate; // Ȱ��ȭ ����

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        monsterSummon = GetComponent<MonsterSummon>();
    }

    void Update()
    {
        // Ȱ��ȭ �� ���� �ִϸ��̼� �۵�, ���� ��ȯ
        if (activate)
        {
            anim.SetTrigger("Activate");
            monsterSummon.activate = true;
        }
    }
}
