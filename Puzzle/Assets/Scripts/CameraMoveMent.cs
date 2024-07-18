using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 지정합니다.
    public Vector3 cameraOffset; // 카메라의 위치 오프셋을 설정합니다.

    // Start is called before the first frame update
    void Start()
    {
        // 카메라를 플레이어의 자식으로 설정하여 플레이어와 함께 이동하도록 합니다.
        transform.SetParent(player);
        // 카메라의 위치를 플레이어의 위치와 오프셋만큼 이동시킵니다.
        transform.localPosition = cameraOffset;
        // 카메라의 회전을 플레이어의 회전과 일치시킵니다.
        transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 회전에 따라 카메라가 회전하도록 합니다.
        transform.localPosition = cameraOffset;
    }
}
