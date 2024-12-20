using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    public SummonManager summonManager;

    // ���� ��ȯ ��ġ
    public Transform[] creepSummonPositions;
    public Transform[] demonSummonPositions;

    public int wave; // ���̺�
    public float gameTime; // �ð�
    public int creepCount; // ��ȯ�� ���� ��
    public int demonCount;
    public int totalMonsterCount; // ��ü ���� ��

    public bool start; // ���� ����
    public bool waiting; // ��� ����

    // UI
    public TMP_Text waveText;
    public TMP_Text timeText;
    public TMP_Text guidText;

    public GameObject portal; // Ŭ���� ��Ż

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

    // ���̺� �� ��ȯ�� ���� �� ����
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

    // ���۽� 30���� ����
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
            start = true; // ���� ���� ������ ��Ż ���� �� ���� �Ǵ��� Ȯ�� �ʿ�
        }
    }

    // ���� ��ȯ
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
            // ���� ��ġ ����
            Transform randomPosition = creepSummonPositions[Random.Range(0, creepSummonPositions.Length)];
            // ���� �̵�
            creep.transform.position = randomPosition.position;

            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator DemonSummon()
    {
        for (int i = 0; i < demonCount; i++)
        {
            GameObject demon = summonManager.MonstetSummon(summonManager.demonQueue);
            // ���� ��ġ ����
            Transform randomPosition = creepSummonPositions[Random.Range(0, demonSummonPositions.Length)];
            // ���� �̵�
            demon.transform.position = randomPosition.position;

            yield return new WaitForSeconds(3f);
        }
    }

    // ���� ��� ����� 20�� ���ð�
    IEnumerator WaitingStage()
    {
        yield return new WaitForSeconds(20f);

        gameTime = 0;
        SummonMonster();
    }

    // ��� ���̺� Ŭ����� ��Ż����
    void GameClear()
    {
        portal.SetActive(true);
    }
}
