using UnityEngine;

public class GemManager : MonoBehaviour
{
    private GemUI gemUI;

    public GameObject player;

    [Header("�Ѿ� ȹ�� ����")]
    public bool onLarge;

    [Header("�Ӽ� ȹ�� ����")]
    public bool onControl;
    public bool onElectricity;
    public bool onExpansion;
    public bool onGravity;

    [Header("��� ȹ�� ����")]
    public bool onPenetrate;
    public bool onDestruction;
    public bool onDiffusion;
    public bool onUpgrade;
    public bool onQuick;


    private void Awake()
    {
        gemUI = FindObjectOfType<GemUI>();
    }

    public void CollectGem(string gemName) // ���� ȹ��
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
