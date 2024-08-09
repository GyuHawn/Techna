using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObejctPostion : MonoBehaviour
{
    public Transform resetPos; // 리셋 위치

    private void OnCollisionEnter(Collision collision)
    {
        // 잡은 오브젝트, 플레이어가 충돌시 리셋 위치로 이동
        if (collision.gameObject.CompareTag("GrabObject") || collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = resetPos.position;
        }
    }
}
