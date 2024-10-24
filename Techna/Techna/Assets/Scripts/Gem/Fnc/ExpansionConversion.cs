using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionConversion : MonoBehaviour
{
    public GemCombination gem; // 현재 보석 상태 확인용
    public GameObject[] state; // 상태 오브젝트
    public bool plus; // 현재 상태

    void Start()
    {
        plus = true;
        SetState(plus); // 초기 상태 설정
    }

    void Update()
    {
        // 보석 상태가 확장 보석(1.3f)일 때만 상태 변환 가능
        if (gem.currentGemNum == 1.3f && Input.GetButtonDown("Expansion")) //
        {
            plus = !plus; // 상태를 반전
            SetState(plus); // 상태 적용
        }
    }

    // 상태에 따라 오브젝트 활성/비활성화
    private void SetState(bool isPlus)
    {
        state[0].SetActive(isPlus);
        state[1].SetActive(!isPlus);
    }
}
