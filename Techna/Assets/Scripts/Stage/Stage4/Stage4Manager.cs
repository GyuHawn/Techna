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

    // UI
    public TMP_Text waveText; // 웨이브 텍스트
    public TMP_Text timeText; // 시간
    public TMP_Text guidText; // 가이드
    public TMP_Text monsterCountText; // 남은 몬스터 수

    public GameObject portal; // 클리어 포탈

    void Update()
    {
        if (!start)
        {
            GameStart();
        }
        if (start && summonMonster.currentMonsterCount == 0)
        {
            StartCoroutine(WaitingStage());
        }

        UpdateMonsterCount(); // 남은 몬스터 수 표시
        UpdateWaitingTime(); // 준비 시간 표시
    }

    // 남은 몬스터 수 표시
    void UpdateMonsterCount()
    {
        if(summonMonster.totalMonsterCount != summonMonster.currentMonsterCount)
        {
            monsterCountText.text = "남은 몬스터 수 : " + summonMonster.currentMonsterCount;
        }
    }

    // 준비 시간 표시
    void UpdateWaitingTime()
    {
        if (gameTime > 0)
        {
            timeText.text = "준비 시간: " + gameTime.ToString("F2") + "초";
        }
    }

    // 현재 웨이브 표시
    public void UpdateWaveText()
    {
        waveText.text = "Wave : " + wave;
    }

    // 시작시 30초후 시작
    void GameStart()
    {
        //if(gameTime < 30)
        if (gameTime < 2)
        {
            StartCoroutine(ShowGuidText()); // 가이트 텍스트 표시
            timeText.gameObject.SetActive(true); // 타임 텍스트 표시
            gameTime += Time.deltaTime;
        }

        //if(gameTime >= 30)
        if (gameTime >= 2)
        {
            summonMonster.Summon();
            timeText.gameObject.SetActive(false); // 타임 텍스트 비표시
            gameTime = 0;
            start = true; // 시작 여부 변경전 토탈 몬스터 수 변경 되는지 확인 필요
        }

        if (wave == 5 && summonMonster.currentMonsterCount <= 0 && !clear)
        {
            GameClear();
        }
    }

    // 가이트 텍스트 표시
    IEnumerator ShowGuidText()
    {
        guidText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        guidText.gameObject.SetActive(false);
    }

    // 몬스터 모두 사망시 20초 대기시간
    IEnumerator WaitingStage()
    {
        if (wave == 0)
        {
            gameTime = 1f;
        }
        else if (wave > 0)
        {
            gameTime = 10f;
        }

        yield return new WaitForSeconds(gameTime);

        summonMonster.Summon();
    }

    // 모든 웨이브 클리어시 포탈생성
    void GameClear()
    {
        clear = true;
        portal.SetActive(true);
    }
}
