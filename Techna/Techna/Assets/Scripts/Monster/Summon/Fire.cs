using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : I_Monster
{
    private GameObject monsterPrefab; // ���� ������
    private Transform spawnPoint; // ��ȯ ��ġ

    public Fire(GameObject prefab, Transform point)
    {
        monsterPrefab = prefab;
        spawnPoint = point;
    }

    public void Summon() // ���� ��ȯ
    {
        GameObject monster = GameObject.Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
        monster.name = "FireMonster";
    }
}
