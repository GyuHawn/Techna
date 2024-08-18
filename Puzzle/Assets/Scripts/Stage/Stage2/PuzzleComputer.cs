using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PuzzleComputer : MonoBehaviour
{
    public LineButton[] line_Button; // 이동 버튼(현재 값)
    public List<int> lineNum; // ( -1, 0, 1 ) 중심기준으로 설정

    public LEDNode computerLightLine; // 컴퓨터 전기선
    public LEDNode activateLightLine; // 활성화 전기선
    public bool activate; // 활성화 

    public LightningRod lightningRod; // 전원공급 장치
    public bool on; // 컴퓨터 전원 상태
    public GameObject barrier; // 차단막

    public RotateObject[] gears; // 회전 기어
    public GameObject jumpPad; // 점프패드
    public RotateObject[] RotateFloors; // 클리어 발판

    void Start()
    {
        for (int i = 0; i <= 6; i++)
        {
            lineNum.Add(line_Button[i].curruntNum);
        }     
    }

    private void Update()
    {
        if (on)
        {
            on = false;
            barrier.SetActive(false);
            OnLight();
        }
        else
        {
            if (lightningRod.activate)
            {
                on = true;
            }
        }
    }

    void OnLight()
    {
        // lineNum의 모든 값이 0인지 확인
        bool allZero = true;
        foreach (int num in lineNum)
        {
            if (num != 0)
            {
                allZero = false;
                break;
            }
        }

        // 모든 값이 0이면 활성화 
        computerLightLine.activate = allZero; // 컴퓨터 전기선 활성화
        activateLightLine.activate = allZero; // 활성화 전기선 활성화
        ActivateGear(allZero); // 모든 기어 활성화
        RotateFloor(allZero); // 클리어 발판 회전
        jumpPad.SetActive(allZero); // 점프패드 활성화
    }

    void ActivateGear(bool on)
    {
        if (on)
        {
            foreach (var gear in gears)
            {
                gear.activate = true;
            }
        }
    }
    
    void RotateFloor(bool on)
    {
        if (on)
        {
            foreach (var flooor in RotateFloors)
            {
                flooor.activate = true;
            }
        }
    }

    // 블록의 위치를 업데이트
    public void UpdateBlockPositions(GameObject[] blocks)
    {
        if (blocks != null)
        {
            blocks[0].transform.position += new Vector3(0, 1.7f, 0);
            blocks[1].transform.position += new Vector3(0, 1.7f, 0);
            blocks[2].transform.position += new Vector3(0, -3.4f, 0);
        }
    }
}