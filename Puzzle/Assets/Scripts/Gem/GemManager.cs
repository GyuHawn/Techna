using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    private GemUI gemUI;

    public GameObject player;

    // ÃÑ¾Ë È¹µæ ¿©ºÎ
    public bool onLarge;

    // ¼Ó¼® È¹µæ ¿©ºÎ
    public bool onControl;
    public bool onElectricity;
    public bool onExpansion;
    public bool onGravity;

    // ±â´É È¹µæ ¿©ºÎ
    public bool onDestruction;
    public bool onPenetrate;
    public bool onDiffusion;
    public bool onUpgrade;
    public bool onQuick;

    private void Awake()
    {
        if (!gemUI)
            gemUI = FindObjectOfType<GemUI>();
    }

    public void CollectGem(string gemName)
    {
        switch (gemName)
        {
            case "Large":
                onLarge = true;
                break;
            case "Control":
                onControl = true;
                break;
            case "Electricity":
                onElectricity = true;
                break;
            case "Expansion":
                onExpansion = true;           
                break;
            case "Gravity":
                onGravity = true;
                break;
            case "Destruction":
                onDestruction = true;
                break;
            case "Penetrate":
                onPenetrate = true;
                break;
            case "Diffusion":
                onDiffusion = true;
                break;
            case "Upgrade":
                onUpgrade = true;
                break;
            case "Quick":
                onQuick = true;
                break;
        }
        gemUI.ActivateGemUI();
    }
}
