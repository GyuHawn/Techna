using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FlaskPuzzle : MonoBehaviour
{
    public Cauldronm cauldronm;

    public GameObject redFlask;
    public Vector3 redFlaskPosition;
    public GameObject greenFlask;
    private Vector3 greenFlaskPosition;
    public GameObject purpleFlask;
    private Vector3 purpleFlaskPosition;

    public int puzzleNum; // 최대 넣어야할 갯수
    public int redNum; // 빨강 플라스크 넣은 횟수
    public int greenNum;
    public int purpleNum;

    public GameObject flask; // 조합 물약

    public TMP_Text failedUI; // 조합실패 UI
    public bool showUI;

    private void Awake()
    {
        cauldronm = GetComponent<Cauldronm>();
    }

    private void Start()
    {
        redFlaskPosition = new Vector3(redFlask.transform.position.x, redFlask.transform.position.y + 0.1f, redFlask.transform.position.z);
        greenFlaskPosition = new Vector3(greenFlask.transform.position.x, greenFlask.transform.position.y + 0.1f, greenFlask.transform.position.z);
        purpleFlaskPosition = new Vector3(purpleFlask.transform.position.x, purpleFlask.transform.position.y + 0.1f, purpleFlask.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cauldronm.active)
        {
            if (puzzleNum < 4)
            {
                if (other.gameObject == redFlask)
                {
                    redNum++;
                    puzzleNum++;
                    RedFlaskMove();
                }
                if (other.gameObject == greenFlask)
                {
                    greenNum++;
                    puzzleNum++;
                    GreenFlaskMove();
                }
                if (other.gameObject == purpleFlask)
                {
                    purpleNum++;
                    puzzleNum++;
                    PurpleFlaskMove();
                }
            }

            if(puzzleNum >= 4)
            {
                PuzzleClear();
            }
        }
        else
        {
            cauldronm.Failed();

            if (other.gameObject == redFlask)
            {
                RedFlaskMove();
            }
            if (other.gameObject == greenFlask)
            {
                GreenFlaskMove();
            }
            if (other.gameObject == purpleFlask)
            {
                PurpleFlaskMove();
            }
        }
    }

    // 플라스크 위치 초기화
    void RedFlaskMove()
    {
        Rigidbody flask = redFlask.GetComponent<Rigidbody>();
        // 움직임 정지
        flask.velocity = Vector3.zero;
        flask.angularVelocity = Vector3.zero;

        redFlask.transform.position = redFlaskPosition;
    }
    void GreenFlaskMove()
    {
        Rigidbody flask = greenFlask.GetComponent<Rigidbody>();
        flask.velocity = Vector3.zero;
        flask.angularVelocity = Vector3.zero;

        greenFlask.transform.position = greenFlaskPosition;
    }
    void PurpleFlaskMove()
    {
        Rigidbody flask = purpleFlask.GetComponent<Rigidbody>();
        flask.velocity = Vector3.zero;
        flask.angularVelocity = Vector3.zero;

        purpleFlask.transform.position = purpleFlaskPosition;
    }

    // 퍼즐 클리어
    void PuzzleClear()
    {
        if (redNum == 2 && greenNum == 1 && purpleNum == 1)
        {
            flask.SetActive(true);
            gameObject.GetComponent<FlaskPuzzle>().enabled = false;
        }
        else
        {
            Failed();

            puzzleNum = 0;
            redNum = 0;
            greenNum = 0;
            purpleNum = 0;
        }
    }

    public void Failed()
    {
        if (!showUI)
        {
            showUI = true;
            StartCoroutine(ShowFailedUI());
        }
    }

    IEnumerator ShowFailedUI()
    {
        failedUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        failedUI.gameObject.SetActive(false);
        showUI = false;
    }
}
