using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldronm : MonoBehaviour
{
    public GameObject CauldronmFire; // ������ ��
    public GameObject fireWood; // ����

    private void OnTriggerEnter(Collider other)
    {
        // ���� �� �ѱ�
        if(other.gameObject == fireWood)
        {
            CauldronmFire.SetActive(true);
            Destroy(fireWood);
        }
    }
}
