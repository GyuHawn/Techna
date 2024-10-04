using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : I_Monster
{
    private GameObject monsterPrefab; // 몬스터 프리팹
    private Transform spawnPoint; // 소환 위치

    public Fire(GameObject prefab, Transform point)
    {
        monsterPrefab = prefab;
        spawnPoint = point;
    }

    public void Summon() // 몬스터 소환
    {
        GameObject monster = GameObject.Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
        monster.name = "FireMonster";
    }
}
