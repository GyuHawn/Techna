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
        UpdateColor(); // 기능 시 색 변경
    }

    void UpdateColor()
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

    IEnumerator ChangeColor(float changeTime)
    {
        onControl = true;

        yield return new WaitForSeconds(changeTime);

        onControl = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BasicControl")
        {
            StartCoroutine(ChangeColor(3f));
        }
        else if (collision.gameObject.name == "LargeControl")
        {
            StartCoroutine(ChangeColor(5f));
        }
    }
}
