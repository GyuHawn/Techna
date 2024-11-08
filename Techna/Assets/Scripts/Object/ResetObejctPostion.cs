using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectPosition : MonoBehaviour
{
    public Transform resetPos; // ���� ��ġ
    public bool objectRestrictions; // ������Ʈ �̵� ����

    private void OnTriggerEnter(Collider other)
    {
        if (objectRestrictions) // ���� ������Ʈ�� ���� ���������� �ѱ�� ������ ����
        {
            if (other.CompareTag("Player")) // �÷��̰� ���� ������Ʈ ����
            {  
                GrabObject obj = other.gameObject.GetComponent<GrabObject>();
                if (obj.grabbedObject != null)
                {
                    Rigidbody grabbedRigidbody = obj.grabbedObject.GetComponent<Rigidbody>();
                    if (grabbedRigidbody != null)
                    {
                        grabbedRigidbody.freezeRotation = false; // ȸ�� ���� ����
                        grabbedRigidbody.isKinematic = false; // ���� ȿ�� �ٽ� Ȱ��ȭ
                    }

                    obj.grab = false;
                    obj.grabbedObject = null;
                }
            }
            else if (other.CompareTag("GrabObject")) // �Ϻ� ������Ʈ�� ������ �Ѿ�� ��Ȳ ���
            {
                Rigidbody grabbedRigidbody = other.GetComponent<Rigidbody>();
                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.velocity = Vector3.zero; // �ӵ� �ʱ�ȭ
                    grabbedRigidbody.angularVelocity = Vector3.zero; // ȸ�� �ʱ�ȭ
                }
            }
        }
        else // Ư�� �ٴ����� ���������� ��ġ ����
        {
            if (other.CompareTag("GrabObject")) // ������Ʈ
            {
                other.transform.position = resetPos.position;
            }
            else if (other.CompareTag("Player")) // �÷��̾�
            {
                CharacterController characterController = other.GetComponent<CharacterController>();
                characterController.enabled = false;
                other.transform.position = resetPos.position;
                characterController.enabled = true;
            }
        }
    }
}
