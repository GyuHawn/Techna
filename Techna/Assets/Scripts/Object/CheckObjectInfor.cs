using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckObjectInfor : MonoBehaviour
{
    public int expansValue; // 최대 확장값
    public int reducedValue; // 최대 확장값
    public int currentValue; // 현재값

    public float weight; // 무게

    public bool expansion; // 크기 조절 가능 오브젝트인지
}
