using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfor : MonoBehaviour
{
    public ButtonsController controller;

    [Header("��¥ or ��¥")]
    public bool trueButton; // ��¥ ��ư
    public bool currentStatus; // ���� ����

    [Header("����")]
    public Material[] materials; // 0: False, 1: True
    private new Renderer renderer; // Material�� �����ϱ� ���� Renderer

    [Header("Ȯ���� �Ѿ�")]
    public string[] collisionBullet = new string[] {"Bullet", "Expansion", "Penetrate" }; // �浹 �±� 

    private void Awake()
    {
        controller = GetComponentInParent<ButtonsController>();
        renderer = GetComponent<Renderer>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (System.Array.Exists(collisionBullet, tag => tag == collision.gameObject.tag)) // �Ѿ� �浹�� ���� ����
        {
            if (controller != null) 
            {
                controller.currentCheckCount--;
            }

            currentStatus = true;
            renderer.material = materials[1];
        }
    }
}
