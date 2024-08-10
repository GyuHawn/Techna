using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectHintDisplay : MonoBehaviour
{
    public string hintText; // 힌트
    public GameObject hintUI; // 힌트 UI
    public TMP_Text showText; // 표시 텍스트
    public bool showUI; // 표시 중 여부

    void Start()
    {
        showText.gameObject.SetActive(false); // 힌트 비활성화
    }

    private void OnCollisionStay(Collision collision)
    {
        // 플레이어 충돌 시 힌트 출력
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!showUI)
            {
                showUI = true;
                hintUI.gameObject.SetActive(true);
                showText.gameObject.SetActive(true);
                showText.text = hintText; // 힌트 적용
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 플레이어 충돌 종료시 충돌 종료
        if (collision.gameObject.CompareTag("Player"))
        {
            if (showUI)
            {
                showUI = false;
                hintUI.gameObject.SetActive(false);
                showText.gameObject.SetActive(false);
                showText.text = ""; // 텍스트 초기화
            }
        }
    }
}
