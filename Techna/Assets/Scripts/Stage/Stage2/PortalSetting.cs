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
        SceneManager.activeSceneChanged += OnSceneChanged; // �� ���� �� �̺�Ʈ ���
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged; // �� ���� �� �̺�Ʈ ����
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        if (next.name == "Stage2")
        {
            NullObjectFind(); // ������Ʈ ã��

            // Ŭ����
            if (clear)
            {
                stage8Portal.transform.position = new Vector3(0, 0, 0);
            }
        }
    }

    void NullObjectFind() // ������Ʈ ã��
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

        PortalActivete(); // ��Ż Ȱ��ȭ
    }

    // ��Ż Ȱ��ȭ
    void PortalActivete()
    {
        if (stage4)
        {
            stage4Portal.transform.localPosition = new Vector3(0, 1.3f, 0); // ���� ��ǥ�� �̵�
        }

        if (stage5)
        {
            stage5Portal.transform.localPosition = new Vector3(0, 1.3f, 0); // ���� ��ǥ�� �̵�
        }

        if (stage6)
        {
            stage6Portal.transform.localPosition = new Vector3(0, 1.3f, 0); // ���� ��ǥ�� �̵�
        }

        if (stage7)
        {
            stage7Portal.transform.localPosition = new Vector3(0, 1.3f, 0); // ���� ��ǥ�� �̵�
        }
    }
}
