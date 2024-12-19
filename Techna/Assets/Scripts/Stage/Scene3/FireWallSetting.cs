using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallSetting : MonoBehaviour
{
    public GameObject fireWall; // 불 벽

    public bool actived; // 활성화 여부

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 위치에 따라 비/활성화
        if (other.gameObject.CompareTag("Player")) 
        {
            fireWall.SetActive(actived);           
        }
    }
}
