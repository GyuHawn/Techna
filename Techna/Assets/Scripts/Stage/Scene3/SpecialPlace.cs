using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlace : MonoBehaviour
{
    public GameObject placa; // ���� ������Ʈ

    public GameObject flask; // Ư�� �ö�ũ

    private void OnTriggerEnter(Collider other)
    {
        // Ư���� ��ġ�� �ö�ũ�� �浹�� ���� ����
        if(other.gameObject == flask)
        {
            placa.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
