using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SummonMonster : MonoBehaviour
{
    public Stage4Manager stage4Manager;
    public MonsterSummonSetting monsterSummonSetting;

    // 몬스터 소환 위치
    public Transform[] creepSummonPositions;
    public Transform[] demonSummonPositions;
    public int creepCount; // 소환할 몬스터 수
    public int demonCount;
    public int totalMonsterCount; // 전체 몬스터 수  
    public int currentMonsterCount; // 현재 몬스터 수

    // 웨이브 당 소환할 몬스터 수 설정
    public void SummonCount()
    {
        switch (stage4Manager.wave)
        {
            case 1:
                //creepCount = 10;
                //creepCount = 1;
                demonCount = 2;
                totalMonsterCount = 2;
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
            default:
                creepCount = 0;
                demonCount = 0;
                totalMonsterCount = 0;
                break;
        }
        currentMonsterCount = totalMonsterCount;
        // 몬스터 카운트 텍스트 설정
        stage4Manager.monsterCountText.text = "남은 몬스터 수 : " + stage4Manager.summonMonster.currentMonsterCount;
    }
    
    // 몬스터 소환
    public void Summon()
    {
        
        if(stage4Manager.wave <= 5)
        {
            stage4Manager.wave++;
            stage4Manager.UpdateWaveText();
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

            //yield return new WaitForSeconds(1f);
            yield return new WaitForSeconds(0.1f);
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

            //yield return new WaitForSeconds(1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
