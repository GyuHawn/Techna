using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float moveNum; // 이동 거리
    public bool x; // x축 이동 여부
    public bool y; // y축 이동 여부
    public bool z; // z축 이동 여부

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (x) // x방향으로 이동
        {
            move.x = moveNum * Time.deltaTime;
        }
        if (y) // y방향으로 이동 
        {
            move.y = moveNum * Time.deltaTime;
        }
        if (z) // z방향으로 이동
        {
            move.z = moveNum * Time.deltaTime;
        }

        transform.Translate(move);
    }
}
