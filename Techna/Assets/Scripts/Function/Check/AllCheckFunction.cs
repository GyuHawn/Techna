using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCheckFunction : MonoBehaviour
{
    [Header("üũ�� ������Ʈ")]
    public GameObject[] checkObjects; // Ȯ���� ����

    [Header("Ȱ��ȭ ����")]
    public bool activate; // Ȱ��ȭ ����
    public bool on; // ���� ���� 

    [Header("Ȯ���� Ÿ��")]
    public bool plate; // ����

    [Header("������ Ÿ��")]
    public bool obj; // ������Ʈ
    public bool stairs; // ���
    public bool controller; // ��ư

    [Header("������ ������Ʈ")]
    public GameObject targetObj;
    public bool target; // �ڽ�����

    void Start()
    {
        if (plate)
        {
            foreach (GameObject plate in checkObjects)
            {
                PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
                if (plateFunction != null)
                {
                    // �� ������ activationChanged �̺�Ʈ ����, Ȱ��ȭ ���� ����
                    plateFunction.activationChanged += CheckAllPlatesActivated;
                }
            }
        }
    }

    void OnDestroy()
    {
        if (plate)
        {
            foreach (GameObject plate in checkObjects)
            {
                PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
                if (plateFunction != null)
                {
                    // �̺�Ʈ ���� ����
                    plateFunction.activationChanged -= CheckAllPlatesActivated;
                }
            }
        }
    }

    void Update()
    {
        CheckAllPlatesActivated(false);
    }

    void CheckAllPlatesActivated(bool dummy) // ��� ���� Ȱ��ȭ �� �� ����
    {
        bool allPlatesActivated = true;

        if (plate)
        {
            foreach (GameObject plate in checkObjects) // ��� ���� Ȱ��ȭ Ȯ��
            {
                PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
                if (plateFunction != null)
                {
                    if (!plateFunction.activate)
                    {
                        // ���� ��Ȱ��ȭ �� ��ü ���� false
                        allPlatesActivated = false;
                        break;
                    }
                }
            }
        }

        if (allPlatesActivated) // ��� ���� Ȱ��ȭ �� �� ����
        {
            activate = true;
            CheckObject();
        }
    }

    void CheckObject()
    {
        if (obj) // �� - ������
        {
            if (!target)
            {
                MovingObject(gameObject);
            }
            else
            {
                MovingObject(targetObj);
            }
        }
        else if (stairs) // ��� - ��� Ȱ��ȭ
        {
            ActivatedStairs();
        }
        else if (controller)
        {
            ActivatedController();
        }
    }

    void ActivatedStairs() // ��� Ȱ��ȭ
    {
        if (!on)
        {
            on = true;
            ActivatedStairs stairsObj = gameObject.GetComponent<ActivatedStairs>();
            stairsObj.activated = true;
        }
    }
    
    void ActivatedController()
    {
        if (!on)
        {
            on = true;
            ActivatedController buttonsObj = gameObject.GetComponent<ActivatedController>();
            buttonsObj.activated = true;
        }
    }

    void MovingObject(GameObject obj) // ������Ʈ �̵�
    {
        activate = false;
        if (!on)
        {
            on = true;        
            MovingObject move = obj.GetComponent<MovingObject>();
            move.activated = true;
        }
    }
}
