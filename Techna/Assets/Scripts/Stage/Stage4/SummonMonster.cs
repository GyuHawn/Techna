using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SummonMonster : MonoBehaviour
{
    public MonsterSummonSetting monsterSummonSetting;

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
    public bool clear; // Ŭ���� ����

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

        if(wave == 5 && totalMonsterCount <= 0 && !clear)
        {
            GameClear();
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
        //if(gameTime < 30)
        if(gameTime < 2)
        {
            StartCoroutine(ShowGuidText()); // ����Ʈ �ؽ�Ʈ ǥ��
            timeText.gameObject.SetActive(true); // Ÿ�� �ؽ�Ʈ ǥ��
            gameTime += Time.deltaTime;
        }

        //if(gameTime >= 30)
        if(gameTime >= 2)
        {
            Summon();
            timeText.gameObject.SetActive(false); // Ÿ�� �ؽ�Ʈ ��ǥ��
            gameTime = 0;
            start = true; // ���� ���� ������ ��Ż ���� �� ���� �Ǵ��� Ȯ�� �ʿ�
        }
    }

    // ����Ʈ �ؽ�Ʈ ǥ��
    IEnumerator ShowGuidText()
    {
        guidText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        guidText.gameObject.SetActive(false);
    }

    // ���� ��ȯ
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
            // ���� ��ġ ����
            Transform randomPosition = creepSummonPositions[Random.Range(0, creepSummonPositions.Length)];
            // ���� �̵�
            creep.transform.position = randomPosition.position;

            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator DemonSummon()
    {
        for (int i = 0; i < demonCount; i++)
        {
            GameObject demon = monsterSummonSetting.MonstetSummon(monsterSummonSetting.demonQueue);
            // ���� ��ġ ����
            Transform randomPosition = creepSummonPositions[Random.Range(0, demonSummonPositions.Length)];
            // ���� �̵�
            demon.transform.position = randomPosition.position;

            yield return new WaitForSeconds(2f);
        }
    }

    // ���� ��� ����� 20�� ���ð�
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

    // ��� ���̺� Ŭ����� ��Ż����
    void GameClear()
    {
        clear = true;
        portal.SetActive(true);
    }
}
