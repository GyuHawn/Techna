using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { None, Fire } // ���� Ÿ�� ������

public class MonsterSummon : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // ���� ������
    public Transform spawnPoint; // ��ȯ ����Ʈ

    // ��ȯ �ð�
    public float currentSpawnTime;
    public float maxSpawnTime;

    public bool activate; // Ȱ��ȭ ����

    private MonsterFactory factory; // ���� Ȯ��

    public MonsterType monsterType;

    void Start()
    {
        factory = new MonsterFactory(); // ���丮 Ŭ���� �ν��Ͻ� ����
    }

    void Update()
    {
        if (activate) // Ȱ��ȭ �� ���� �ð� ���� ���� ����
        {
            currentSpawnTime += Time.deltaTime;

            if (currentSpawnTime >= maxSpawnTime)
            {
                I_Monster monster = factory.CreateMonster(monsterType.ToString(), GetPrefabForType(monsterType), spawnPoint);
                monster?.Summon(); // ���� ��ȯ
                currentSpawnTime = 0f; // ��ȯ �ð� �ʱ�ȭ
            }
        }
    }

    // Ÿ�Կ� ���� ������ ��ȯ
    GameObject GetPrefabForType(MonsterType type)
    {
        switch (type)
        {
            case MonsterType.Fire:
                return monsterPrefabs[0];
            default:
                return null;
        }
    }
}