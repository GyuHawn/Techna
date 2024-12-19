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
    public Vector3 fireWoodPosition;

    public bool active; // Ȱ��ȭ

    public TMP_Text keyStatusUI; // ���� ������ ǥ�� UI
    public TMP_Text flaskStatusUI; // �ö�ũ ������ ǥ�� UI

    public TMP_Text failedUI; // ���� �����ʾ����� ���� UI
    public TMP_Text mixtureFailedUI; // ���ս��� UI

    public bool showUI; // UI ǥ�ÿ���

    private void Start()
    {
        fireWoodPosition = fireWood.transform.position + Vector3.up * 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
        {
            // ���� �� �ѱ�
            if (other.gameObject == fireWood)
            {
                CauldronmFire.SetActive(true);
                active = true;
                FireWoodMove();
            }
        }
    }

    public void Failed() // ���� ������ �ʾ����� ���� UI ǥ��
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

    public void MixtureFailed() // ���չ��� Ʋ������ ���� UI ǥ��
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

    void FireWoodMove() // �� ���� ���ۻ��� ������ ���� �ٽ� ���ڸ��� �̵�
    {
        Rigidbody fireWoodObj = fireWood.GetComponent<Rigidbody>();
        fireWoodObj.velocity = Vector3.zero;
        fireWoodObj.angularVelocity = Vector3.zero;
        fireWood.transform.position = fireWoodPosition;
    }
}
