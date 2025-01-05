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

    [Header("UI")]
    public TMP_Text waveText; // ���̺� �ؽ�Ʈ
    public TMP_Text timeText; // �ð�
    public TMP_Text guidText; // ���̵�
    public TMP_Text monsterCountText; // ���� ���� ��

    [Header("����")]
    public GameObject boss;
    public GameObject bossUI;

    public GameObject portal; // Ŭ���� ��Ż


    void Start()
    {
        //gameTime = 30f; // ���� ���� �� 30�ʷ� �ʱ�ȭ
        gameTime = 5f; 
    }

    void Update()
    {
        UpdateUI();

        if (!start) // ���� ����
        {
            GameStart();
        }
        else if (start && summonMonster.currentMonsterCount == 0 && !waiting) // �غ� �� ���� ��ȯ
        {
            StartCoroutine(WaitingStage());
        }

        if (wave >= 5 && summonMonster.currentMonsterCount <= 0 && !clear)
        {
            GameClear();
        }
    }

    private void UpdateUI() // UI ������Ʈ
    {
        monsterCountText.text = "���� ���� �� : " + summonMonster.currentMonsterCount;
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
            start = true; // ���� ���� ������ ��Ż ���� �� ���� �Ǵ��� Ȯ�� �ʿ�
            summonMonster.Summon();
            timeText.gameObject.SetActive(false); // Ÿ�� �ؽ�Ʈ ��ǥ��
        }      
    }

    // ����Ʈ �ؽ�Ʈ ǥ��
    IEnumerator ShowGuidText()
    {
        yield return new WaitForSeconds(5f);

        guidText.gameObject.SetActive(false);
    }

    // ���� ��� ����� ���ð�
    IEnumerator WaitingStage()
    {
        wave++;
        waiting = true;
        timeText.gameObject.SetActive(true);
        gameTime = wave == 0 ? 1f : 2f;

        while (gameTime > 0)
        {
            timeText.text = "��� �ð� : " + gameTime.ToString("F2") + "��";
            gameTime -= Time.deltaTime; // ���� �ð� ����
            yield return null; // ���� �����ӱ��� ���
        }
        
        if(wave == 5)
        {
            BossStage();
        }

        summonMonster.Summon();
        timeText.gameObject.SetActive(false);
        waiting = false;
    }

    // ���� ��ȯ
    void BossStage()
    {
        boss.SetActive(true);
        bossUI.SetActive(true);
    }
    
    // ��� ���̺� Ŭ����� ��Ż����
    void GameClear()
    {
        clear = true;
        this.enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
    }
}
