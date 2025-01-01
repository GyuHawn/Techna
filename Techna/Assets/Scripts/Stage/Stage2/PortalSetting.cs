using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSetting : MonoBehaviour
{
    public GameObject stage4Portal;
    public GameObject stage5Portal;
    public GameObject stage6Portal;
    public GameObject stage7Portal;
    public GameObject stage8Portal;

    public bool stage4;
    public bool stage5;
    public bool stage6;
    public bool stage7;

    public bool clear;

    void Update()
    {
        NullObjectFind(); // 오브젝트 찾기
        PortalActivete(); // 포탈활성화

        // 클리어
        if (clear)
        {
            stage8Portal.SetActive(true);
        }   
    }

    void NullObjectFind() // 오브젝트 찾기
    {
        if (stage4Portal == null)
        {
            stage4Portal = GameObject.Find("Stage4Portal");
        }

        if (stage5Portal == null)
        {
            stage5Portal = GameObject.Find("Stage5Portal");
        }

        if (stage6Portal == null)
        {
            stage6Portal = GameObject.Find("Stage6Portal");
        }

        if (stage7Portal == null)
        {
            stage7Portal = GameObject.Find("Stage7Portal");
        }

        if (stage8Portal == null)
        {
            stage8Portal = GameObject.Find("Stage8Portal");
        }
    }

    // 포탈 활성화
    void PortalActivete()
    {
        if (stage4 && !stage4Portal.activeInHierarchy)
        {
            stage4Portal.SetActive(true);
        }

        if (stage5 && !stage5Portal.activeInHierarchy)
        {
            stage5Portal.SetActive(true);
        }
        
        if (stage6 && !stage6Portal.activeInHierarchy)
        {
            stage6Portal.SetActive(true);
        }
        
        if (stage7 && !stage7Portal.activeInHierarchy)
        {
            stage7Portal.SetActive(true);
        }
    }
}
