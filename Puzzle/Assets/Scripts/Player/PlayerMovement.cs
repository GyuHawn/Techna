using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // 이동속도
    public float mouseSensitivity; // 마우스 감도
    public float jumpPower; // 점프력

    // 키입력
    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded; // 점프 여부
    
    // 총위치
    public Transform gunPos; 
    public Vector3 gunOffset;

    public bool isCursorVisible; // 마우스 커서 활성화 여부

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 회전은 물리 엔진이 처리하지 않도록 고정

        moveSpeed = 15f; 
        mouseSensitivity = 300f;
        jumpPower = 7f;

        gunOffset = new Vector3(0, 1.2f, 0);

        isCursorVisible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput(); // 키입력
        Move(); // 이동
        Rotate(); // 회전

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump(); // 점프
        }

        UpdateGunPosition(); // 총 위치 설정
        OffCursorVisibility("`"); // 마우스 커서 비/활성화
    }

    void UpdateGunPosition() // 총 위치 설정
    {
        gunPos.position = gameObject.transform.position + gunOffset; // 플레이어 위치에 오프셋 적용
    }

    void GetInput() // 키입력
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    void Move() // 이동
    {
        Vector3 moveDirection = transform.right * hAxis + transform.forward * vAxis;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    void Rotate() // 회전
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);

        gunPos.localRotation = Camera.main.transform.localRotation;
    }

    void Jump() // 점프
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OffCursorVisibility(string KeyNum) // 커서 비활성화
    {
        if (Input.GetButtonDown(KeyNum)) // 키 입력 감지
        {
            isCursorVisible = !isCursorVisible; // 마우스 포인터 활성화 여부
            Cursor.visible = isCursorVisible; // 마우스 포인터 활성화 상태 설정
            Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked; // 마우스 포인터 잠금 상태 설정
        }
    }
}
