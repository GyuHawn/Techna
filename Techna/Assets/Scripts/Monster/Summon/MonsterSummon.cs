using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { None, Fire } // 몬스터 타입 열거형

public class MonsterSummon : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // 몬스터 프리팹
    public Transform spawnPoint; // 소환 포인트

    // 소환 시간
    public float currentSpawnTime;
    public float maxSpawnTime;

    public bool activate; // 활성화 여부

    private MonsterFactory factory; // 몬스터 확인

    public MonsterType monsterType;

    void Start()
    {
        factory = new MonsterFactory(); // 팩토리 클래스 인스턴스 생성
    }

    void Update()
    {
        if (activate) // 활성화 시 일정 시간 마다 몬스터 생성
        {
            currentSpawnTime += Time.deltaTime;

            if (currentSpawnTime >= maxSpawnTime)
            {
                I_Monster monster = factory.CreateMonster(monsterType.ToString(), GetPrefabForType(monsterType), spawnPoint);
                monster?.Summon(); // 몬스터 소환
                currentSpawnTime = 0f; // 소환 시간 초기화
            }
        }
    }

    // 타입에 따라 프리팹 반환
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