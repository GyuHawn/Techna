using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    public GemManager gemManager;

    [Header("정보")]
    public string infor; // 정보 
    public TMP_Text inforText; // 정보 텍스트

    [Header("텍스트 설정")]
    public int textSize; // 텍스트 사이즈
    public Color textColor; // 색 
    public bool showUI; // 표시 중 여부

    [Header("보석 or 정보")]
    public bool gem; // 보석 인지
    public bool gemInfor; // 보석 정보 인지

    private void Start()
    {
        FindNullObject(); // 빠진 오브젝트 찾기

        // 줄바꿈 및 색상 설정
        if (!string.IsNullOrEmpty(infor))
        {
            infor = infor.Replace(@"\n", "\n");
        }
    }

    void FindNullObject() // 빠진 오브젝트 찾기
    {
        if (gemManager == null)
        {
            gemManager = FindObjectOfType<GemManager>();
        }

        if (gemInfor)
        {
            if (inforText == null)
            {
                inforText = GameObject.Find("GemInforText").GetComponent<TMP_Text>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gemInfor)
            {
                if (!showUI)
                {
                    showUI = true;
                    DisplayInfoUI(); // 정보 표시
                }
            }

            if (gem)
            {
                gemManager.CollectGem(gameObject.name); // 보석 이름 확인
                Destroy(gameObject); // 보석 제거
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gemInfor && showUI)
        {
            showUI = false; 
            HideInfoUI(); // 정보 숨기기
        }
    }

    private void DisplayInfoUI() // 정보 표시
    {
        inforText.color = textColor; // 색상 설정
        inforText.text = infor;
        inforText.fontSize = textSize;
    }

    private void HideInfoUI() // 정보 숨기기
    {
        inforText.color = Color.white;
        inforText.text = "";
    }
}
