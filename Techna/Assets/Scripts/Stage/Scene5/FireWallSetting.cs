using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallSetting : MonoBehaviour
{
    public GameObject fireWall;

    public bool actived;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fireWall.SetActive(actived);           
        }
    }
}
