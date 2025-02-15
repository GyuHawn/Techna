using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionObject : MonoBehaviour
{
    public GameObject[] objects;

    public BoxCollider collider;

    public void Breaking()
    {
        collider.enabled = false;

        foreach (GameObject obj in objects)
        {
            Rigidbody rigid = obj.GetComponent<Rigidbody>();

            rigid.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destruction"))
        {
            Breaking();
            Destroy(gameObject, 3f);
        }
    }
}
