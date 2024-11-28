 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldronm : MonoBehaviour
{
    public GameObject CauldronmFire; // ������ ��
    public GameObject fireWood; // ����

    public bool active; // Ȱ��ȭ

    public bool keyStatus; // ���� ���� ����
    public TMP_Text keyStatusUI; // ���� ������ ǥ�� UI
    public bool flaskStatus; // �ö�ũ ���� ����
    public TMP_Text flaskStatusUI; // �ö�ũ ������ ǥ�� UI

    public TMP_Text failedUI; // ���� �����ʾ����� ���� UI
    public TMP_Text mixtureFailedUI; // ���ս��� UI

    public bool showUI; // UI ǥ�ÿ���

    private void OnTriggerEnter(Collider other)
    {
        // ���� �� �ѱ�
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
