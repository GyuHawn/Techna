using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    public bool activate; // Ȱ��ȭ ����

    private void OnCollisionEnter(Collision collision)
    {
        // Ű �浹�� Ȱ��ȭ �� Ű ����
        if(collision.gameObject.name == "Key")
        {
            activate = true;
            Destroy(collision.gameObject);
        }
    }
}
