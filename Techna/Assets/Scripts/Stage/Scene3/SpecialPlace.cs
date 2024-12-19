using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlace : MonoBehaviour
{
    public GameObject placa; // 공간 오브젝트

    public GameObject flask; // 특수 플라스크

    private void OnTriggerEnter(Collider other)
    {
        // 특수한 위치에 플라스크가 충돌시 공간 생성
        if(other.gameObject == flask)
        {
            placa.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
