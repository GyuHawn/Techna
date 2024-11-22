using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldronm : MonoBehaviour
{
    public GameObject CauldronmFire; // 가마솥 불
    public GameObject fireWood; // 장작

    public bool active; // 활성화

    private void OnTriggerEnter(Collider other)
    {
        // 장작 불 켜기
        if(other.gameObject == fireWood)
        {
            CauldronmFire.SetActive(true);
            active = true;
            Destroy(fireWood);
        }
    }
}
