using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallSetting : MonoBehaviour
{
    public GameObject fireWall; // �� ��

    public bool actived; // Ȱ��ȭ ����

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾� ��ġ�� ���� ��/Ȱ��ȭ
        if (other.gameObject.CompareTag("Player")) 
        {
            fireWall.SetActive(actived);           
        }
    }
}
