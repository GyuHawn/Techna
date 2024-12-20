using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    public SummonManager summonManager;

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
        if(gameTime < 30)
        {
            gameTime += Time.deltaTime;
        }

        if(gameTime >= 30)
        {
            SummonMonster();
            gameTime = 0;
            start = true; // 시작 여부 변경전 토탈 몬스터 수 변경 되는지 확인 필요
        }
    }

    // 몬스터 소환
    void SummonMonster()
    {
        SummonCount();
        StartCoroutine(CreepSummon());
        StartCoroutine(DemonSummon());
    }
    
    IEnumerator CreepSummon()
    {
        for (int i = 0; i < creepCount; i++)
        {
            GameObject creep = summonManager.MonstetSummon(summonManager.creepQueue);
            // 랜덤 위치 선택
            Transform randomPosition = creepSummonPositions[Random.Range(0, creepSummonPositions.Length)];
            // 몬스터 이동
            creep.transform.position = randomPosition.position;

            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator DemonSummon()
    {
        for (int i = 0; i < demonCount; i++)
        {
            GameObject demon = summonManager.MonstetSummon(summonManager.demonQueue);
            // 랜덤 위치 선택
            Transform randomPosition = creepSummonPositions[Random.Range(0, demonSummonPositions.Length)];
            // 몬스터 이동
            demon.transform.position = randomPosition.position;

            yield return new WaitForSeconds(3f);
        }
    }

    // 몬스터 모두 사망시 20초 대기시간
    IEnumerator WaitingStage()
    {
        yield return new WaitForSeconds(20f);

        gameTime = 0;
        SummonMonster();
    }

    // 모든 웨이브 클리어시 포탈생성
    void GameClear()
    {
        portal.SetActive(true);
    }
}
