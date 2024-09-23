using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGem : MonoBehaviour
{
    public Material[] gemMaterials; // 재질
    private Renderer render;

    public bool activate; // 자신 제어 여부

    public bool control;
    public bool electricity;
    public float electricityTime;

    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // 이벤트 호출

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

    void UpdateColor() // 재질 변경
    {
        render.material = activate ? gemMaterials[1] : gemMaterials[0];
    }

    public void OnElectricityGem() // 전기 속성 재질 변경
    {
        StartCoroutine(ElectricityUpdateColor(electricityTime));
    }

    IEnumerator ElectricityUpdateColor(float time)
    {
        yield return new WaitForSeconds(time);
        UpdateColor();
    }

    IEnumerator ChangeColor(float changeTime) // 재질 변경후 다시 복구
    {
        activate = true;
        activationChanged?.Invoke(true);
        UpdateColor();

        yield return new WaitForSeconds(changeTime);

        activate = false;
        activationChanged?.Invoke(false);
        UpdateColor();
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (control) // 제어 속성 재질 변경
        {
            // 충돌에 따른 시간 설정
            if (collision.gameObject.name == "BasicControl" || collision.gameObject.name == "BasicControlPenetrate")
            {
                StartCoroutine(ChangeColor(3f));
            }
            else if (collision.gameObject.name == "LargeControl" || collision.gameObject.name == "LargeControlPenetrate")
            {
                StartCoroutine(ChangeColor(5f));
            }
        }
    }

}
