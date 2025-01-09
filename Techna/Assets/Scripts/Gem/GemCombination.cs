using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCombination : MonoBehaviour
{
    private GemUI gemUI;
    private GemManager gemManager;
    public ProjectilesScript projectilesScript;
    public GunTexture gunTexture;

    [Header("������ ��ȣ")]
    public int selectBulletNum; // ���õ� �Ѿ� ��ȣ
    public int selectAttributeNum; // ���õ� �Ӽ� ��ȣ
    public int selectFunctionNum; // ���õ� ��� ��ȣ

    [Header("�Ѿ� ������Ʈ")]
    public GameObject[] B_Gems; // �Ѿ� ������Ʈ �迭
    public GameObject[] B_A_Gems; // �Ѿ� + �Ӽ� ������Ʈ �迭
    public GameObject[] B_F_Gems; // �Ѿ� + ��� ������Ʈ �迭
    public GameObject[] B_A_F_Gems; // �Ѿ� + �Ӽ� + ��� ������Ʈ �迭
    public float currentGemNum; // ���� ���õ� ���� ��
    public int gemIndex; // ���� ���� �ε���

    public TMP_Text ExpansionText;

    [Header("���� ����")]
    public GameObject faileEffect;
    public GameObject failePostion;

    private void Awake()
    {
        gemUI = GetComponent<GemUI>();
        gemManager = GetComponent<GemManager>();
    }

    private void Start()
    {
        // �⺻ ���� ���� �ʱ�ȭ
        InitializeDefaultGem();
    }

    private void Update()
    {
        // ���õ� ���� ����
        SelectGem(selectBulletNum, selectAttributeNum, selectFunctionNum);
    }

    private void InitializeDefaultGem()
    {
        // �⺻ ���� ����
        currentGemNum = 1; // �⺻ ��
        selectBulletNum = 1; // �⺻ źȯ
    }

    private void ResetGem()
    {
        // ��� ���� ��Ȱ��ȭ
        DeactivateAllGems(B_Gems);
        DeactivateAllGems(B_A_Gems);
        DeactivateAllGems(B_F_Gems);
        DeactivateAllGems(B_A_F_Gems);
    }

    private void DeactivateAllGems(GameObject[] gems)
    {
        // ���޵� ���� �迭�� ��� ������Ʈ ��Ȱ��ȭ
        foreach (var gem in gems)
        {
            gem.SetActive(false);
        }
    }

    private void SelectGem(int bullet, int attribute, int function)
    {
        // ��� ���� ��Ȱ��ȭ
        ResetGem();

        // ���� ��� ���� ���� üũ
        if (!CheckGemAvailability(bullet, attribute, function))
        {
            ActivateDefaultGem(); // �⺻ ���� Ȱ��ȭ
            StartCoroutine(CombinationFailed()); // ���� ����Ʈ ����
            gemUI.CombinationFailedUI(); // UI �ʱ�ȭ
            return;
        }

        // ���� �ε��� �� �� ���
        gemIndex = GetGemIndex(bullet, attribute, function);
        currentGemNum = CalculateCurrentGem(bullet, attribute, function);

        // ����(�⺻, ���� �Ѿ� �� �ؽ�Ʈ ���)
        if((1.3f <= currentGemNum && currentGemNum < 1.4f) || (2.3f <= currentGemNum && currentGemNum < 2.4f))
        {
            ExpansionText.gameObject.SetActive(true);
        }
        else
        {
            ExpansionText.gameObject.SetActive(false);
        }

        // ���� Ȱ��ȭ
        ActivateGem(bullet, attribute, function);

        gunTexture.TextuerSetting(); // �� ��������

        // ���� �����϶� ��� �ӵ� ����
        ChangeShotSpeed();
    }

    private void ActivateDefaultGem()
    {
        // �⺻ ���� Ȱ��ȭ
        B_Gems[0].SetActive(true);
        currentGemNum = 1;
        gemIndex = -6;
        selectBulletNum = 1;
        selectAttributeNum = 0;
        selectFunctionNum = 0;
    }
    IEnumerator CombinationFailed()
    {
        GameObject effect = Instantiate(faileEffect, failePostion.transform.position, Quaternion.identity, failePostion.transform);

        yield return new WaitForSeconds(1f);

        Destroy(effect);
    }


    private void ActivateGem(int bullet, int attribute, int function)
    {
        // ���õ� ���� Ȱ��ȭ
        if (bullet == 1)
        {
            ActivateStandardGem(attribute, function); // �⺻ź ���� Ȱ��ȭ
        }
        else if (bullet == 2)
        {
            ActivateLargeGem(attribute, function); // ����ź ���� Ȱ��ȭ
        }
    }

    private void ActivateStandardGem(int attribute, int function)
    {
        // �⺻ź ���� Ȱ��ȭ
        if (attribute == 0 && function == 0)
        {
            B_Gems[0].SetActive(true); // �Ӽ�x, ���x
            projectilesScript.effectToSpawn = projectilesScript.B_Bullets[0]; // Ȱ��ȭ�� ������ ���� ����ü ����
            projectilesScript.b_Bullet = true;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = false;
        }
        else if (attribute == 0)
        {
            B_F_Gems[function - 1].SetActive(true); // �Ӽ�x, ���o
            projectilesScript.effectToSpawn = projectilesScript.B_F_Bullets[function - 1];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = true;
            projectilesScript.b_A_F_Bullet = false;
        }
        else if (function == 0)
        {
            B_A_Gems[attribute - 1].SetActive(true); // ���x, �Ӽ�o
            projectilesScript.effectToSpawn = projectilesScript.B_A_Bullets[attribute - 1];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = true;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = false;
        }
        else
        {
            B_A_F_Gems[gemIndex].SetActive(true); // �Ӽ�o, ���o
            projectilesScript.effectToSpawn = projectilesScript.B_A_F_Bullets[gemIndex];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = true;
        }
    }

    private void ActivateLargeGem(int attribute, int function)
    {
        // ����ź ���� Ȱ��ȭ
        if (attribute == 0 && function == 0)
        {
            B_Gems[1].SetActive(true); // �Ӽ�x, ���x
            projectilesScript.effectToSpawn = projectilesScript.B_Bullets[1];
            projectilesScript.b_Bullet = true;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = false;
        }
        else if (attribute == 0)
        {
            B_F_Gems[function + 4].SetActive(true); // �Ӽ�x, ���o
            projectilesScript.effectToSpawn = projectilesScript.B_F_Bullets[function + 4];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = true;
            projectilesScript.b_A_F_Bullet = false;
        }
        else if (function == 0)
        {
            B_A_Gems[attribute + 3].SetActive(true); // ���x, �Ӽ�o
            projectilesScript.effectToSpawn = projectilesScript.B_A_Bullets[attribute + 3];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = true;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = false;
        }
        else
        {
            B_A_F_Gems[gemIndex + 20].SetActive(true); // �Ӽ�o, ���o
            projectilesScript.effectToSpawn = projectilesScript.B_A_F_Bullets[gemIndex + 20];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = true;
        }
    }

    private bool CheckGemAvailability(int bullet, int attribute, int function)
    {
        // ���� ��� ���� ���� üũ
        if (bullet == 2 && !gemManager.onLarge) return false;
        if (attribute == 1 && !gemManager.onControl) return false;
        if (attribute == 2 && !gemManager.onElectricity) return false;
        if (attribute == 3 && !gemManager.onExpansion) return false;
        if (attribute == 4 && !gemManager.onGravity) return false;
        if (function == 1 && !gemManager.onPenetrate) return false;
        if (function == 2 && !gemManager.onDestruction) return false;
        if (function == 3 && !gemManager.onDiffusion) return false;
        if (function == 4 && !gemManager.onUpgrade) return false;
        if (function == 5 && !gemManager.onQuick) return false;
        return true;
    }

    private int GetGemIndex(int bullet, int attribute, int function)
    {
        // ���� �ε��� ���
        return (attribute - 1) * 5 + (function - 1);
    }

    private float CalculateCurrentGem(int bullet, int attribute, int function)
    {
        // ���� �� ���
        return bullet + attribute * 0.1f + function * 0.01f;
    }

    // ���� �����϶� ��� �ӵ� ����
    public void ChangeShotSpeed()
    {
        if (currentGemNum > 0 && Mathf.Abs(currentGemNum * 100 % 10 - 5) < 0.01f)
        {
            projectilesScript.fireSpeed = 0.7f;
        }
        else
        {
            projectilesScript.fireSpeed = 1f;
        }
    }
}
