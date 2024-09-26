using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // 이동속도
    public float mouseSensitivity; // 마우스 감도
    public float jumpPower; // 점프력

    // 상태
    public bool moving; // 이동 가능 상태
    public int maxHealth; // 최대 체력
    public int currentHealth; // 현재 체력
    public Image healthBar; // 체력바
    public TMP_Text healthText; // 체력 텍스트
    public int currentStage; // 현재 스테이지

    public int damage; // 데미지

    public bool hit; // 피격 가능 여부

    // 키입력
    private bool isGrounded; // 점프 여부
    private Vector3 moveDirection; // 이동 관련
    private Vector3 velocity; // 중력 및 이동속도 관리
    private float rotationY;

    // 총위치
    public Transform gunPos;
    public Vector3 gunOffset;

    public Transform movingPlatform; // 이동 발판
    private Vector3 lastPlatformPosition; // 마지막으로 기록된 발판의 위치

    // 레버 작동
    private bool checkLever = false;
    private GameObject currentLever;

    private CharacterController controller;
    private float gravity = -9.81f; // 중력

    public PlayableDirector pd;
    public GameObject canvasCamera;
    public MeshRenderer[] deactivateMesh; // 타임라인중 비활성화할 오브젝트

    private PlayerInputActions inputActions; // Input Actions 변수

    private void Awake()
    {
        inputActions = new PlayerInputActions(); // Input Actions 초기화

        // 씬에 따라 타임라인 세팅
        PlayableDirectorSetting();
    }

    private void OnEnable()
    {
        inputActions.Enable(); // Input Actions 활성화
    }

    private void OnDisable()
    {
        inputActions.Disable(); // Input Actions 비활성화
    }

    void Start()
    {
        NullObjectFind(); // 오브젝트 찾기

        controller = GetComponent<CharacterController>();

        // 타임라인 종료 이벤트 추가
        pd.stopped += OnTimelineStopped;
        StartCinemachine();

        currentStage = 1;

        moveSpeed = 12f;
        mouseSensitivity = 10f;
        jumpPower = 2f;

        maxHealth = 100;
        currentHealth = maxHealth;
        damage = 3;

        gunOffset = new Vector3(0, 1.2f, 0);

        // Input Actions에 메서드 연결
        inputActions.PlayerActions.Move.performed += OnMove; // Move 입력 연결
        inputActions.PlayerActions.Look.performed += OnLook; // Look 입력 연결
        inputActions.PlayerActions.Jump.performed += OnJump; // Jump 입력 연결
    }

    void PlayableDirectorSetting()
    {
        // 현재 씬 이름 확인
        string currentScene = SceneManager.GetActiveScene().name;

        // 씬 이름에 따라 타임라인 배열에서 타임라인 선택 및 재생/정지
        if (currentScene == "Stage1")
        {
            pd.Play();  // Stage1일 경우 타임라인 시작
        }
        else
        {
            pd.Stop();  // 다른 스테이지에서는 타임라인 중지
        }
    }

    void StartCinemachine()
    {
        moving = false;
        canvasCamera.SetActive(false);
    }

    // 타임라인 종료 시 호출되는 함수
    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == pd)
        {
            moving = true; // 이동 가능 상태로 변경
            canvasCamera.SetActive(true); // 캔버스 카메라 활성화

            // deactivateMesh의 메쉬 렌더러 활성화
            foreach (MeshRenderer meshRenderer in deactivateMesh)
            {
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = true; // 메쉬 렌더러 활성화
                }
            }
        }
    }

    void NullObjectFind() // 오브젝트 찾기
    {
        if (canvasCamera == null)
        {
            canvasCamera = GameObject.Find("CanvasCamera");
        }
    }

    void Update()
    {
        NullObjectFind(); // 오브젝트 찾기

        if (moving)
        {
            Move(); // 이동
        }

        UpdateGunPosition(); // 총 위치 설정
        FunctionLever(); // 레버 작동
        UPdateInfor(); // 플레이어 정보 업데이트
        ApplyPlatformMovement(); // 이동 발판의 이동값 적용
    }

    // Input System을 통한 이동
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        // 이동 방향 계산
        moveDirection = transform.right * input.x + transform.forward * input.y;

        if (context.canceled) // 입력이 해제되면
        {
            moveDirection = Vector3.zero; // 이동 방향을 0으로 설정
        }
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // 상하 회전 제한

        transform.Rotate(Vector3.up * mouseX); // 좌우 회전
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f); // 카메라 상하 회전
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded && context.performed)
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
        }
    }

    public void ApplyJump(Vector3 jump)
    {
        velocity = jump;
    }

    void ApplyPlatformMovement()
    {
        // 만약 이동 발판 위에 있다면
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

    void Move() // 이동
    {
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void FunctionLever()
    {
        if (checkLever && Input.GetButtonDown("Function")) // 레버 작동
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
        healthText.text = "HP " + currentHealth.ToString() + " / " + maxHealth.ToString(); //

        // 현재 체력을 최대 체력으로 나눈 비율을 체력 바의 Fill Amount로 설정
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
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
            movingPlatform = null;
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
            currentHealth -= thorn.damage;
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
