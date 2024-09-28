using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

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
    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded; // 점프 여부
    private Vector3 moveDirection; // 이동 관련
    private Vector3 velocity; // 중력 및 이동속도 관리

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

    private void Awake()
    {
        // 씬에 따라 타임라인 세팅
        PlayableDirectorSetting();
    }

    void Start()
    {
        NullObjectFind(); // 오브젝트 찾기

        controller = GetComponent<CharacterController>();

        // 타임라인 종료 이벤트 추가
        pd.stopped += OnTimelineStopped;
        StartCinemachine();

        currentStage = 1;

        moveSpeed = 10f;
        mouseSensitivity = 100f;
        jumpPower = 1.5f;

        maxHealth = 100;
        currentHealth = maxHealth;
        damage = 3;

        gunOffset = new Vector3(0, 1.2f, 0);
    }

    void PlayableDirectorSetting()
    {
        // 현재 씬 이름 확인
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Stage1")
        {
            pd.Play();  // 타임라인 시작
        }
        else
        {
            pd.Stop();  // 타임라인 중지
        }
    }

    void StartCinemachine()
    {
        moving = false;
        canvasCamera.SetActive(false);
    }

    // 타임라인 종료 시
    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == pd)
        {
            moving = true; // 이동 가능 상태로 변경
            canvasCamera.SetActive(true); // 캔버스 카메라 활성화
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
            GetInput(); // 키입력
            Move(); // 이동
            Rotate(); // 회전
            Jump(); // 점프 및 중력 처리
        }

        UpdateGunPosition(); // 총 위치 설정

        FunctionLever(); // 레버 작동

        UPdateInfor(); // 플레이어 정보 업데이트

        ApplyPlatformMovement(); // 이동 발판의 이동값 적용
    }

    public void ApplyJump(Vector3 jump)
    {
        velocity = jump;
    }

    void ApplyPlatformMovement() // 이동 발판위에 있을 시 함께 이동
    {
        if (movingPlatform != null)
        {

            Vector3 platformMovement = movingPlatform.position - lastPlatformPosition;

            controller.Move(platformMovement);

            if (movingPlatform != null)
            {
                lastPlatformPosition = movingPlatform.position;
            }
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
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
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