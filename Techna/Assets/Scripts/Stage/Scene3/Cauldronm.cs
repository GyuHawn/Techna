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
    public Vector3 fireWoodPosition;

    public bool active; // 활성화

    public TMP_Text keyStatusUI; // 열쇠 재작중 표시 UI
    public TMP_Text flaskStatusUI; // 플라스크 조합중 표시 UI

    public TMP_Text failedUI; // 불이 붙지않았으때 실패 UI
    public TMP_Text mixtureFailedUI; // 조합실패 UI

    public bool showUI; // UI 표시여부

    private void Start()
    {
        fireWoodPosition = fireWood.transform.position + Vector3.up * 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
        {
            // 장작 불 켜기
            if (other.gameObject == fireWood)
            {
                CauldronmFire.SetActive(true);
                active = true;
                FireWoodMove();
            }
        }
    }

    public void Failed() // 불이 켜지지 않았을시 실패 UI 표시
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

    public void MixtureFailed() // 조합법이 틀렸을시 실패 UI 표시
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

    void FireWoodMove() // 불 붙일 장작사용시 재사용을 위해 다시 제자리로 이동
    {
        Rigidbody fireWoodObj = fireWood.GetComponent<Rigidbody>();
        fireWoodObj.velocity = Vector3.zero;
        fireWoodObj.angularVelocity = Vector3.zero;
        fireWood.transform.position = fireWoodPosition;
    }
}
