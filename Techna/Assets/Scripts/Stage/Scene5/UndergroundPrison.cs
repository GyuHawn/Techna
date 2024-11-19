using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundPrison : MonoBehaviour
{
    public GameObject key; // ���� 
    public GameObject hint; // ��Ʈ
    public GameObject bread; // ��

    private void OnTriggerEnter(Collider other)
    {
        // �� �ٽ� ��Ʈ�� Ű ȹ��
        if (other.gameObject == bread)
        {
            Destroy(bread);
            GetHint();
        }
    }

    void GetHint()
    {
        Rigidbody keyRigid = key.GetComponent<Rigidbody>();
        Rigidbody hintRigid = hint.GetComponent<Rigidbody>();

        keyRigid.AddForce(new Vector3(10, 0, 0), ForceMode.Impulse);
        hintRigid.AddForce(new Vector3(15, 0, 0), ForceMode.Impulse);
    }
}
