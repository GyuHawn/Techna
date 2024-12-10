using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Clear : MonoBehaviour
{
    public int currentSkullNum; // ���� ������ �ִ� �ذ�� 
    public GameObject door; // �����ؾ� �� ��
    public GameObject potal; // �̵��� ��Ż

    public GameObject[] emptySkulls; // �� �ذ�
    public Transform[] skullPositions; // �� �ذ� ��ġ

    private void OnTriggerEnter(Collider other)
    {
        // �ذ� ������ �浹�� �� �ذ� ��ġ�� �̵�
        // 5���� �ذ��� ������ ���� �����ϰ� ��Ż�� ������
        if (other.gameObject.name == "Skull")
        {
            if (currentSkullNum < 5)
            {
                other.gameObject.name = "Check";

                Rigidbody skull = other.gameObject.GetComponent<Rigidbody>();
                skull.isKinematic = true;

                other.gameObject.transform.position = skullPositions[currentSkullNum].position;
                other.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);

                emptySkulls[currentSkullNum].SetActive(false);
                currentSkullNum++;

                if(currentSkullNum >= 5)
                {
                    door.SetActive(false);
                    potal.SetActive(true);
                }
            }
        }
    }
}
