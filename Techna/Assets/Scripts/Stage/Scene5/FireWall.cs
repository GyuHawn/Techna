using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public GameObject waterBucket; // 물 바구니

    private void OnTriggerEnter(Collider other)
    {
        // 불 끄기
        if (other.gameObject == waterBucket)
        {
            Destroy(waterBucket);
            Destroy(gameObject, 1f);
        }
    }
}
