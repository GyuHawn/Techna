using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSummonSetting : MonoBehaviour
{
    // 몬스터 프리팹
    public GameObject creepBasic;
    public GameObject creepCrouch;
    public GameObject demonBasic;
    public GameObject demonAttack;
    public GameObject boss;

    // 몬스터 큐
    public Queue<GameObject> creepQueue = new Queue<GameObject>();
    public Queue<GameObject> demonQueue = new Queue<GameObject>();
    private Queue<GameObject> bossQueue = new Queue<GameObject>();

    // 총 몬스터 수
    public int creepCount = 59;
    public int demonCount = 21;
    public int bossCount = 1;

    private void Start()
    {
        SummonMonsters(creepCount, creepQueue, new[] { creepBasic, creepCrouch }, "Creep");
        SummonMonsters(demonCount, demonQueue, new[] { demonBasic, demonAttack }, "Demon");
    }

    // 몬스터 생성 및 큐 저장
    private void SummonMonsters(int count, Queue<GameObject> queue, GameObject[] prefabs, string name)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            GameObject obj = Instantiate(prefab);
            obj.name = name; // 모든 몬스터에 같은 이름 부여
            obj.SetActive(false); // 비활성화 상태로 생성
            queue.Enqueue(obj); // 큐에 추가
        }
    }

    // 몬스터 소환
    public GameObject MonstetSummon(Queue<GameObject> pool)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true); // 활성화
            return obj;
        }
        return null; // 풀에 오브젝트가 없을 경우
    }
}
