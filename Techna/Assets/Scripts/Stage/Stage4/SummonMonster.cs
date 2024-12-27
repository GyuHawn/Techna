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
                //creepCount = 10;
                creepCount = 1;
                totalMonsterCount = 1;
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
        // ���� ī��Ʈ �ؽ�Ʈ ����
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
            // ���� ��ġ ����
            Transform randomPosition = creepSummonPositions[Random.Range(0, creepSummonPositions.Length)];

            // ���� ��ȯ
            GameObject creep = monsterSummonSetting.MonstetSummon(monsterSummonSetting.creepQueue);
            if (creep != null) // null üũ �߰�
            {
                // ���� �̵�
                creep.transform.position = randomPosition.position;
                creep.SetActive(true); // Ȱ��ȭ
            }

            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator DemonSummon()
    {
        for (int i = 0; i < demonCount; i++)
        {
            // ���� ��ġ ����
            Transform randomPosition = demonSummonPositions[Random.Range(0, demonSummonPositions.Length)];

            // ���� ��ȯ
            GameObject demon = monsterSummonSetting.MonstetSummon(monsterSummonSetting.demonQueue);
            if (demon != null) // null üũ �߰�
            {
                // ���� �̵�
                demon.transform.position = randomPosition.position;
                demon.SetActive(true); // Ȱ��ȭ
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
