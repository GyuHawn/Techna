using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFunction : MonoBehaviour
{
    public GameObject[] movingObj; // 이동하는 오브젝트
    public GameObject[] rotateObj; // 회전하는 오브젝트

   // public GameObject functionObj; // 제어할 오브젝트
    public GameObject checkObj; // 확인할 오브젝트

    public bool activate; // 활성화
    public bool checkPlate; // 특정 오브젝트에만 활성화 되도록 

    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // 이벤트 호출

    private void Update()
    {
        // 이동, 회전할 오브젝트가 있고 활성화 시
        if ((movingObj != null || rotateObj != null) && activate)
        {
            OnActivate(); // 이동, 회전
        }
    }

    void OnActivate() // 이동, 회전
    {
        if (movingObj != null)
        {
            for (int i = 0; i < movingObj.Length; i++) // 모든 오브젝트 이동
            {
                MovingObject obj = movingObj[i].GetComponent<MovingObject>();
                obj.MoveObject();
            }
        }

        if (rotateObj != null)
        {
            foreach (var r_Obj in rotateObj) // 모든 오브젝트 회전
            {
                // RotateObject obj = r_Obj.GetComponent<RotateObject>();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // checkPlate 일때는 알맞은 오브젝트인지 확인
        if (checkPlate)
        {
            if (collision.gameObject == checkObj)
            {
                activate = true;
                activationChanged?.Invoke(true);
            }
        }
        else // 아닐시 태그 확인
        {
            if (collision.gameObject.CompareTag("GrabObject"))
            {
                activate = true;
                activationChanged?.Invoke(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (checkPlate)
        {
            if (collision.gameObject == checkObj)
            {
                activate = false;
                activationChanged?.Invoke(false);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("GrabObject"))
            {
                activate = false;
                activationChanged?.Invoke(false);
            }
        }
    }
}
