using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateNum; // 회전 거리
    public bool x; // x축 회전 여부
    public bool y; // y축 회전 여부
    public bool z; // z축 회전 여부

    void Update()
    {
        if (x) // x방향으로 회전
        {          
            transform.Rotate(Vector3.right * rotateNum * Time.deltaTime);
        }
        if (y) // y방향으로 회전
        {
            transform.Rotate(Vector3.up * rotateNum * Time.deltaTime);
        }
        if (z) // z방향으로 회전
        {
            transform.Rotate(Vector3.forward * rotateNum * Time.deltaTime);
        }
    }
}
