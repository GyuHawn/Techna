using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineButton : MonoBehaviour
{
    public PuzzleComputer computer;

    [Header("움직일 오브젝트")]
    public GameObject[] line_Blocks; // 움직일 블록

    [Header("현재값")]
    public int curruntNum;
    public int line;

    [Header("확인할 총알")]
    public string[] collisionBullet = new string[] { "Bullet", "Expansion", "Penetrate" };

    private void OnTriggerEnter(Collider other)
    {
        if (System.Array.Exists(collisionBullet, tag => tag == other.gameObject.tag)) // 전원이 켜져 있고 Bullet일 때
        {
            Debug.Log("1");
            // 배열 순서 변경 
            if (line_Blocks.Length >= 3)
                RotateArray(line_Blocks);

            // 블록 위치 업데이트
            computer.UpdateBlockPositions(line_Blocks);

            // 값 업데이트
            curruntNum = (curruntNum + 2) % 3 - 1;

            // 컴퓨터 lineNum 값 갱신
            computer.lineNum[line] = curruntNum;
        }
    }

    void RotateArray(GameObject[] array)
    {
        if (array.Length < 3)
            return; // 배열 3미만일때 무시 (이후 변경 예정)

        GameObject temp = array[0]; // 배열 첫 번째 요소 저장
        for (int i = 0; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1]; // 각 요소를 왼쪽으로 이동
        }
        array[array.Length - 1] = temp; // 저장된 첫 번째 요소를 배열의 마지막 위치에 삽입

    }
}
