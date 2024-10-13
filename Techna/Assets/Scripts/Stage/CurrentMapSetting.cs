using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMapSetting : MonoBehaviour
{
    public CurrentMapManager currentMapManager;

    public int changeMap; // ����� �� (��ġ���� ���� ����)
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentMapManager.currentMap = changeMap;

            currentMapManager.SetMapActive(); // ��ġ�� ���� �ٸ� �� ��Ȱ��ȭ
        }
    }
}
