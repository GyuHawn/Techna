using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunction : MonoBehaviour
{
    public GameObject[] movingObj; // 이동하는 오브젝트
    public GameObject[] rotateObj; // 회전하는 오브젝트

    public bool activate; // 활성화

    private void Update()
    {
        if (activate) // 활성화 시 작동
        {
            OnActivate();
        }
    }

    void OnActivate() // 이동 or 회전
    {
        if (movingObj != null)
        {
            foreach (var m_Obj in movingObj)
            {
                MovingObject obj = m_Obj.GetComponent<MovingObject>();
                obj.MoveObject();
            }
        }

        if (rotateObj != null)
        {
            foreach (var r_Obj in rotateObj)
            {
                // RotateObject obj = r_Obj.GetComponent<RotateObject>();
            }
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        // 총알에 충돌시 3초간 작동
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(OnButton(3f));
        }
    }
    IEnumerator OnButton(float time)
    {
        activate = !activate;
        yield return new WaitForSeconds(time);
        activate = !activate;
    }
}
