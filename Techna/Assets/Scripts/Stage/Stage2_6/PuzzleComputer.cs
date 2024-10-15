using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PuzzleComputer : MonoBehaviour
{
    [Header("제어 버튼")]
    public LineButton[] line_Button; // 이동 버튼(현재 값)

    [Header("각 라인 값")]
    public List<int> lineNum; // ( -1, 0, 1 ) 중심기준으로 설정
    private bool allZero; // 모든 값이 0인지 여부

    [Header("전선")]
    public LEDNode computerLightLine; // 컴퓨터 전기선
    public LEDNode activateLightLine; // 활성화 전기선
    public bool activate; // 활성화 

    [Header("전원 관련")]
    public LightningRod lightningRod; // 전원공급 장치
    public bool on; // 컴퓨터 전원 상태
    public GameObject barrier; // 차단막

    [Header("활성화시 제어할 오브젝트")]
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
        allZero = false; // 처음엔 false로 설정
    }

    private void Update()
    {
        // lightningRod.activate가 참이 되면 on을 true로 설정
        if (lightningRod.activate)
        {
            on = true;
        }

        // on이 true일 때, 즉 컴퓨터가 활성화된 경우에만 실행
        if (on && !allZero)
        {
            barrier.SetActive(false); // 차단막 비활성화
            OnLight(); // 전기선 및 오브젝트 상태 업데이트
        }
    }

    void OnLight()
    {
        // 모든 오브젝트 상태 확인 (전부 0인지)
        allZero = lineNum.TrueForAll(num => num == 0);

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