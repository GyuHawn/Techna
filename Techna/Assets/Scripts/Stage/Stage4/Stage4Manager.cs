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

        UpdateMonsterCount(); // ���� ���� �� ǥ��
        UpdateWaitingTime(); // �غ� �ð� ǥ��
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
            timeText.text = "�غ� �ð�: " + gameTime.ToString("F2") + "��";
        }
    }

    // ���� ���̺� ǥ��
    public void UpdateWaveText()
    {
        waveText.text = "Wave : " + wave;
    }

    // ���۽� 30���� ����
    void GameStart()
    {
        //if(gameTime < 30)
        if (gameTime < 2)
        {
            StartCoroutine(ShowGuidText()); // ����Ʈ �ؽ�Ʈ ǥ��
            timeText.gameObject.SetActive(true); // Ÿ�� �ؽ�Ʈ ǥ��
            gameTime += Time.deltaTime;
        }

        //if(gameTime >= 30)
        if (gameTime >= 2)
        {
            summonMonster.Summon();
            timeText.gameObject.SetActive(false); // Ÿ�� �ؽ�Ʈ ��ǥ��
            gameTime = 0;
            start = true; // ���� ���� ������ ��Ż ���� �� ���� �Ǵ��� Ȯ�� �ʿ�
        }

        if (wave == 5 && summonMonster.currentMonsterCount <= 0 && !clear)
        {
            GameClear();
        }
    }

    // ����Ʈ �ؽ�Ʈ ǥ��
    IEnumerator ShowGuidText()
    {
        guidText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        guidText.gameObject.SetActive(false);
    }

    // ���� ��� ����� 20�� ���ð�
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

    // ��� ���̺� Ŭ����� ��Ż����
    void GameClear()
    {
        clear = true;
        portal.SetActive(true);
    }
}
