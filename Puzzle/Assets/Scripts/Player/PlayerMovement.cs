using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // 이동속도
    public float mouseSensitivity; // 마우스 감도
    public float jumpPower; // 점프력

    // 상태
    public int maxHealth;
    public int currentHealth;
    public Image healthBar;
    public TMP_Text healthText;

    public int damage; // 데미지

    public bool hit; // 피격 가능 여부

    // 키입력
    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded; // 점프 여부

    // 총위치
    public Transform gunPos; 
    public Vector3 gunOffset;

    public bool isCursorVisible; // 마우스 커서 활성화 여부

    public Transform movingPlatform; // 이동 발판
    private Vector3 lastPlatformPosition; // 마지막으로 기록된 발판의 위치

    // 레버 작동
    private bool checkLever = false;
    private GameObject currentLever;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 회전은 물리 엔진이 처리하지 않도록 고정

        //moveSpeed = 10f; 
        moveSpeed = 20f; 
        mouseSensitivity = 100f;
        jumpPower = 6f;

        maxHealth = 100;
        currentHealth = maxHealth;

        damage = 3;

        gunOffset = new Vector3(0, 1.2f, 0);

        isCursorVisible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        GetInput(); // 키입력
        Move(); // 이동
        Rotate(); // 회전
        Jump(); // 점프

        UpdateGunPosition(); // 총 위치 설정
        OffCursorVisibility(); // 마우스 커서 비/활성화

        FunctionLever(); // 레버 작동

        UPdateInfor(); // 플레이어 정보 업데이트
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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FunctionLever()
    {
        if (checkLever && Input.GetButtonDown("lever")) // 레버 작동
        {
            if (currentLever != null)
            {
                LeverFunction lever = currentLever.GetComponent<LeverFunction>();
                if (lever != null)
                {
                    lever.activate = true;
                }
            }
        }
    }

    void UPdateInfor()
    {
        healthText.text = "HP " + currentHealth.ToString() + " / " + maxHealth.ToString();

        // 현재 체력을 최대 체력으로 나눈 비율을 체력 바의 Fill Amount로 설정
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 바닥에 있을때 점프 가능
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("MovingObject"))
        {
            isGrounded = true;
        }

        // 이동 발판 위에 있을시 이동값 변화
        if (collision.gameObject.CompareTag("MovingObject"))
        {
            movingPlatform = collision.transform; // 이동 발판
            lastPlatformPosition = movingPlatform.position; // 마지막 위치 저장
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 이동 발판 충돌 해제
        if (collision.gameObject.CompareTag("MovingObject"))
        {
            movingPlatform = null; // 연결 해제
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        // 레버 충돌, 레버 추가
        if (other.gameObject.CompareTag("Lever"))
        {
            checkLever = true;
            currentLever = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 피격
        if (other.gameObject.CompareTag("Monster"))
        {
            MonsterController monster = other.gameObject.GetComponent<MonsterController>();
            HitDamage(other.gameObject, monster.damage);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        // 레버 충돌 해제, 레버 삭제
        if (other.gameObject.CompareTag("Lever"))
        {
            checkLever = false;
            currentLever = null;
        }
    }

    private void OffCursorVisibility() // 커서 비활성화
    {
        if (Input.GetButtonDown("CursorHide")) // 키 입력 감지
        {
            isCursorVisible = !isCursorVisible; // 마우스 포인터 활성화 여부
            UnityEngine.Cursor.visible = isCursorVisible; // 마우스 포인터 활성화 상태 설정
            UnityEngine.Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked; // 마우스 포인터 잠금 상태 설정
        }
    }

    private void HitDamage(GameObject obj, int damage) // 피격
    {
        if (!hit)
        {
            hit = true;
            currentHealth -= damage;

            StartCoroutine(HitInterval());
        }
    }

    IEnumerator HitInterval() // 피격 간격
    {
        yield return new WaitForSeconds(1f);
        hit = false;
    }
}
