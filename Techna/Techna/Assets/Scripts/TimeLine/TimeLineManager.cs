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
            pd.stopped += OnTimelineStopped; // 타임라인 종료 이벤트 추가
        }
    }

    // 타임라인 종료 시
    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == pd)
        {
            suitManager.progress = true; // 게이지 진행 시작
            playerMovement.moving = true; // 이동 가능 상태로 변경
            canvasCamera.SetActive(true); // 캔버스 카메라 활성화
        }
    }

    void StartCinemachine()
    {
        playerMovement.moving = false;
        canvasCamera.SetActive(false);
    }

}
