using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    private GemManager gemManager;

    private void Awake()
    {
        if (!gemManager)
            gemManager = GameObject.Find("GemManager").GetComponent<GemManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == gemManager.player)
        {
            gemManager.CollectGem(gameObject.name);
            Destroy(gameObject);
        }
    }
}
