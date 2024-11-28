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

    public bool keyStatus; // 열쇠 재작 상태
    public TMP_Text keyStatusUI; // 열쇠 재작중 표시 UI
    public bool flaskStatus; // 플라스크 조합 상태
    public TMP_Text flaskStatusUI; // 플라스크 조합중 표시 UI

    public TMP_Text failedUI; // 불이 붙지않았으때 실패 UI
    public TMP_Text mixtureFailedUI; // 조합실패 UI

    public bool showUI; // UI 표시여부

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

    public void MixtureFailed()
    {
        if (!showUI)
        {
            showUI = true;
            StartCoroutine(ShowMixtureFailedUI());
        }
    }

    IEnumerator ShowMixtureFailedUI()
    {
        mixtureFailedUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        mixtureFailedUI.gameObject.SetActive(false);
        showUI = false;
    }
}
