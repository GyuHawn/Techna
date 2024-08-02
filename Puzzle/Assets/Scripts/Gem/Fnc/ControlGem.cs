using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGem : MonoBehaviour
{
    public Material[] gemMaterials;
    public bool onControl;

    private Renderer render;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!onControl)
        {
            render.material = gemMaterials[0];
        }
        else
        {
            render.material = gemMaterials[1];
        }
    }
}
