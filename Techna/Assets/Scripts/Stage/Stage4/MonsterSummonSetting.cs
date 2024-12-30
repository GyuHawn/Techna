using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSummonSetting : MonoBehaviour
{
    // ���� ������
    public GameObject creepBasic;
    public GameObject creepCrouch;
    public GameObject demonBasic;
    public GameObject demonAttack;
    public GameObject boss;

    // ���� ť
    public Queue<GameObject> creepQueue = new Queue<GameObject>();
    public Queue<GameObject> demonQueue = new Queue<GameObject>();
    private Queue<GameObject> bossQueue = new Queue<GameObject>();

    // �� ���� ��
    public int creepCount = 59;
    public int demonCount = 21;
    public int bossCount = 1;

    private void Start()
    {
        SummonMonsters(creepCount, creepQueue, new[] { creepBasic, creepCrouch }, "Creep");
        SummonMonsters(demonCount, demonQueue, new[] { demonBasic, demonAttack }, "Demon");
    }

    // ���� ���� �� ť ����
    private void SummonMonsters(int count, Queue<GameObject> queue, GameObject[] prefabs, string name)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            GameObject obj = Instantiate(prefab);
            obj.name = name; // ��� ���Ϳ� ���� �̸� �ο�
            obj.SetActive(false); // ��Ȱ��ȭ ���·� ����
            queue.Enqueue(obj); // ť�� �߰�
        }
    }

    // ���� ��ȯ
    public GameObject MonstetSummon(Queue<GameObject> pool)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true); // Ȱ��ȭ
            return obj;
        }
        return null; // Ǯ�� ������Ʈ�� ���� ���
    }
}
