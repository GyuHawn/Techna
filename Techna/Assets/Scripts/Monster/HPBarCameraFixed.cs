using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarCameraFixed : MonoBehaviour
{
    Transform camera;

    void Start()
    {
        camera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + camera.rotation * Vector3.forward, camera.rotation * Vector3.up);
    }
}
