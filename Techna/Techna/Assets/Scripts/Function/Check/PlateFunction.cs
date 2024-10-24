using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFunction : MonoBehaviour
{
    public Material[] plateMaterials;
    private Renderer render;

    [Header("�̵� & ȸ�� ������Ʈ ")]
    public GameObject[] movingObj; // �̵��ϴ� ������Ʈ
    public GameObject[] rotateObj; // ȸ���ϴ� ������Ʈ

    [Header("Ư�� ������Ʈ üũ")]
    public GameObject checkObj; // Ȯ���� ������Ʈ
    public bool checkPlate; // Ư�� ������Ʈ���� Ȱ��ȭ �ǵ��� 

    public bool activate; // Ȱ��ȭ
    
    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // �̺�Ʈ ȣ��

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    void UpdateColor()
    {
        render.material = activate ? plateMaterials[1] : plateMaterials[0];
    }

    private void OnCollisionStay(Collision collision)
    {
        // checkPlate �϶��� �˸��� ������Ʈ���� Ȯ��
        if (checkPlate)
        {
            if (collision.gameObject == checkObj)
            {
                activate = true;
                activationChanged?.Invoke(true);
                UpdateColor();
            }
        }
        else if(!checkPlate) // �ƴҽ� �±� Ȯ��
        {
            if (collision.gameObject.CompareTag("GrabObject"))
            {
                activate = true;
                activationChanged?.Invoke(true);
                UpdateColor();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (checkPlate)
        {
            if (collision.gameObject == checkObj)
            {
                activate = false;
                activationChanged?.Invoke(false);
                UpdateColor();
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("GrabObject"))
            {
                activate = false;
                activationChanged?.Invoke(false);
                UpdateColor();
            }
        }
    }
}
