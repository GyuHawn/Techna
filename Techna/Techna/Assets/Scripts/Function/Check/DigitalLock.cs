using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalLock : MonoBehaviour
{
    public GrabObject grabObject;

    public GameObject card; // ������ ī��Ű
    public Transform activatedCard; 

    public bool activate; // Ȱ��ȭ ����


    private void Start()
    {
        NullObjectFind(); // null ������Ʈ ã��
    }

    void NullObjectFind() // null ������Ʈ ã��
    {
        if (grabObject == null)
        {
            grabObject = GameObject.Find("Player").GetComponent<GrabObject>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == card) // ������Ʈ üũ
        {
            activate = true;
            ActivatedLock();
        }
    }

    void ActivatedLock() // ī�带 ������ ������ ���� �� ���� ������Ʈ
    {
        grabObject.grabbedObject = null;

        Rigidbody rb = card.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeAll;
        card.tag = "Untagged";

        card.transform.position = activatedCard.position;
        card.transform.rotation = Quaternion.Euler(84, -90, 0);
    }
}
