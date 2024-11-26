 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldronm : MonoBehaviour
{
    public GameObject CauldronmFire; // 가마솥 불
    public GameObject fireWood; // 장작

    public bool active; // 활성화

    public TMP_Text failedUI;
    public bool showUI;

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

    public void Failed()
    {
        if (!showUI)
        {
            showUI = true;
            StartCoroutine(ShowFailedUI());
        }
    }

    IEnumerator ShowFailedUI()
    {
        failedUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        failedUI.gameObject.SetActive(false);
        showUI = false;
    }
}
