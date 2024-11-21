using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlaskPuzzle : MonoBehaviour
{
    public GameObject redFlask;
    private Transform redFlaskPosition;
    public GameObject greenFlask;
    private Transform greenFlaskPosition;
    public GameObject purpleFlask;
    private Transform purpleFlaskPosition;

    public int puzzleNum; // 최대 넣어야할 갯수
    public int redNum; // 빨강 플라스크 넣은 횟수
    public int greenNum;
    public int purpleNum;

    public GameObject flask; // 조합 물약

    private void Start()
    {
        redFlaskPosition = redFlask.transform;
        greenFlaskPosition = greenFlask.transform;
        purpleFlaskPosition = purpleFlask.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (puzzleNum < 4)
        {
            if (other.gameObject == redFlask)
            {
                redNum++;
                RedFlaskMove();
            }
            if (other.gameObject == greenFlask)
            {
                greenNum++;
                GreenFlaskMove();
            }
            if (other.gameObject == purpleFlask)
            {
                purpleNum++;
                PurpleFlaskMove();
            }
        }
        else
        {
            PuzzleClear();
        }
    }

    void RedFlaskMove()
    {
        redFlask.transform.position = redFlaskPosition.position;
    }
    void GreenFlaskMove()
    {
        greenFlask.transform.position = greenFlaskPosition.position;
    }
    void PurpleFlaskMove()
    {
        purpleFlask.transform.position = purpleFlaskPosition.position;
    }

    void PuzzleClear()
    {
        if(purpleNum >= 4)
        {
            if(redNum == 2 && greenNum == 1 && purpleNum == 1)
            {
                flask.SetActive(true);
                gameObject.GetComponent<FlaskPuzzle>().enabled = false;
            }
            else
            {
                puzzleNum = 0;
                redNum = 0;
                greenNum = 0;
                purpleNum = 0;
            }
        }
    }
}
