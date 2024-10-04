using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfor : MonoBehaviour
{
    public ButtonsController controller;
    public bool trueButton; // 진짜 버튼
    public bool currentStatus; // 현재 상태

    public Material[] materials; // 0: False, 1: True

    public new Renderer renderer; // Material을 변경하기 위한 Renderer

    public string[] collisionBullet = new string[] {"Bullet", "Expansion", "Penetrate" };

    private void Awake()
    {
        controller = GetComponentInParent<ButtonsController>();
        renderer = GetComponent<Renderer>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (System.Array.Exists(collisionBullet, tag => tag == collision.gameObject.tag)) // 총알 충돌시 재질 변경
        {
            if (controller != null) 
            {
                controller.currentCheckCount--;
            }

            currentStatus = true;
            renderer.material = materials[1];
        }
    }
}
