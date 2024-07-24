using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float mouseSensitivity;
    public float jumpPower;

    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded;

    private Rigidbody rb;

    public Transform gunPos;
    public Vector3 gunOffset;

    public bool isCursorVisible = true; // 마우스 커서 활성화 여부
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 회전은 물리 엔진이 처리하지 않도록 고정

        moveSpeed = 15f;
        mouseSensitivity = 300f;
        jumpPower = 7f;

        gunOffset = new Vector3(0, 1.2f, 0);
    }

    void Update()
    {
        GetInput();
        Rotate();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        UpdateGunPosition();

        OffCursorVisibility("`"); // 마우스 커서 비/활성화
    }

    void FixedUpdate()
    {
        Move();
    }

    void UpdateGunPosition()
    {
        gunPos.position = gameObject.transform.position + gunOffset; // 플레이어 위치에 오프셋 적용
    }

    void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    void Move()
    {
        Vector3 moveDirection = transform.right * hAxis + transform.forward * vAxis;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);

        gunPos.localRotation = Camera.main.transform.localRotation;
    }

    void Jump()
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
