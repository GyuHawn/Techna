using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory
{
    public I_Monster CreateMonster(string type, GameObject prefab, Transform spawnPoint)
    {
        switch (type)
        {
            case "Fire":
                return new Fire(prefab, spawnPoint);
            /*case "Gost":
               return new Gost(prefab, spawnPoint);*/
            default:
                return null;
        }
    }
}
