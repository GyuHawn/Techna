using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteDevice : MonoBehaviour
{
    public LEDNode activateNode; // ����� ��
    
    public Material[] materials; // ����
    public GameObject changeMaterialObj;  // �������� �� ������Ʈ

    public GameObject[] objs; // ������ ������Ʈ 

    public bool destroy; // ��������
    public bool activate; // Ȱ��ȭ ��ų��
    public bool stairs; // ��� Ȱ��ȭ


    private Renderer render;

    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged;

    void Start()
    {
        render = changeMaterialObj.GetComponent<Renderer>();

        if (activateNode != null) // �̺�Ʈ ����, Ȱ��ȭ ���� ����
        {
            activateNode.activationChanged += ActivatedCheck;
        }
    }

    void Update()
    {
        ActivatedCheck(false); // Ȱ��ȭ üũ
    }

    void ActivatedCheck(bool dummy)
    {
        bool activated = true;

        if (!activateNode.activate) // ��Ȱ��ȭ�� ���� false, ���� ����
        {
            activated = false;
            UpdateColor();
        }

        if (activated) // Ȱ��ȭ�� ��������
        {
            UpdateColor();

            if (destroy) // ������ ������Ʈ ����
            {
                Destroy();
            }
            else if (activate)
            {
                if (stairs)
                {
                    foreach(GameObject stairs in objs)
                    {
                        ActivatedStairs staireFunction = stairs.GetComponent<ActivatedStairs>();
                        staireFunction.activated = true;
                    }
                }
            }
        }
    }

    void UpdateColor() // ���� ����
    {
        render.material = activateNode.activate ? materials[1] : materials[0];
    }

    void Destroy() // ����
    {
        if(activateNode.activate && objs != null)
        {
            foreach(GameObject obj in objs)
            {
                Destroy(obj);
            }   
        }
    }

}