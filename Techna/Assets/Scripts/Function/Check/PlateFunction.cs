using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFunction : MonoBehaviour
{
    public Material[] plateMaterials;
    private Renderer render;

    public GameObject[] movingObj; // 이동하는 오브젝트
    public GameObject[] rotateObj; // 회전하는 오브젝트

   // public GameObject functionObj; // 제어할 오브젝트
    public GameObject checkObj; // 확인할 오브젝트

    public bool activate; // 활성화
    public bool checkPlate; // 특정 오브젝트에만 활성화 되도록 
    
    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // 이벤트 호출

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
        // checkPlate 일때는 알맞은 오브젝트인지 확인
        if (checkPlate)
        {
            if (collision.gameObject == checkObj)
            {
                activate = true;
                activationChanged?.Invoke(true);
                UpdateColor();
            }
        }
        else if(!checkPlate) // 아닐시 태그 확인
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
