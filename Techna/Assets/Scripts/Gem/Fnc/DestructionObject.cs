using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destruction"))
        {
            Destroy(gameObject);
        }
    }
}
