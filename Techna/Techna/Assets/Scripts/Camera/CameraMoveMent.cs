using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // �÷��̾�
    public Vector3 cameraOffset; // ī�޶� ��ġ 

    void Start()
    {
        transform.SetParent(player);  // ī�޶� �÷��̾��� �ڽ����� ����, �÷��̾�� �Բ� �̵�     
        transform.localPosition = cameraOffset; // ī�޶� �÷��̾ ���� �̵�     
        transform.localRotation = Quaternion.identity; // ī�޶��� ȸ�� = �÷��̾��� ȸ��
    }

    void Update()
    {
        transform.localPosition = cameraOffset; // �÷��̾��� ȸ���� ���� ī�޶� ȸ��
    }
}
