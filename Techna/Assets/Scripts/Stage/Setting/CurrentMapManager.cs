using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMapManager : MonoBehaviour
{
    public int currentMap; // 현재 맵
    public GameObject[] maps; // 전체 맵

    public void SetMapActive() // 위치에 따른 다른 맵 비활성화
    {
        for (int i = 0; i < maps.Length; i++)
        {
            // 비활성화할 범위를 설정
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
