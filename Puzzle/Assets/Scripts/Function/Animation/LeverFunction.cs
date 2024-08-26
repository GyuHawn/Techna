using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverFunction : MonoBehaviour
{
    private MonsterSummon monsterSummon;

    public bool activate;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        monsterSummon = GetComponent<MonsterSummon>();
    }

    void Update()
    {
        if (activate)
        {
            anim.SetTrigger("Activate");
            monsterSummon.activate = true;
        }
    }
}
