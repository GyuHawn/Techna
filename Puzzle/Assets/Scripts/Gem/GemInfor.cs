using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    private GemManager gemManager;

    public string infor; // 정보 
    public GameObject inforUI; // 정보 UI
    public int inforUIWidthSize; // 힌트 UI 너비 사이즈
    public int inforUIHeightSize; // 힌트 UI 높이 사이즈
    public TMP_Text inforText; // 정보 텍스트
    public int textSize; // 텍스트 사이즈
    public Color textColor; // 색 
    public bool showUI; // 표시 중 여부

    public bool gemInfor; // 보석 정보 인지
    public bool gem; // 보석 인지

    private void Awake()
    {
        if (!gemManager)
            gemManager = GameObject.Find("GemManager").GetComponent<GemManager>();
    }

    private void Start()
    {
        if (infor != null)
        {
            infor = infor.Replace(@"\n", "\n"); // 줄바꿈 수동 설정
        }
        if (inforText != null)
        {
            inforText.color = textColor; // 색 설정
        }       
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어 충돌 시
        if (collision.gameObject.CompareTag("Player"))
        {         
            if (gem)
            {
                gemManager.CollectGem(gameObject.name); // 보석 이름 확인
                Destroy(gameObject); // 보석 제거
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 플레이어 충돌 시 힌트 출력
        if (other.gameObject.CompareTag("Player"))
        {
            if (gemInfor && !showUI)
            {
                showUI = true;
                inforUI.gameObject.SetActive(true);
                inforText.gameObject.SetActive(true);
                inforText.color = textColor;
                inforText.text = infor;

                // UI 사이즈 조절
                RectTransform inforUIRect = inforUI.GetComponent<RectTransform>();
                inforUIRect.sizeDelta = new Vector2(inforUIWidthSize, inforUIHeightSize);

                // 폰트 크기 조절
                inforText.fontSize = textSize;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어 충돌 종료시 충돌 종료
        if (other.gameObject.CompareTag("Player"))
        {
            if (gemInfor && showUI)
            {
                showUI = false;
                inforUI.gameObject.SetActive(false);
                inforText.gameObject.SetActive(false);
                inforText.text = "";
            }
        }
    }
}
