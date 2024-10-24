using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunction : MonoBehaviour
{
    public GameObject[] movingObj; // 이동하는 오브젝트
    public GameObject[] rotateObj; // 회전하는 오브젝트

    public bool activate; // 활성화

    [Header("확인할 총알")]
    public string[] collisionBullet = new string[] { "Bullet", "Expansion", "Penetrate" };

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
            for(int i = 0; i < movingObj.Length; i++)
            {
                MovingObject obj = movingObj[i].GetComponent<MovingObject>();
                obj.MoveObject();
            }
        }

        if (rotateObj != null)
        {
            for (int i = 0; i < rotateObj.Length; i++)
            {
                // 사용시 추가
            }
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        // 총알에 충돌시 3초간 작동
        if (System.Array.Exists(collisionBullet, tag => tag == collision.gameObject.tag))
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
