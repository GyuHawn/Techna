using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPuzzleBlock : MonoBehaviour
{
    public LEDNode lEDNode; // 자신의 전기선
    public LEDNode connectingNode; // 연결할 전기선
    public int blockValue; // 자신의 값
    public bool check; // 올바른 위치 확인

    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 퍼즐 체크 시 값이 같을때 위치고정후 전기선 연결 
        if (collision.gameObject.CompareTag("BoardPuzzleCheck")) 
        {
            BoardPuzzleCheck boardCheck = collision.gameObject.GetComponent<BoardPuzzleCheck>();

            if (blockValue == boardCheck.checkValue)
            {
                check = true; // 확인 완료
                HoldPostion(collision.gameObject); // 위치 고정

                if (lEDNode.prevNode == null)
                {
                    lEDNode.prevNode = connectingNode;
                }
            }
            else
            {
                lEDNode.prevNode = null;
            }
        }
    }

    void HoldPostion(GameObject block) // 위치 고정
    {
        gameObject.transform.position = block.transform.position; // 위치 설정
        gameObject.transform.rotation = block.transform.rotation; // 회전값 설정
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }
}
