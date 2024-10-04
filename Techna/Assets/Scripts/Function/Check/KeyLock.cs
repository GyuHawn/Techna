using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    public bool activate; // 활성화 여부

    private void OnCollisionEnter(Collision collision)
    {
        // 키 충돌시 활성화 후 키 제거
        if(collision.gameObject.name == "Key")
        {
            activate = true;
            Destroy(collision.gameObject);
        }
    }
}
