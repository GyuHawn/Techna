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

    public GameObject summonEffect;

    // 웨이브 당 소환할 몬스터 수 설정
    public void SummonCount()
    {
            Debug.Log("wave = " + stage4Manager.wave);
        switch (stage4Manager.wave)
        {
            case 1:
                creepCount = 5;
                totalMonsterCount = 5;
                break;
            case 2:
                creepCount = 5;
                demonCount = 3;
                totalMonsterCount = 8;
                break;
            case 3:
                creepCount = 7;
                demonCount = 5;
                totalMonsterCount = 10;
                break;
            case 4:
                creepCount = 10;
                demonCount = 7;
                totalMonsterCount = 17;
                break;
            case 5:
                creepCount = 5;
                demonCount = 2;
                totalMonsterCount = 7;
                break;
            default:
                creepCount = 0;
                demonCount = 0;
                totalMonsterCount = 0;
                break;
        }

        currentMonsterCount = totalMonsterCount; 
    }

    // 몬스터 카운트 텍스트 설정
    void UpdateMonsterCountText() 
    {
        stage4Manager.monsterCountText.text = "남은 몬스터 수 : " + currentMonsterCount;
    }


    // 몬스터 소환
    public void Summon()
    {  
        if(stage4Manager.wave <= 5)
        {
            stage4Manager.UpdateWaveText();
        }

        SummonCount();

        if (creepCount > 0)
        {
            StartCoroutine(SummonMonsters(creepCount, creepSummonPositions, monsterSummonSetting.creepQueue));
        }
        if (demonCount > 0)
        {
            StartCoroutine(SummonMonsters(demonCount, demonSummonPositions, monsterSummonSetting.demonQueue));
        }
    }

    // 몬스터 소환
    private IEnumerator SummonMonsters(int count, Transform[] summonPositions, Queue<GameObject> monsterQueue)
    {
        for (int i = 0; i < count; i++)
        {
            // 랜덤 위치 선택
            Transform randomPosition = summonPositions[Random.Range(0, summonPositions.Length)];

            // 몬스터 소환
            GameObject monster = monsterSummonSetting.Summon(monsterQueue);
            if (monster != null) // null 체크 추가
            {
                // 몬스터 이동
                monster.transform.position = randomPosition.position;
                monster.SetActive(true); // 활성화
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
