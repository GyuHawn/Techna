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

    public int puzzleNum; // �ִ� �־���� ����
    public int redNum; // ���� �ö�ũ ���� Ƚ��
    public int greenNum;
    public int purpleNum;

    public GameObject flask; // ���� ����

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
