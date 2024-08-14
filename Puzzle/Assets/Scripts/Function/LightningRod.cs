using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRod : MonoBehaviour
{
    public Material[] gemMaterials; // 재질
    public GameObject lightObj;
    public bool activate;

    public GameObject changeMaterialObj;
    private Renderer render;

    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // 이벤트 호출

    void Start()
    {
        render = changeMaterialObj.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!activate)
        {
            activationChanged?.Invoke(false);
        }
        else
        {
            activationChanged?.Invoke(true);
        }
    }

    void UpdateColor() // 재질 변경
    {
        render.material = activate ? gemMaterials[1] : gemMaterials[0];
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌에 따른 시간 설정
        if (collision.gameObject.name == "BasicElectricity")
        {
            lightObj.SetActive(!activate);
            activate = !activate;
            UpdateColor();          
        }
        else if (collision.gameObject.name == "LargeElectricity")
        {
            lightObj.SetActive(!activate);
            activate = !activate;
            UpdateColor();
        }
    }
}
