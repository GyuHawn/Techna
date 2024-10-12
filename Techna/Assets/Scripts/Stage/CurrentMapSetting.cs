using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMapSetting : MonoBehaviour
{
    public CurrentMapManager currentMapManager;

    public int changeMap; // 변경된 맵 (각각 설정)
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentMapManager.currentMap = changeMap;

            currentMapManager.SetMapActive();
        }
    }
}
