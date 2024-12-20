using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonManager : MonoBehaviour
{
    // 몬스터 프리팹
    public GameObject creep_Basic;
    public GameObject creep_Crouch;
    public GameObject demon_Basic;
    public GameObject demon_Attack;
    public GameObject boss;

    // 몬스터 큐
    public Queue<GameObject> creepQueue = new Queue<GameObject>();
    public Queue<GameObject> demonQueue = new Queue<GameObject>();
    private Queue<GameObject> bossQueue = new Queue<GameObject>();

    // 총 몬스터 수
    public int creep_Count = 59;
    public int d_BasicCount = 21;
    public int bossCount = 1;

    void Start()
    {
        SummonCreeps();
        SummonDemons();
    }

    // 몬스터 생성 및 큐 저장
    private void SummonCreeps()
    {
        for (int i = 0; i < creep_Count; i++)
        {
            // 두개의 프리팹 중 선택
            GameObject creep = Random.Range(0, 2) == 0 ? creep_Basic : creep_Crouch;
            GameObject obj = Instantiate(creep);
            obj.SetActive(false); // 비활성화 상태로 생성
            creepQueue.Enqueue(obj); // 큐에 추가
        }
    }
    private void SummonDemons()
    {
        for (int i = 0; i < d_BasicCount; i++)
        {
            GameObject demon = Random.Range(0, 2) == 0 ? demon_Basic : demon_Attack;
            GameObject obj = Instantiate(demon);
            obj.SetActive(false); // 비활성화 상태로 생성
            demonQueue.Enqueue(obj); // 큐에 추가
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
