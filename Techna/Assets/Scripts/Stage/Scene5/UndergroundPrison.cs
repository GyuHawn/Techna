using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundPrison : MonoBehaviour
{
    public GameObject key; // ø≠ºË 
    public GameObject bread; // ªß

    private void OnTriggerEnter(Collider other)
    {
        // ªß ¡ŸΩ√ ≈∞ »πµÊ
        if (other.gameObject == bread)
        {
            Destroy(bread);
            GetKey();
        }
    }

    void GetKey() // ≈∞ »πµÊ
    {
        Rigidbody keyRigid = key.GetComponent<Rigidbody>();
        keyRigid.AddForce(new Vector3(10, 0, 0), ForceMode.Impulse);
    }
}
