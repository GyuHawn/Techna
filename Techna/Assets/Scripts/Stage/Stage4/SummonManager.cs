using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonManager : MonoBehaviour
{
    // ���� ������
    public GameObject creep_Basic;
    public GameObject creep_Crouch;
    public GameObject demon_Basic;
    public GameObject demon_Attack;
    public GameObject boss;

    // ���� ť
    public Queue<GameObject> creepQueue = new Queue<GameObject>();
    public Queue<GameObject> demonQueue = new Queue<GameObject>();
    private Queue<GameObject> bossQueue = new Queue<GameObject>();

    // �� ���� ��
    public int creep_Count = 59;
    public int d_BasicCount = 21;
    public int bossCount = 1;

    void Start()
    {
        SummonCreeps();
        SummonDemons();
    }

    // ���� ���� �� ť ����
    private void SummonCreeps()
    {
        for (int i = 0; i < creep_Count; i++)
        {
            // �ΰ��� ������ �� ����
            GameObject creep = Random.Range(0, 2) == 0 ? creep_Basic : creep_Crouch;
            GameObject obj = Instantiate(creep);
            obj.SetActive(false); // ��Ȱ��ȭ ���·� ����
            creepQueue.Enqueue(obj); // ť�� �߰�
        }
    }
    private void SummonDemons()
    {
        for (int i = 0; i < d_BasicCount; i++)
        {
            GameObject demon = Random.Range(0, 2) == 0 ? demon_Basic : demon_Attack;
            GameObject obj = Instantiate(demon);
            obj.SetActive(false); // ��Ȱ��ȭ ���·� ����
            demonQueue.Enqueue(obj); // ť�� �߰�
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
