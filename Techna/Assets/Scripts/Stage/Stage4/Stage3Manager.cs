using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    public SummonManager summonManager;

    public Transform[] creepSummonPositions;
    public Transform[] demonSummonPositions;

    public int wave;
    public int creepCount;
    public int demonCount;

    private void Start()
    {
        wave = 0;
    }

    public void SummonCount()
    {
        switch (wave)
        {
            case 1:
                creepCount = 10;
                break;
            case 2:
                creepCount = 12;
                demonCount = 3;
                break;
            case 3:
                creepCount = 15;
                demonCount = 5;
                break;
            case 4:
                creepCount = 15;
                demonCount = 10;
                break;
            case 5:
                creepCount = 7;
                demonCount = 3;
                break;
        }
    }
}
