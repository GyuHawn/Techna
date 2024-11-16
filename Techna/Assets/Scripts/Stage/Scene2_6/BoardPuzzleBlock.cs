using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPuzzleBlock : MonoBehaviour
{
    public LEDNode lEDNode; // �ڽ��� ���⼱
    public LEDNode connectingNode; // ������ ���⼱
    public int blockValue; // �ڽ��� ��
    public bool check; // �ùٸ� ��ġ Ȯ��

    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���� üũ �� ���� ������ ��ġ������ ���⼱ ���� 
        if (collision.gameObject.CompareTag("BoardPuzzleCheck")) 
        {
            BoardPuzzleCheck boardCheck = collision.gameObject.GetComponent<BoardPuzzleCheck>();

            if (blockValue == boardCheck.checkValue)
            {
                check = true; // Ȯ�� �Ϸ�
                HoldPostion(collision.gameObject); // ��ġ ����

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

    void HoldPostion(GameObject block) // ��ġ ����
    {
        gameObject.transform.position = block.transform.position; // ��ġ ����
        gameObject.transform.rotation = block.transform.rotation; // ȸ���� ����
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }
}
