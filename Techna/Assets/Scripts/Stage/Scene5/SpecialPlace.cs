using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlace : MonoBehaviour
{
    public GameObject placa;

    public GameObject flask;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == flask)
        {
            placa.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
