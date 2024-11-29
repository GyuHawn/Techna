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
            HandleFlaskCollision(other);
        }
        else
        {
            ResetPuzzleState();
        }
    }

    private void HandleFlaskCollision(Collider other)
    {
        if (puzzleNum < 4)
        {
            if (other.gameObject == redFlask || other.gameObject == greenFlask || other.gameObject == purpleFlask)
            {
                cauldronm.flaskStatusUI.gameObject.SetActive(true);

                // 조합법이 맞을때
                if (other.gameObject == redFlask) { redNum++; }
                else if (other.gameObject == greenFlask) { greenNum++; }
                else if (other.gameObject == purpleFlask) { purpleNum++; }

                puzzleNum++;
                MoveFlaskBack(other.gameObject);
            }
            else if (other.gameObject == cauldronm.fireWood) { }
            else
            {
                cauldronm.MixtureFailed();
                ResetPuzzleState();
            }

            if (puzzleNum >= 4)
            {
                PuzzleState();
                ResetPuzzleState();
            }
        }
    }

    private void MoveFlaskBack(GameObject flask)
    {
        Rigidbody rb = flask.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (flask == redFlask) { redFlask.transform.position = redFlaskPosition; }
        else if (flask == greenFlask) { greenFlask.transform.position = greenFlaskPosition; }
        else if (flask == purpleFlask) { purpleFlask.transform.position = purpleFlaskPosition; }
    }

    // 퍼즐 상태 확인 및 초기화
    void PuzzleState()
    {
        if (redNum == 2 && greenNum == 1 && purpleNum == 1)
        {
            flask.SetActive(true);
            gameObject.GetComponent<FlaskPuzzle>().enabled = false;
        }
        else
        {
            cauldronm.MixtureFailed();
            ResetPuzzleState();
        }
    }

    private void ResetPuzzleState()
    {
        cauldronm.flaskStatusUI.gameObject.SetActive(false);

        puzzleNum = 0;
        redNum = 0;
        greenNum = 0;
        purpleNum = 0;

        MoveFlaskBack(redFlask);
        MoveFlaskBack(greenFlask);
        MoveFlaskBack(purpleFlask);
    }   
}