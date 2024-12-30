using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SummonMonster : MonoBehaviour
{
    public Stage4Manager stage4Manager;
    public MonsterSummonSetting monsterSummonSetting;

    // ���� ��ȯ ��ġ
    public Transform[] creepSummonPositions;
    public Transform[] demonSummonPositions;
    public int creepCount; // ��ȯ�� ���� ��
    public int demonCount;
    public int totalMonsterCount; // ��ü ���� ��  
    public int currentMonsterCount; // ���� ���� ��

    // ���̺� �� ��ȯ�� ���� �� ����
    public void SummonCount()
    {
        switch (stage4Manager.wave)
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
            default:
                creepCount = 0;
                demonCount = 0;
                totalMonsterCount = 0;
                break;
        }

        currentMonsterCount = totalMonsterCount; 
    }

    // ���� ī��Ʈ �ؽ�Ʈ ����
    void UpdateMonsterCountText() 
    {
        stage4Manager.monsterCountText.text = "���� ���� �� : " + stage4Manager.summonMonster.currentMonsterCount;
    }


    // ���� ��ȯ
    public void Summon()
    {  
        if(stage4Manager.wave <= 5)
        {
            stage4Manager.wave++;
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

    // ���� ��ȯ
    private IEnumerator SummonMonsters(int count, Transform[] summonPositions, Queue<GameObject> monsterQueue)
    {
        for (int i = 0; i < count; i++)
        {
            // ���� ��ġ ����
            Transform randomPosition = summonPositions[Random.Range(0, summonPositions.Length)];

            // ���� ��ȯ
            GameObject monster = monsterSummonSetting.MonstetSummon(monsterQueue);
            if (monster != null) // null üũ �߰�
            {
                // ���� �̵�
                monster.transform.position = randomPosition.position;
                monster.SetActive(true); // Ȱ��ȭ
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
