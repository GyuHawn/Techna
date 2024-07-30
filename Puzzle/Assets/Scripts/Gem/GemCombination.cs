using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UI;

public class GemCombination : MonoBehaviour
{
    private GemManager gemManager;

    public int selectBulletNum; // 선택된 총알 값
    public int selectAttributeNum; // 선택된 속성 값
    public int selectFunctionNum; // 선택된 기능 값
    public GameObject[] B_Gems; // 총알
    public GameObject[] B_A_Gems; // 총알 + 속성
    public GameObject[] B_F_Gems; // 총알 + 기능
    public GameObject[] B_A_F_Gems; // 총알 + 속성 + 기능
    public float currentGemNum; // 현재 선택된 보석 값

    public Sprite[] crossHair; // 조준점 배열

    private void Awake()
    {
        if (!gemManager)
            gemManager = FindObjectOfType<GemManager>();
    }

    private void Start()
    {
        currentGemNum = 1; // 가장 처음은 기본탄 설정
    }

    void Update()
    {
        // 선택한 보석
        SelectGem(selectBulletNum, selectAttributeNum, selectFunctionNum);
    }

    void ResetGem() // 모든 보석 비활성화
    {
        DeactivateAllGems(B_Gems);
        DeactivateAllGems(B_A_Gems);
        DeactivateAllGems(B_F_Gems);
        DeactivateAllGems(B_A_F_Gems);
    }
    void DeactivateAllGems(GameObject[] gems) // 보석 비활성화
    {
        foreach (var gem in gems)
        {
            gem.SetActive(false);
        }
    }

    void SelectGem(int bullet, int attribute, int function) // 보석 선택
    {
        ResetGem(); // 모든 보석 비활성화

        int gemIndex = GetGemIndex(bullet, attribute, function); // 보석 인덱스 계산
        currentGemNum = CalculateCurrentGem(bullet, attribute, function); // 선택한 보석 값 설정

        if (bullet == 1) // 기본탄
        {
            if (attribute == 0 && function == 0) // 속성x, 기능x
                B_Gems[0].SetActive(true);
            else if (attribute == 0) // 속성x, 기능o
                B_F_Gems[function - 1].SetActive(true);
            else if (function == 0) // 기능x, 속성o
                B_A_Gems[attribute - 1].SetActive(true);
            else // 속성o, 기능o
                B_A_F_Gems[gemIndex].SetActive(true);
        }
        else if (bullet == 2) // 대형탄
        {
            if (attribute == 0 && function == 0) // 속성x, 기능x
                B_Gems[1].SetActive(true);
            else if (attribute == 0) // 속성x, 기능o
                B_F_Gems[function + 4].SetActive(true);
            else if (function == 0) // 기능x, 속성o
                B_A_Gems[attribute + 3].SetActive(true);
            else // 속성o, 기능o
                B_A_F_Gems[gemIndex + 20].SetActive(true);
        }
    }

    int GetGemIndex(int bullet, int attribute, int function) // 보석 인덱스 계산
    {
        return (attribute - 1) * 5 + (function - 1);
    }

    float CalculateCurrentGem(int bullet, int attribute, int function) // 선택한 보석 값 설정
    {
        float baseValue = bullet;
        float attributeValue = attribute * 0.1f;
        float functionValue = function * 0.01f;
        return baseValue + attributeValue + functionValue;
    }
}
