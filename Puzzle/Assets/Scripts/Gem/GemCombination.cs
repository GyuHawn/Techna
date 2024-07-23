using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UI;

public class GemCombination : MonoBehaviour
{
    public int selectBulletNum;
    public int selectAttributeNum;
    public int selectFunctionNum;
    public GameObject[] B_Gems; // ÃÑ¾Ë
    public GameObject[] B_A_Gems; // ÃÑ¾Ë + ¼Ó¼º
    public GameObject[] B_F_Gems; // ÃÑ¾Ë + ±â´É
    public GameObject[] B_A_F_Gems; // ÃÑ¾Ë + ¼Ó¼º + ±â´É

    public Sprite[] crossHair;

    void Update()
    {
        SelectGem(selectBulletNum, selectAttributeNum, selectFunctionNum);
    }

    void ResetGem()
    {
        foreach(var gem in B_Gems)
        {
            gem.SetActive(false);
        }
        foreach (var gem in B_A_Gems)
        {
            gem.SetActive(false);
        }
        foreach (var gem in B_F_Gems)
        {
            gem.SetActive(false);
        }
        foreach (var gem in B_A_F_Gems)
        {
            gem.SetActive(false);
        }
    }

    void SelectGem(int bullet, int attribute, int function)
    {
        ResetGem();
        switch (bullet, attribute, function)
        {
            // ±âº»Åº
            case (1, 0, 0):
                B_Gems[0].SetActive(true);
                break;
            case (1, 1, 0):
                B_A_Gems[0].SetActive(true);
                break;
            case (1, 2, 0):
                B_A_Gems[1].SetActive(true);
                break;
            case (1, 3, 0):
                B_A_Gems[2].SetActive(true);
                break;
            case (1, 4, 0):
                B_A_Gems[3].SetActive(true);
                break;
            case (1, 0, 1):
                B_F_Gems[0].SetActive(true);
                break;
            case (1, 0, 2):
                B_F_Gems[1].SetActive(true);
                break;
            case (1, 0, 3):
                B_F_Gems[2].SetActive(true);
                break;
            case (1, 0, 4):
                B_F_Gems[3].SetActive(true);
                break;
            case (1, 0, 5):
                B_F_Gems[4].SetActive(true);
                break;
            case (1, 1, 1):
                B_A_F_Gems[0].SetActive(true);
                break;
            case (1, 1, 2):
                B_A_F_Gems[1].SetActive(true);
                break;
            case (1, 1, 3):
                B_A_F_Gems[2].SetActive(true);
                break;
            case (1, 1, 4):
                B_A_F_Gems[3].SetActive(true);
                break;
            case (1, 1, 5):
                B_A_F_Gems[4].SetActive(true);
                break;
            case (1, 2, 1):
                B_A_F_Gems[5].SetActive(true);
                break;
            case (1, 2, 2):
                B_A_F_Gems[6].SetActive(true);
                break;
            case (1, 2, 3):
                B_A_F_Gems[7].SetActive(true);
                break;
            case (1, 2, 4):
                B_A_F_Gems[8].SetActive(true);
                break;
            case (1, 2, 5):
                B_A_F_Gems[9].SetActive(true);
                break;
            case (1, 3, 1):
                B_A_F_Gems[10].SetActive(true);
                break;
            case (1, 3, 2):
                B_A_F_Gems[11].SetActive(true);
                break;
            case (1, 3, 3):
                B_A_F_Gems[12].SetActive(true);
                break;
            case (1, 3, 4):
                B_A_F_Gems[13].SetActive(true);
                break;
            case (1, 3, 5):
                B_A_F_Gems[14].SetActive(true);
                break;
            case (1, 4, 1):
                B_A_F_Gems[15].SetActive(true);
                break;
            case (1, 4, 2):
                B_A_F_Gems[16].SetActive(true);
                break;
            case (1, 4, 3):
                B_A_F_Gems[17].SetActive(true);
                break;
            case (1, 4, 4):
                B_A_F_Gems[18].SetActive(true);
                break;
            case (1, 4, 5):
                B_A_F_Gems[19].SetActive(true);
                break;

            // ´ëÇüÅº
            case (2, 0, 0):
                B_Gems[1].SetActive(true);
                break;
            case (2, 1, 0):
                B_A_Gems[4].SetActive(true);
                break;
            case (2, 2, 0):
                B_A_Gems[5].SetActive(true);
                break;
            case (2, 3, 0):
                B_A_Gems[6].SetActive(true);
                break;
            case (2, 4, 0):
                B_A_Gems[7].SetActive(true);
                break;
            case (2, 0, 1):
                B_F_Gems[5].SetActive(true);
                break;
            case (2, 0, 2):
                B_F_Gems[6].SetActive(true);
                break;
            case (2, 0, 3):
                B_F_Gems[7].SetActive(true);
                break;
            case (2, 0, 4):
                B_F_Gems[8].SetActive(true);
                break;
            case (2, 0, 5):
                B_F_Gems[9].SetActive(true);
                break;
            case (2, 1, 1):
                B_A_F_Gems[20].SetActive(true);
                break;
            case (2, 1, 2):
                B_A_F_Gems[21].SetActive(true);
                break;
            case (2, 1, 3):
                B_A_F_Gems[22].SetActive(true);
                break;
            case (2, 1, 4):
                B_A_F_Gems[23].SetActive(true);
                break;
            case (2, 1, 5):
                B_A_F_Gems[24].SetActive(true);
                break;
            case (2, 2, 1):
                B_A_F_Gems[25].SetActive(true);
                break;
            case (2, 2, 2):
                B_A_F_Gems[26].SetActive(true);
                break;
            case (2, 2, 3):
                B_A_F_Gems[27].SetActive(true);
                break;
            case (2, 2, 4):
                B_A_F_Gems[28].SetActive(true);
                break;
            case (2, 2, 5):
                B_A_F_Gems[29].SetActive(true);
                break;
            case (2, 3, 1):
                B_A_F_Gems[30].SetActive(true);
                break;
            case (2, 3, 2):
                B_A_F_Gems[31].SetActive(true);
                break;
            case (2, 3, 3):
                B_A_F_Gems[32].SetActive(true);
                break;
            case (2, 3, 4):
                B_A_F_Gems[33].SetActive(true);
                break;
            case (2, 3, 5):
                B_A_F_Gems[34].SetActive(true);
                break;
            case (2, 4, 1):
                B_A_F_Gems[35].SetActive(true);
                break;
            case (2, 4, 2):
                B_A_F_Gems[36].SetActive(true);
                break;
            case (2, 4, 3):
                B_A_F_Gems[37].SetActive(true);
                break;
            case (2, 4, 4):
                B_A_F_Gems[38].SetActive(true);
                break;
            case (2, 4, 5):
                B_A_F_Gems[39].SetActive(true);
                break;
        }
    }
}
