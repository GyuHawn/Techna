using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGem : MonoBehaviour
{
    public Material[] gemMaterials; // 재질
    public bool onControl; // 자신 제어 여부

    private Renderer render;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void UpdateColor() // 재질 변경
    {
        render.material = onControl ? gemMaterials[1] : gemMaterials[0];
    }

    IEnumerator ChangeColor(float changeTime) // 재질 변경후 다시 복구
    {
        onControl = true;
        Debug.Log("onControl(참): " + onControl);
        UpdateColor();

        yield return new WaitForSeconds(changeTime);

        onControl = false;
        Debug.Log("onControl(거짓): " + onControl);
        UpdateColor();
    }

    private void OnCollisionEnter(Collision collision) 
    {
        // 충돌에 따른 시간 설정
        if (collision.gameObject.name == "BasicControl")
        {
        Debug.Log("BasicControl");
            StartCoroutine(ChangeColor(3f));
        }
        else if (collision.gameObject.name == "LargeControl")
        {
        Debug.Log("LargeControl");
            StartCoroutine(ChangeColor(5f));
        }
    }
}
