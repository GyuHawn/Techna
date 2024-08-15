using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRod : MonoBehaviour
{
    public Material[] gemMaterials; // 재질
    public GameObject lightObj; // 라이트 
    public bool activate; // 활성화 여부

    public LEDNode lightLine; // 전기 선
    public ActivateGem gem; // 연결된 보석
    
    public GameObject changeMaterialObj; // 재직변경 할 오브젝트
    private Renderer render;

    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // 이벤트 호출

    void Start()
    {
        render = changeMaterialObj.GetComponent<Renderer>();
    }

    void Update()
    {
        activationChanged?.Invoke(activate); // 이벤트 호출 여부
    }

    void UpdateColor() // 재질 변경
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
        activate = !activate; // 활성화 여부 변경
        lightObj.SetActive(activate); // 라이트 활성화 변경
        lightLine.activate = activate; // 전기선 활성화 변경
        gem.activate = activate; // 보석 활성화 변경
        gem.OnElectricityGem(); // 보석 재질 변경
        UpdateColor(); // 재질 변경
    }
}
