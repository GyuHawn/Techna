using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineButton : MonoBehaviour
{
    public PuzzleComputer computer;

    public GameObject[] line_Blocks; // 움직일 블록
    public int curruntNum;
    public int line;

    private void OnCollisionEnter(Collision collision)
    {
        if (computer.on) // 전원이 켜있을때만
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                // 배열의 순서를 바꿉니다
                RotateArray(line_Blocks);

                // 블록의 위치 업데이트를 호출합니다
                computer.UpdateBlockPositions(line_Blocks);

                // 값 업데이트
                if (curruntNum == -1)
                {
                    curruntNum = 0;
                }
                else if (curruntNum == 0)
                {
                    curruntNum = 1;
                }
                else if (curruntNum == 1)
                {
                    curruntNum = -1;
                }

                computer.lineNum[line] = curruntNum;
            }
        }
    }
    void RotateArray(GameObject[] array)
    {
        if (array.Length < 3)
            return; // 배열 길이가 3보다 작은 경우 처리하지 않음

        GameObject temp = array[0]; // 배열의 첫 번째 요소를 저장합니다
        for (int i = 0; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1]; // 각 요소를 왼쪽으로 이동합니다
        }
        array[array.Length - 1] = temp; // 저장된 첫 번째 요소를 배열의 마지막 위치에 삽입합니다
    }

}
