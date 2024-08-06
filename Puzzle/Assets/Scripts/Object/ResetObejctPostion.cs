using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObejctPostion : MonoBehaviour
{
    public Transform resetPos;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GrabObject"))
        {
            collision.gameObject.transform.position = resetPos.position;
        }
    }
}
