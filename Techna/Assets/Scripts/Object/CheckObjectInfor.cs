using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckObjectInfor : MonoBehaviour
{
    [Header("크기 조절 값 설정")]
    public int expansValue; // 최대 증가값
    public int reducedValue; // 최대 감소값
    public int currentValue; // 현재값

    [Header("크기 조절 가능 여부")]
    public bool expansion; // 크기 조절 가능 오브젝트 인지

    [Header("무게")]
    public float weight; // 무게

}
