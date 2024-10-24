using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossStage : MonoBehaviour
{
    public Transform[] bossMapPositions; // ������ ����Ʈ
    public GameObject[] SkillWalls; // ���� ��ų ��
    public Transform[] SkillPositions; // ������ ť�� ���� ����Ʈ
    public GameObject clearWall; // Ŭ���� �� ������������ �̵� ���� ��
    public GameObject destroyFloor; // ���� �ٴ�
    public GameObject gem; // Ŭ���� ����

    private void Update()
    {
        if(gem == null && destroyFloor != null)
        {
            StageClear();
        }
    }

    public void StageClear()
    {
        Destroy(destroyFloor);
    }
}
