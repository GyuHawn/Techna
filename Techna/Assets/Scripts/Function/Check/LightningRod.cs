 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRod : MonoBehaviour
{
    public Material[] gemMaterials; // ����

    [Header("����Ʈ")]
    public GameObject lightObj; // ����Ʈ 
    public bool activate; // Ȱ��ȭ ����

    [Header("���� ����")]
    public LEDNode lightLine; // ���� ��

    [Header("����� ����")]
    public ActivateGem gem; // ����� ����

    [Header("Ȱ��ȭ�� ������Ʈ ���� ����")]
    public GameObject changeMaterialObj; // �������� �� ������Ʈ
    private Renderer render;

    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // �̺�Ʈ ȣ��

    void Start()
    {
        render = changeMaterialObj.GetComponent<Renderer>();
    }

    void Update()
    {
        activationChanged?.Invoke(activate); // �̺�Ʈ ȣ�� ����
    }

    void UpdateColor() // ���� ����
    {
        render.material = activate ? gemMaterials[1] : gemMaterials[0];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BasicElectricity" || collision.gameObject.name == "LargeElectricity")
        {
            ToggleActivation(); 
        }
    }

    private void ToggleActivation()
    {
        activate = !activate; // Ȱ��ȭ ���� ����
        lightObj.SetActive(activate); // ����Ʈ Ȱ��ȭ ����
        lightLine.activate = activate; // ���⼱ Ȱ��ȭ ����

        if(gem != null) 
        {
            gem.activate = activate; // ���� Ȱ��ȭ ����
            gem.OnElectricityGem(); // ���� ���� ����
        }

        UpdateColor(); // ���� ����
    }
}
