using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged; // 씬 변경 시 이벤트 등록
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged; // 씬 변경 시 이벤트 해제
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        if (next.name == "Stage2")
        {
            NullObjectFind(); // 오브젝트 찾기

            // 클리어
            if (clear)
            {
                stage8Portal.transform.position = new Vector3(0, 0, 0);
            }
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

        PortalActivete(); // 포탈 활성화
    }

    // 포탈 활성화
    void PortalActivete()
    {
        if (stage4)
        {
            stage4Portal.transform.localPosition = new Vector3(0, 1.3f, 0); // 로컬 좌표로 이동
        }

        if (stage5)
        {
            stage5Portal.transform.localPosition = new Vector3(0, 1.3f, 0); // 로컬 좌표로 이동
        }

        if (stage6)
        {
            stage6Portal.transform.localPosition = new Vector3(0, 1.3f, 0); // 로컬 좌표로 이동
        }

        if (stage7)
        {
            stage7Portal.transform.localPosition = new Vector3(0, 1.3f, 0); // 로컬 좌표로 이동
        }
    }
}
