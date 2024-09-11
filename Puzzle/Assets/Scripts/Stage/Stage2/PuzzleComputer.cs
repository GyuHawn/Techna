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
        // 리스트 초기화 및 리스트 값 추가
        lineNum = new List<int>(line_Button.Length);
        foreach (var button in line_Button)
        {
            lineNum.Add(button.curruntNum);
        }
    }

    private void Update()
    {
        if (on) 
        {
            on = false;
            barrier.SetActive(false); // 차단막 비활성 화
            OnLight(); // 전기선 및 오브젝트 상태 업데이트 
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
        // 모든 오브젝트 상태 확인 (전부 0인지)
        bool allZero = lineNum.TrueForAll(num => num == 0);

        // 전기선 상태 업데이트
        computerLightLine.activate = allZero;
        activateLightLine.activate = allZero;

        // 관련 오브젝트 활성화 및 상태 업데이트
        ActivateObjects(gears, allZero);
        ActivateObjects(RotateFloors, allZero);
        jumpPad.SetActive(allZero);
    }

    void ActivateObjects(RotateObject[] objects, bool activate) // 오브젝트 상태 활성화
    {
        foreach (var obj in objects)
        {
            obj.activate = activate;
        }
    }

    public void UpdateBlockPositions(GameObject[] blocks) // 블록 위치 업데이트
    {
        if (blocks != null && blocks.Length >= 3)
        {
            Vector3[] positions = { new Vector3(0, 1.7f, 0), new Vector3(0, 1.7f, 0), new Vector3(0, -3.4f, 0) };
            for (int i = 0; i < blocks.Length && i < positions.Length; i++)
            {
                blocks[i].transform.position += positions[i];
            }
        }
    }
}