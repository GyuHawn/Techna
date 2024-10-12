using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMapManager : MonoBehaviour
{
    public int currentMap; // ���� ��
    public GameObject[] maps; // ��ü ��

    public void SetMapActive()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            // ��Ȱ��ȭ�� ������ ����
            if (i < currentMap - 1 || i > currentMap + 2)
            {
                maps[i].SetActive(false);
            }
            else
            {
                maps[i].SetActive(true);
            }
        }
    }
}
