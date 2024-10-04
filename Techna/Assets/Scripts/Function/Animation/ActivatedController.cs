using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedController : MonoBehaviour
{
    public bool activated; // 활성화 여부

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (activated) // 활성화시 작동 및 애니메이션 실행
        {
            activated = false;
            anim.SetTrigger("Activeted");
        }
    }
}
