using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SummonMonster : MonoBehaviour
{
    public MonsterSummonSetting monsterSummonSetting;

    // 몬스터 소환 위치
    public Transform[] creepSummonPositions;
    public Transform[] demonSummonPositions;

    public int wave; // 웨이브
    public float gameTime; // 시간
    public int creepCount; // 소환할 몬스터 수
    public int demonCount;
    public int totalMonsterCount; // 전체 몬스터 수

    public bool start; // 시작 여부
    public bool waiting; // 대기 여부
    public bool clear; // 클리어 여부

    // UI
    public TMP_Text waveText;
    public TMP_Text timeText;
    public TMP_Text guidText;

    public GameObject portal; // 클리어 포탈

    void Update()
    {
        if (!start)
        {
            GameStart();
        }

        if (start && totalMonsterCount == 0)
        {
            StartCoroutine(WaitingStage());
        }

        if(wave == 5 && totalMonsterCount <= 0 && !clear)
        {
            GameClear();
        }
    }

    // 웨이브 당 소환할 몬스터 수 설정
    public void SummonCount()
    {
        switch (wave)
        {
            case 1:
                creepCount = 10;
                totalMonsterCount = 10;
                break;
            case 2:
                creepCount = 12;
                demonCount = 3;
                totalMonsterCount = 15;
                break;
            case 3:
                creepCount = 15;
                demonCount = 5;
                totalMonsterCount = 20;
                break;
            case 4:
                creepCount = 15;
                demonCount = 10;
                totalMonsterCount = 25;
                break;
            case 5:
                creepCount = 7;
                demonCount = 3;
                totalMonsterCount = 10;
                break;
        }
    }

    // 시작시 30초후 시작
    void GameStart()
    {
        //if(gameTime < 30)
        if(gameTime < 2)
        {
            StartCoroutine(ShowGuidText()); // 가이트 텍스트 표시
            timeText.gameObject.SetActive(true); // 타임 텍스트 표시
            gameTime += Time.deltaTime;
        }

        //if(gameTime >= 30)
        if(gameTime >= 2)
        {
            Summon();
            timeText.gameObject.SetActive(false); // 타임 텍스트 비표시
            gameTime = 0;
            start = true; // 시작 여부 변경전 토탈 몬스터 수 변경 되는지 확인 필요
        }
    }

    // 가이트 텍스트 표시
    IEnumerator ShowGuidText()
    {
        guidText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        guidText.gameObject.SetActive(false);
    }

    // 몬스터 소환
    void Summon()
    {
        if(wave <= 5)
        {
            wave++;
        }

        SummonCount();
        if(creepCount > 0)
        {
            StartCoroutine(CreepSummon());
        }
        if(demonCount > 0)
        {
            StartCoroutine(DemonSummon());
        }
    }
    
    IEnumerator CreepSummon()
    {
        for (int i = 0; i < creepCount; i++)
        {
            GameObject creep = monsterSummonSetting.MonstetSummon(monsterSummonSetting.creepQueue);
            // 랜덤 위치 선택
            Transform randomPosition = creepSummonPositions[Random.Range(0, creepSummonPositions.Length)];
            // 몬스터 이동
            creep.transform.position = randomPosition.position;

            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator DemonSummon()
    {
        for (int i = 0; i < demonCount; i++)
        {
            GameObject demon = monsterSummonSetting.MonstetSummon(monsterSummonSetting.demonQueue);
            // 랜덤 위치 선택
            Transform randomPosition = creepSummonPositions[Random.Range(0, demonSummonPositions.Length)];
            // 몬스터 이동
            demon.transform.position = randomPosition.position;

            yield return new WaitForSeconds(2f);
        }
    }

    // 몬스터 모두 사망시 20초 대기시간
    IEnumerator WaitingStage()
    {
        if(wave == 0)
        {
            gameTime = 1f;
        }
        else if (wave > 0)
        {
            gameTime = 10f;
        }

        yield return new WaitForSeconds(gameTime);

        Summon();
    }

    // 모든 웨이브 클리어시 포탈생성
    void GameClear()
    {
        clear = true;
        portal.SetActive(true);
    }
}
