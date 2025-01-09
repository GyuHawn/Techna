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

    [Header("선택한 번호")]
    public int selectBulletNum; // 선택된 총알 번호
    public int selectAttributeNum; // 선택된 속성 번호
    public int selectFunctionNum; // 선택된 기능 번호

    [Header("총알 오브젝트")]
    public GameObject[] B_Gems; // 총알 오브젝트 배열
    public GameObject[] B_A_Gems; // 총알 + 속성 오브젝트 배열
    public GameObject[] B_F_Gems; // 총알 + 기능 오브젝트 배열
    public GameObject[] B_A_F_Gems; // 총알 + 속성 + 기능 오브젝트 배열
    public float currentGemNum; // 현재 선택된 보석 값
    public int gemIndex; // 현재 보석 인덱스

    public TMP_Text ExpansionText;

    [Header("조합 실패")]
    public GameObject faileEffect;
    public GameObject failePostion;

    private void Awake()
    {
        gemUI = GetComponent<GemUI>();
        gemManager = GetComponent<GemManager>();
    }

    private void Start()
    {
        // 기본 보석 설정 초기화
        InitializeDefaultGem();
    }

    private void Update()
    {
        // 선택된 보석 적용
        SelectGem(selectBulletNum, selectAttributeNum, selectFunctionNum);
    }

    private void InitializeDefaultGem()
    {
        // 기본 보석 설정
        currentGemNum = 1; // 기본 값
        selectBulletNum = 1; // 기본 탄환
    }

    private void ResetGem()
    {
        // 모든 보석 비활성화
        DeactivateAllGems(B_Gems);
        DeactivateAllGems(B_A_Gems);
        DeactivateAllGems(B_F_Gems);
        DeactivateAllGems(B_A_F_Gems);
    }

    private void DeactivateAllGems(GameObject[] gems)
    {
        // 전달된 보석 배열의 모든 오브젝트 비활성화
        foreach (var gem in gems)
        {
            gem.SetActive(false);
        }
    }

    private void SelectGem(int bullet, int attribute, int function)
    {
        // 모든 보석 비활성화
        ResetGem();

        // 보석 사용 가능 여부 체크
        if (!CheckGemAvailability(bullet, attribute, function))
        {
            ActivateDefaultGem(); // 기본 보석 활성화
            StartCoroutine(CombinationFailed()); // 실패 이펙트 생성
            gemUI.CombinationFailedUI(); // UI 초기화
            return;
        }

        // 보석 인덱스 및 값 계산
        gemIndex = GetGemIndex(bullet, attribute, function);
        currentGemNum = CalculateCurrentGem(bullet, attribute, function);

        // 증감(기본, 대형 총알 시 텍스트 출력)
        if((1.3f <= currentGemNum && currentGemNum < 1.4f) || (2.3f <= currentGemNum && currentGemNum < 2.4f))
        {
            ExpansionText.gameObject.SetActive(true);
        }
        else
        {
            ExpansionText.gameObject.SetActive(false);
        }

        // 보석 활성화
        ActivateGem(bullet, attribute, function);

        gunTexture.TextuerSetting(); // 총 재질변경

        // 연사 보석일때 사격 속도 변경
        ChangeShotSpeed();
    }

    private void ActivateDefaultGem()
    {
        // 기본 보석 활성화
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
        // 선택된 보석 활성화
        if (bullet == 1)
        {
            ActivateStandardGem(attribute, function); // 기본탄 보석 활성화
        }
        else if (bullet == 2)
        {
            ActivateLargeGem(attribute, function); // 대형탄 보석 활성화
        }
    }

    private void ActivateStandardGem(int attribute, int function)
    {
        // 기본탄 보석 활성화
        if (attribute == 0 && function == 0)
        {
            B_Gems[0].SetActive(true); // 속성x, 기능x
            projectilesScript.effectToSpawn = projectilesScript.B_Bullets[0]; // 활성화된 보석에 따른 투사체 선택
            projectilesScript.b_Bullet = true;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = false;
        }
        else if (attribute == 0)
        {
            B_F_Gems[function - 1].SetActive(true); // 속성x, 기능o
            projectilesScript.effectToSpawn = projectilesScript.B_F_Bullets[function - 1];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = true;
            projectilesScript.b_A_F_Bullet = false;
        }
        else if (function == 0)
        {
            B_A_Gems[attribute - 1].SetActive(true); // 기능x, 속성o
            projectilesScript.effectToSpawn = projectilesScript.B_A_Bullets[attribute - 1];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = true;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = false;
        }
        else
        {
            B_A_F_Gems[gemIndex].SetActive(true); // 속성o, 기능o
            projectilesScript.effectToSpawn = projectilesScript.B_A_F_Bullets[gemIndex];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = true;
        }
    }

    private void ActivateLargeGem(int attribute, int function)
    {
        // 대형탄 보석 활성화
        if (attribute == 0 && function == 0)
        {
            B_Gems[1].SetActive(true); // 속성x, 기능x
            projectilesScript.effectToSpawn = projectilesScript.B_Bullets[1];
            projectilesScript.b_Bullet = true;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = false;
        }
        else if (attribute == 0)
        {
            B_F_Gems[function + 4].SetActive(true); // 속성x, 기능o
            projectilesScript.effectToSpawn = projectilesScript.B_F_Bullets[function + 4];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = true;
            projectilesScript.b_A_F_Bullet = false;
        }
        else if (function == 0)
        {
            B_A_Gems[attribute + 3].SetActive(true); // 기능x, 속성o
            projectilesScript.effectToSpawn = projectilesScript.B_A_Bullets[attribute + 3];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = true;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = false;
        }
        else
        {
            B_A_F_Gems[gemIndex + 20].SetActive(true); // 속성o, 기능o
            projectilesScript.effectToSpawn = projectilesScript.B_A_F_Bullets[gemIndex + 20];
            projectilesScript.b_Bullet = false;
            projectilesScript.b_A_Bullet = false;
            projectilesScript.b_F_Bullet = false;
            projectilesScript.b_A_F_Bullet = true;
        }
    }

    private bool CheckGemAvailability(int bullet, int attribute, int function)
    {
        // 보석 사용 가능 여부 체크
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
        // 보석 인덱스 계산
        return (attribute - 1) * 5 + (function - 1);
    }

    private float CalculateCurrentGem(int bullet, int attribute, int function)
    {
        // 보석 값 계산
        return bullet + attribute * 0.1f + function * 0.01f;
    }

    // 연사 보석일때 사격 속도 변경
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
