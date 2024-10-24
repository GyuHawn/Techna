using UnityEngine;

public class GemManager : MonoBehaviour
{
    private GemUI gemUI;

    public GameObject player;

    [Header("ÃÑ¾Ë È¹µæ ¿©ºÎ")]
    public bool onLarge;

    [Header("¼Ó¼º È¹µæ ¿©ºÎ")]
    public bool onControl;
    public bool onElectricity;
    public bool onExpansion;
    public bool onGravity;

    [Header("±â´É È¹µæ ¿©ºÎ")]
    public bool onPenetrate;
    public bool onDestruction;
    public bool onDiffusion;
    public bool onUpgrade;
    public bool onQuick;


    private void Awake()
    {
        gemUI = FindObjectOfType<GemUI>();
    }

    public void CollectGem(string gemName) // º¸¼® È¹µæ
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
            case "Penetrate":
                onPenetrate = true;
                break;
            case "Destruction":
                onDestruction = true;
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
