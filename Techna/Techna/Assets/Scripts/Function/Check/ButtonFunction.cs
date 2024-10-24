using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunction : MonoBehaviour
{
    public GameObject[] movingObj; // �̵��ϴ� ������Ʈ
    public GameObject[] rotateObj; // ȸ���ϴ� ������Ʈ

    public bool activate; // Ȱ��ȭ

    [Header("Ȯ���� �Ѿ�")]
    public string[] collisionBullet = new string[] { "Bullet", "Expansion", "Penetrate" };

    private void Update()
    {
        if (activate) // Ȱ��ȭ �� �۵�
        {
            OnActivate();
        }
    }

    void OnActivate() // �̵� or ȸ��
    {
        if (movingObj != null)
        {
            for(int i = 0; i < movingObj.Length; i++)
            {
                MovingObject obj = movingObj[i].GetComponent<MovingObject>();
                obj.MoveObject();
            }
        }

        if (rotateObj != null)
        {
            for (int i = 0; i < rotateObj.Length; i++)
            {
                // ���� �߰�
            }
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        // �Ѿ˿� �浹�� 3�ʰ� �۵�
        if (System.Array.Exists(collisionBullet, tag => tag == collision.gameObject.tag))
        {
            StartCoroutine(OnButton(3f));
        }
    }
    IEnumerator OnButton(float time)
    {
        activate = !activate;
        yield return new WaitForSeconds(time);
        activate = !activate;
    }
}
