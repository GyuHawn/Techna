using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundPrison : MonoBehaviour
{
    public GameObject key; // ���� 
    public GameObject bread; // ��

    private void OnTriggerEnter(Collider other)
    {
        // �� �ٽ� Ű ȹ��
        if (other.gameObject == bread)
        {
            Destroy(bread);
            GetKey();
        }
    }

    void GetKey() // Ű ȹ��
    {
        Rigidbody keyRigid = key.GetComponent<Rigidbody>();
        keyRigid.AddForce(new Vector3(10, 0, 0), ForceMode.Impulse);
    }
}
