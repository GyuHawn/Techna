using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage4Manager : MonoBehaviour
{
    public SummonMonster summonMonster;

    public int wave; // 웨이브
    public float gameTime; // 시간

    public bool start; // 시작 여부
    public bool waiting; // 대기 여부
    public bool clear; // 클리어 여부

    [Header("UI")]
    public TMP_Text waveText; // 웨이브 텍스트
    public TMP_Text timeText; // 시간
    public TMP_Text guidText; // 가이드
    public TMP_Text monsterCountText; // 남은 몬스터 수

    [Header("보스")]
    public GameObject boss;
    public GameObject bossUI;

    public GameObject portal; // 클리어 포탈


    void Start()
    {
        //gameTime = 30f; // 게임 시작 시 30초로 초기화
        gameTime = 5f; 
    }

    void Update()
    {
        UpdateUI();

        if (!start) // 게임 시작
        {
            GameStart();
        }
        else if (start && summonMonster.currentMonsterCount == 0 && !waiting) // 준비 및 몬스터 소환
        {
            StartCoroutine(WaitingStage());
        }

        if (wave >= 5 && summonMonster.currentMonsterCount <= 0 && !clear)
        {
            GameClear();
        }
    }

    private void UpdateUI() // UI 업데이트
    {
        monsterCountText.text = "남은 몬스터 수 : " + summonMonster.currentMonsterCount;
        if (gameTime > 0)
        {
            timeText.text = "준비 시간 : " + gameTime.ToString("F2") + "초";
        }
    }

    // 현재 웨이브 표시
    public void UpdateWaveText()
    {
        waveText.text = "Wave " + wave;
    }

    // 시작시 30초후 시작
    void GameStart()
    {
        if (gameTime > 0)
        {
            StartCoroutine(ShowGuidText()); // 가이트 텍스트 표시
            timeText.gameObject.SetActive(true); // 타임 텍스트 표시
            gameTime -= Time.deltaTime;
        }
        else
        {
            start = true; // 시작 여부 변경전 토탈 몬스터 수 변경 되는지 확인 필요
            summonMonster.Summon();
            timeText.gameObject.SetActive(false); // 타임 텍스트 비표시
        }      
    }

    // 가이트 텍스트 표시
    IEnumerator ShowGuidText()
    {
        yield return new WaitForSeconds(5f);

        guidText.gameObject.SetActive(false);
    }

    // 몬스터 모두 사망시 대기시간
    IEnumerator WaitingStage()
    {
        wave++;
        waiting = true;
        timeText.gameObject.SetActive(true);
        gameTime = wave == 0 ? 1f : 2f;

        while (gameTime > 0)
        {
            timeText.text = "대기 시간 : " + gameTime.ToString("F2") + "초";
            gameTime -= Time.deltaTime; // 남은 시간 감소
            yield return null; // 다음 프레임까지 대기
        }
        
        if(wave == 5)
        {
            BossStage();
        }

        summonMonster.Summon();
        timeText.gameObject.SetActive(false);
        waiting = false;
    }

    // 보스 소환
    void BossStage()
    {
        boss.SetActive(true);
        bossUI.SetActive(true);
    }
    
    // 모든 웨이브 클리어시 포탈생성
    void GameClear()
    {
        clear = true;
        this.enabled = false; // 스크립트 비활성화
    }
}
