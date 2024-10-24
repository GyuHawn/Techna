using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FunctionInfor : MonoBehaviour
{
    [Header("정보")]
    public string infor; // 정보
    public TMP_Text inforText; // 정보 텍스트

    [Header("텍스트 설정")]
    public int textSize; // 텍스트 사이즈
    public Color textColor; // 텍스트 색

    public bool showUI; // 표시 중 여부

    void Start()
    {
        FindNullObject();

        // 줄바꿈 및 색상 설정
        if (!string.IsNullOrEmpty(infor))
        {
            infor = infor.Replace(@"\n", "\n");
        }

        if (inforText != null)
        {
            inforText.color = textColor; // 색상 설정
        }
    }

    void FindNullObject() // 빠진 오브젝트 찾기
    {
        if (inforText == null)
        {
            inforText = GameObject.Find("FunctionInforText").GetComponent<TMP_Text>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!showUI)
            {
                showUI = true;
                DisplayInfoUI(); // 정보 표시
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && showUI)
        {
            showUI = false;
            HideInfoUI(); // 정보 숨기기
        }
    }

    private void DisplayInfoUI() // 정보 표시
    {
        inforText.text = infor;
        inforText.fontSize = textSize;
    }

    private void HideInfoUI() // 정보 숨기기
    {
        inforText.text = "";
    }
}
