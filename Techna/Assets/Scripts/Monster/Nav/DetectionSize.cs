using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSize : MonoBehaviour
{
    public Vector3 detectionBoxSize; // �÷��̾� ���� ���� (������ü�� ũ��)

    private void OnDrawGizmos() // ���� ���� �ð�ȭ
    {
        if (gameObject.transform.position != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gameObject.transform.position, detectionBoxSize);
        }
    }
}
