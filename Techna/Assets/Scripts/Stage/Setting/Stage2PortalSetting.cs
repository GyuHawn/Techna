 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2PortalSetting : MonoBehaviour
{
    public PortalSetting portalSetting;

    void Awake()
    {
        portalSetting = GameObject.Find("Stage2Manager").GetComponentInParent<PortalSetting>();
    }

    // �� Ŭ������ ���ƿý� ���� ��Ż Ȱ��ȭ
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Scene scene = SceneManager.GetActiveScene();
            switch(scene.name) {
                case "Stage3":
                    portalSetting.stage4 = true;
                    break;
                case "Stage4":
                    portalSetting.stage5 = true;
                    break;
                case "Stage5":
                    portalSetting.stage6 = true;
                    break;
                case "Stage6":
                    portalSetting.stage7 = true;
                    break;
            }
        }
    }
}
