using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3BossMotor : MonoBehaviour
{
    void Update()
    {
        RotateAroundZ();
    }
    void RotateAroundZ()
    {
        // Y������ ȸ��
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }
}
