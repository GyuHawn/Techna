using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldronm : MonoBehaviour
{
    public GameObject CauldronmFire; // ������ ��
    public GameObject fireWood; // ����

    public bool active; // Ȱ��ȭ

    private void OnTriggerEnter(Collider other)
    {
        // ���� �� �ѱ�
        if(other.gameObject == fireWood)
        {
            CauldronmFire.SetActive(true);
            active = true;
            Destroy(fireWood);
        }
    }
}
