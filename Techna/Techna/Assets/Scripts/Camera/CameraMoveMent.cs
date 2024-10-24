using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // 플레이어
    public Vector3 cameraOffset; // 카메라 위치 

    void Start()
    {
        transform.SetParent(player);  // 카메라를 플레이어의 자식으로 설정, 플레이어와 함께 이동     
        transform.localPosition = cameraOffset; // 카메라가 플레이어를 따라 이동     
        transform.localRotation = Quaternion.identity; // 카메라의 회전 = 플레이어의 회전
    }

    void Update()
    {
        transform.localPosition = cameraOffset; // 플레이어의 회전에 따라 카메라 회전
    }
}
