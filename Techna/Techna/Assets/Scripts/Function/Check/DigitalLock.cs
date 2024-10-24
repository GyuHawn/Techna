using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalLock : MonoBehaviour
{
    public GrabObject grabObject;

    public GameObject card; // 해제할 카드키
    public Transform activatedCard; 

    public bool activate; // 활성화 여부


    private void Start()
    {
        NullObjectFind(); // null 오브젝트 찾기
    }

    void NullObjectFind() // null 오브젝트 찾기
    {
        if (grabObject == null)
        {
            grabObject = GameObject.Find("Player").GetComponent<GrabObject>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == card) // 오브젝트 체크
        {
            activate = true;
            ActivatedLock();
        }
    }

    void ActivatedLock() // 카드를 잡을수 없도록 설정 및 정보 업데이트
    {
        grabObject.grabbedObject = null;

        Rigidbody rb = card.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeAll;
        card.tag = "Untagged";

        card.transform.position = activatedCard.position;
        card.transform.rotation = Quaternion.Euler(84, -90, 0);
    }
}
