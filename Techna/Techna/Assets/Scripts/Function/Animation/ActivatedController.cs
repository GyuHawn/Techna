using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedController : MonoBehaviour
{
    public bool activated; // Ȱ��ȭ ����

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (activated) // Ȱ��ȭ�� �۵� �� �ִϸ��̼� ����
        {
            activated = false;
            anim.SetTrigger("Activeted");
        }
    }
}
