using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGem : MonoBehaviour
{
    public Material[] gemMaterials; // ����
    private Renderer render;

    public bool activate; // �ڽ� ���� ����

    public bool control;
    public bool electricity;
    public float electricityTime;

    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // �̺�Ʈ ȣ��

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (activate)
        {
            activationChanged?.Invoke(true);
        }
    }

    void UpdateColor() // ���� ����
    {
        render.material = activate ? gemMaterials[1] : gemMaterials[0];
    }

    public void OnElectricityGem() // ���� �Ӽ� ���� ����
    {
        StartCoroutine(ElectricityUpdateColor(electricityTime));
    }

    IEnumerator ElectricityUpdateColor(float time) // �����ð� �� ���� ����
    {
        yield return new WaitForSeconds(time);
        UpdateColor();
    }

    IEnumerator ChangeColor(float changeTime) // ���� ������ �ٽ� ����
    {
        activate = true;
        activationChanged?.Invoke(true);
        UpdateColor();

        yield return new WaitForSeconds(changeTime);

        activate = false;
        activationChanged?.Invoke(false);
        UpdateColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (control) // ���� �Ӽ� ���� ����
        {
            // �浹�� ���� �ð� ����
            if (other.gameObject.name == "BasicControl" || other.gameObject.name == "BasicControlPenetrate")
            {
                StartCoroutine(ChangeColor(3f));
            }
            else if (other.gameObject.name == "LargeControl" || other.gameObject.name == "LargeControlPenetrate")
            {
                StartCoroutine(ChangeColor(5f));
            }
        }
    }
}
