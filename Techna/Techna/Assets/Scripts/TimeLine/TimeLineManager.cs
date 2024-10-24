using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimeLineManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public SuitManager suitManager;

    public PlayableDirector pd;
    public GameObject canvasCamera;

    private void Start()
    {
        StartCinemachine();

        if (pd != null)
        {
            pd.stopped += OnTimelineStopped; // Ÿ�Ӷ��� ���� �̺�Ʈ �߰�
        }
    }

    // Ÿ�Ӷ��� ���� ��
    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == pd)
        {
            suitManager.progress = true; // ������ ���� ����
            playerMovement.moving = true; // �̵� ���� ���·� ����
            canvasCamera.SetActive(true); // ĵ���� ī�޶� Ȱ��ȭ
        }
    }

    void StartCinemachine()
    {
        playerMovement.moving = false;
        canvasCamera.SetActive(false);
    }

}
