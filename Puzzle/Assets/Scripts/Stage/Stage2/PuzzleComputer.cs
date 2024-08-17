using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PuzzleComputer : MonoBehaviour
{
    public LineButton[] line_Button; // 이동 버튼(현재 값)
    public List<int> lineNum; // ( -1, 0, 1 ) 중심기준으로 설정

    public LEDNode lightLine;
    public bool activate; // 활성화 

    public LightningRod lightningRod; // 전원공급 장치
    public bool on; // 컴퓨터 전원 상태
    public GameObject barrier; // 차단막

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

        // 모든 값이 0이면 lightLine.activate를 true로 설정
        lightLine.activate = allZero;
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