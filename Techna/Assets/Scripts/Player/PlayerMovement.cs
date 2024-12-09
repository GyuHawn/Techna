using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [Header("수치")]
    public float moveSpeed; // 이동속도
    public float mouseSensitivity; // 마우스 감도
    public float jumpPower; // 점프력
    public int damage; // 데미지

    [Header("체력")]
    public int maxHealth; // 최대 체력
    public int currentHealth; // 현재 체력
    public Image healthBar; // 체력바
    public TMP_Text healthText; // 체력 텍스트

    // 체력 변화 이벤트
    public delegate void HealthChanged(int currentHealth, int maxHealth);
    public event HealthChanged healthChanged;

    [Header("상태")]
    public int currentStage; // 현재 스테이지
    public bool moving; // 이동 가능 상태
    public bool isMove; // 이동중 상태

    [Header("피격")]
    public bool hit; // 피격 가능 여부
    public GameObject dieUI;
    public bool isDie; // 사망 여부

    [Header("키입력")]
    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded; // 점프 여부
    private Vector3 moveDirection; // 이동 관련
    public Vector3 velocity; // 중력 및 이동속도 관리

    [Header("총 위치")]
    public Transform gunPos;
    public Vector3 gunOffset;

    [Header("이동 발판")]
    public Transform movingPlatform; // 이동 발판
    private Vector3 lastPlatformPosition; // 마지막으로 기록된 발판의 위치

    [Header("러바")]
    private bool checkLever = false;
    private GameObject currentLever;

    [Header("캐릭터 컨트롤러")]
    private CharacterController controller;
    private float gravity = -9.81f; // 중력

    private Animator anim;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        healthChanged += UpdateHealthUI; // 체력 이벤트 구독
    }

    private void OnDisable()
    {
        healthChanged -= UpdateHealthUI; // 체력 이벤트 구독 해제
    }

    void Start()
    {              
        currentStage = 1; // 현재 스테이지

        // 이동관련
         moveSpeed = 10f; 
        mouseSensitivity = 100f;
        jumpPower = 1.5f;

        // 스테이터스
        maxHealth = 100;
        currentHealth = maxHealth;
        damage = 3;

        // 총 위치
        gunOffset = new Vector3(0, 1.2f, 0);
    }
    
    void Update()
    {
        if (isDie) return;

        if (moving)
        {
            GetInput(); // 키입력
            Move(); // 이동
            MoveAnimation(); // 이동 애니메이션
            Rotate(); // 회전
            Jump(); // 점프 및 중력 처리
        }

        Die(); // 사망
        UpdateGunPosition(); // 총 위치 설정
        FunctionLever(); // 레버 작동
        ApplyPlatformMovement(); // 이동 발판의 이동값 적용
    }

    void ApplyPlatformMovement() // 이동 발판위에 있을 시 함께 이동
    {
        if (movingPlatform != null)
        {
            Vector3 platformMovement = movingPlatform.position - lastPlatformPosition;
            controller.Move(platformMovement);

            lastPlatformPosition = movingPlatform.position;
        }
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
        moveDirection = (transform.right * hAxis + transform.forward * vAxis).normalized;

        isMove = moveDirection.magnitude > 0; // 이동중 확인

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void MoveAnimation() // 이동 애니메이션
    {
        if(moveDirection.magnitude > 0)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }
    public void ShotAnimation() // 공격 애니메이션
    {
        anim.SetTrigger("Shot");
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

    void Jump() // 점프 및 중력 처리
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 바닥에 닿으면 속도 초기화
            isGrounded = true;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
            isGrounded = false;

            if (movingPlatform != null)
            {
                movingPlatform = null;
            }
        }

        velocity.y += gravity * Time.deltaTime; // 중력 적용
        controller.Move(velocity * Time.deltaTime); // 중력에 따른 이동

    }

    void FunctionLever()
    {
        if (checkLever && Input.GetButtonDown("Function") && currentLever) // 레버 작동
        {
            LeverFunction lever = currentLever.GetComponent<LeverFunction>();
            lever.activate = true;
        }
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        healthText.text = $"HP {currentHealth} / {maxHealth}"; // 텍스트 업데이트
        healthBar.fillAmount = (float)currentHealth / maxHealth; // 체력 바 업데이트
    }

    void Die() // 사망시
    {
        if (currentHealth <= 0)
        {
            isDie = true;
            StartCoroutine(PlayerDie());
        }
    }

    IEnumerator PlayerDie() // 플레이어 사망시 [현재 돌아가는 기능X (일단 메인으로 이동)]
    {
        dieUI.SetActive(true); 
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // 바닥에 있을때 점프 가능
        if (hit.gameObject.CompareTag("Floor") || hit.gameObject.CompareTag("MovingObject"))
        {
            isGrounded = true;
        }

        // 이동 발판 위에 있을시 이동값 변화
        if (hit.gameObject.CompareTag("MovingObject"))
        {
            if (movingPlatform == null)
            {
                movingPlatform = hit.transform;
                lastPlatformPosition = movingPlatform.position;
            }
        }
        else
        {
            if (movingPlatform != null)
            {
                movingPlatform = null;
            }
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

        // 함정
        if (other.gameObject.CompareTag("Thorn"))
        {
            TrapScript thorn = other.gameObject.GetComponent<TrapScript>();
            StartCoroutine(HitDamage(thorn.damage));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 피격
        if (other.gameObject.CompareTag("Monster"))
        {
            MonsterController monster = other.gameObject.GetComponent<MonsterController>();
            StartCoroutine(HitDamage(monster.damage));
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

    public IEnumerator HitDamage(int damage) // 피격
    {
        if (!hit)
        {
            hit = true;
            TakeDamage(damage);
            yield return new WaitForSeconds(1f);

            hit = false;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthChanged?.Invoke(currentHealth, maxHealth);
    }
}
