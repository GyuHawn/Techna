using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    private GemManager gemManager;

    public string infor; // 정보 
    public GameObject inforUI; // 정보 UI
    public TMP_Text inforText; // 정보 텍스트
    public Color textColor; // 색 

    private void Awake()
    {
        if (!gemManager)
            gemManager = GameObject.Find("GemManager").GetComponent<GemManager>();

        infor = infor.Replace(@"\n", "\n"); // 줄바꿈 수동 설정
        inforText.color = textColor; // 색 설정
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어 충돌 시
        if (collision.gameObject.CompareTag("Player"))
        {
            gemManager.CollectGem(gameObject.name); // 보석 이름 확인
            Destroy(gameObject); // 보석 제거

            StartCoroutine(ShowInforText(3f)); // 정보 표시

        }
    }

    IEnumerator ShowInforText(float time) // 정보 표시 후 비활성화 
    {
        inforUI.gameObject.SetActive(true);
        inforText.gameObject.SetActive(true);
        inforText.text = infor;

        yield return new WaitForSeconds(time);

        inforUI.gameObject.SetActive(false);
        inforText.gameObject.SetActive(false);
        inforText.text = "";
    }
}
