using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public GameObject waterBucket; // �� �ٱ���

    private void OnTriggerEnter(Collider other)
    {
        // �� ����
        if (other.gameObject == waterBucket)
        {
            Destroy(waterBucket);
            Destroy(gameObject, 1f);
        }
    }
}
