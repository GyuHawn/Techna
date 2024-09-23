using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverFunction : MonoBehaviour
{
    private MonsterSummon monsterSummon; // 몬스터 소환 여부

    public bool activate; // 활성화 여부

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        monsterSummon = GetComponent<MonsterSummon>();
    }

    void Update()
    {
        // 활성화 시 레버 애니메이션 작동, 몬스터 소환
        if (activate)
        {
            anim.SetTrigger("Activate");
            monsterSummon.activate = true;
        }
    }
}
