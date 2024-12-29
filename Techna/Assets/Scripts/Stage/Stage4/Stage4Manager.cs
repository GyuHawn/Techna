using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage4Manager : MonoBehaviour
{
    public SummonMonster summonMonster;

    public int wave; // ���̺�
    public float gameTime; // �ð�

    public bool start; // ���� ����
    public bool waiting; // ��� ����
    public bool clear; // Ŭ���� ����

    // UI
    public TMP_Text waveText; // ���̺� �ؽ�Ʈ
    public TMP_Text timeText; // �ð�
    public TMP_Text guidText; // ���̵�
    public TMP_Text monsterCountText; // ���� ���� ��

    public GameObject portal; // Ŭ���� ��Ż

    void Start()
    {
        //gameTime = 30f; // ���� ���� �� 30�ʷ� �ʱ�ȭ
        gameTime = 10f; 
    }

    void Update()
    {
        if (!start)
        {
            GameStart();
        }
        else if (start && summonMonster.currentMonsterCount == 0 && !waiting)
        {
            StartCoroutine(WaitingStage());
        }

        UpdateMonsterCount(); // ���� ���� �� ǥ��
        UpdateWaitingTime(); // �غ� �ð� ǥ��

        if (Input.GetKeyDown(KeyCode.B)) // �׽�Ʈ��
        {
            Attack();
        }

        if (wave >= 5 && summonMonster.currentMonsterCount <= 0 && !clear)
        {
            GameClear();
        }
    }

    // ���� ���� �� ǥ��
    void UpdateMonsterCount()
    {
        if(summonMonster.totalMonsterCount != summonMonster.currentMonsterCount)
        {
            monsterCountText.text = "���� ���� �� : " + summonMonster.currentMonsterCount;
        }
    }

    // �غ� �ð� ǥ��
    void UpdateWaitingTime()
    {
        if (gameTime > 0)
        {
            timeText.text = "�غ� �ð� : " + gameTime.ToString("F2") + "��";
        }
    }

    // ���� ���̺� ǥ��
    public void UpdateWaveText()
    {
        waveText.text = "Wave " + wave;
    }

    // ���۽� 30���� ����
    void GameStart()
    {
        if (gameTime > 0)
        {
            StartCoroutine(ShowGuidText()); // ����Ʈ �ؽ�Ʈ ǥ��
            timeText.gameObject.SetActive(true); // Ÿ�� �ؽ�Ʈ ǥ��
            gameTime -= Time.deltaTime;
        }
        else
        {
            summonMonster.Summon();
            timeText.gameObject.SetActive(false); // Ÿ�� �ؽ�Ʈ ��ǥ��
            start = true; // ���� ���� ������ ��Ż ���� �� ���� �Ǵ��� Ȯ�� �ʿ�
        }      
    }

    // ����Ʈ �ؽ�Ʈ ǥ��
    IEnumerator ShowGuidText()
    {
        yield return new WaitForSeconds(5f);

        guidText.gameObject.SetActive(false);
    }

    // ���� ��� ����� 20�� ���ð�
    IEnumerator WaitingStage()
    {
        waiting = true;
        timeText.gameObject.SetActive(true);

        if (wave == 0)
        {
            gameTime = 1f;
        }
        else if (wave > 0)
        {
            //gameTime = 10f;
            gameTime = 2f;
        }

        while (gameTime > 0)
        {
            UpdateWaitingTime(); // ��� �ð� �ؽ�Ʈ ������Ʈ
            gameTime -= Time.deltaTime; // ���� �ð� ����
            yield return null; // ���� �����ӱ��� ���
        }

        summonMonster.Summon();
        timeText.gameObject.SetActive(false);
        waiting = false;
    }

    // ��� ���̺� Ŭ����� ��Ż����
    void GameClear()
    {
        clear = true;
        portal.SetActive(true);
        
        this.enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
    }


    public void Attack()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            if (monster.name == "Creep")
            {
                CreepMonsterController creep = monster.GetComponent<CreepMonsterController>();
                creep.currentHealth -= 100;
            }
            else if(monster.name == "Demon")
            {
                DemonMonsterController demon = monster.GetComponent<DemonMonsterController>();
                demon.currentHealth -= 100;
            }
        }
    }
}
