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

    public string[] collidingTag = {"Floor", "Wall"}; // 충돌 확인할 태그
    public bool colliding; // 충돌중인지

    private void OnCollisionStay(Collision collision)
    {
        // 태그 확인 후 충돌 확인
        if (System.Array.Exists(collidingTag, tag => tag == collision.gameObject.tag))
        {
            colliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }
}
