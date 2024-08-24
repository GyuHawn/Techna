using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfor : MonoBehaviour
{
    public ButtonController controller;
    public bool trueButton; // 진짜 버튼
    public bool currentStatus; // 현재 상태

    public Material[] materials; // 0: False, 1: True

    public Renderer renderer; // Material을 변경하기 위한 Renderer

    private void Awake()
    {
        controller = GetComponentInParent<ButtonController>();
        renderer = GetComponent<Renderer>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            controller.currentCheckCount--;
            currentStatus = true;
            renderer.material = materials[1];
        }
    }
}
