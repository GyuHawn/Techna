using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTexture : MonoBehaviour
{
    public GemCombination gemCombination;

    public Texture[] attributeTexture; // 텍스쳐
    public Renderer render;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    public void TextuerSetting() // 속성에 따른 총 재질변경
    {
        if(gemCombination.selectAttributeNum == 0)
        {
            render.material.SetTexture("_EmissionMap", attributeTexture[0]);
        }
        else if (gemCombination.selectAttributeNum == 1)
        {
            render.material.SetTexture("_EmissionMap", attributeTexture[1]);
        }
        else if (gemCombination.selectAttributeNum == 2)
        {
            render.material.SetTexture("_EmissionMap", attributeTexture[2]);
        }
        else if (gemCombination.selectAttributeNum == 3)
        {
            render.material.SetTexture("_EmissionMap", attributeTexture[3]);
        }
        else if (gemCombination.selectAttributeNum == 4)
        {
            render.material.SetTexture("_EmissionMap", attributeTexture[4]);
        }
    }
}
