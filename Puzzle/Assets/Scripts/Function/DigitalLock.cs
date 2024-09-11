using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalLock : MonoBehaviour
{
    public GameObject card; // 해제할 카드키
    public Transform activatedCard; 

    public bool activate; // 활성화 여부

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == card)
        {
            activate = true;
            activatedLock();
        }
    }

    void activatedLock()
    {
        Rigidbody rb = card.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeAll;
        card.tag = "Untagged";

        card.transform.position = activatedCard.position;
        card.transform.rotation = Quaternion.Euler(0, 0, 76.5f);
    }
}
