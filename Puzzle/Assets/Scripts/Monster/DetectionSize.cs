using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSize : MonoBehaviour
{
    public Vector3 detectionBoxSize; // 플레이어 감지 범위 (정육면체의 크기)

    private void OnDrawGizmos() // 감지 범위 시각화
    {
        if (gameObject.transform.position != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gameObject.transform.position, detectionBoxSize);
        }
    }
}
