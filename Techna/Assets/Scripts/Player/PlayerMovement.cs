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
    [Header("��ġ")]
    public float moveSpeed; // �̵��ӵ�
    public float mouseSensitivity; // ���콺 ����
    public float jumpPower; // ������
    public int damage; // ������

    [Header("ü��")]
    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public Image healthBar; // ü�¹�
    public TMP_Text healthText; // ü�� �ؽ�Ʈ

    // ü�� ��ȭ �̺�Ʈ
    public delegate void HealthChanged(int currentHealth, int maxHealth);
    public event HealthChanged healthChanged;

    [Header("����")]
    public int currentStage; // ���� ��������
    public bool moving; // �̵� ���� ����
    public bool isMove; // �̵��� ����

    [Header("�ǰ�")]
    public bool hit; // �ǰ� ���� ����
    public GameObject dieUI;
    public bool isDie; // ��� ����

    [Header("Ű�Է�")]
    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded; // ���� ����
    private Vector3 moveDirection; // �̵� ����
    public Vector3 velocity; // �߷� �� �̵��ӵ� ����

    [Header("�� ��ġ")]
    public Transform gunPos;
    public Vector3 gunOffset;

    [Header("�̵� ����")]
    public Transform movingPlatform; // �̵� ����
    private Vector3 lastPlatformPosition; // ���������� ��ϵ� ������ ��ġ

    [Header("����")]
    private bool checkLever = false;
    private GameObject currentLever;

    [Header("ĳ���� ��Ʈ�ѷ�")]
    private CharacterController controller;
    private float gravity = -9.81f; // �߷�

    private Animator anim;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        healthChanged += UpdateHealthUI; // ü�� �̺�Ʈ ����
    }

    private void OnDisable()
    {
        healthChanged -= UpdateHealthUI; // ü�� �̺�Ʈ ���� ����
    }

    void Start()
    {              
        currentStage = 1; // ���� ��������

        // �̵�����
         moveSpeed = 10f; 
        mouseSensitivity = 100f;
        jumpPower = 1.5f;

        // �������ͽ�
        maxHealth = 100;
        currentHealth = maxHealth;
        damage = 3;

        // �� ��ġ
        gunOffset = new Vector3(0, 1.2f, 0);
    }
    
    void Update()
    {
        if (isDie) return;

        if (moving)
        {
            GetInput(); // Ű�Է�
            Move(); // �̵�
            MoveAnimation(); // �̵� �ִϸ��̼�
            Rotate(); // ȸ��
            Jump(); // ���� �� �߷� ó��
        }

        Die(); // ���
        UpdateGunPosition(); // �� ��ġ ����
        FunctionLever(); // ���� �۵�
        ApplyPlatformMovement(); // �̵� ������ �̵��� ����
    }

    void ApplyPlatformMovement() // �̵� �������� ���� �� �Բ� �̵�
    {
        if (movingPlatform != null)
        {
            Vector3 platformMovement = movingPlatform.position - lastPlatformPosition;
            controller.Move(platformMovement);

            lastPlatformPosition = movingPlatform.position;
        }
    }

    void UpdateGunPosition() // �� ��ġ ����
    {
        gunPos.position = gameObject.transform.position + gunOffset; // �÷��̾� ��ġ�� ������ ����
    }

    void GetInput() // Ű�Է�
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    void Move() // �̵�
    {
        moveDirection = (transform.right * hAxis + transform.forward * vAxis).normalized;

        isMove = moveDirection.magnitude > 0; // �̵��� Ȯ��

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void MoveAnimation() // �̵� �ִϸ��̼�
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
    public void ShotAnimation() // ���� �ִϸ��̼�
    {
        anim.SetTrigger("Shot");
    }

    void Rotate() // ȸ��
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);

        gunPos.localRotation = Camera.main.transform.localRotation;
    }

    void Jump() // ���� �� �߷� ó��
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // �ٴڿ� ������ �ӵ� �ʱ�ȭ
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

        velocity.y += gravity * Time.deltaTime; // �߷� ����
        controller.Move(velocity * Time.deltaTime); // �߷¿� ���� �̵�

    }

    void FunctionLever()
    {
        if (checkLever && Input.GetButtonDown("Function") && currentLever) // ���� �۵�
        {
            LeverFunction lever = currentLever.GetComponent<LeverFunction>();
            lever.activate = true;
        }
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        healthText.text = $"HP {currentHealth} / {maxHealth}"; // �ؽ�Ʈ ������Ʈ
        healthBar.fillAmount = (float)currentHealth / maxHealth; // ü�� �� ������Ʈ
    }

    void Die() // �����
    {
        if (currentHealth <= 0)
        {
            isDie = true;
            StartCoroutine(PlayerDie());
        }
    }

    IEnumerator PlayerDie() // �÷��̾� ����� [���� ���ư��� ���X (�ϴ� �������� �̵�)]
    {
        dieUI.SetActive(true); 
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // �ٴڿ� ������ ���� ����
        if (hit.gameObject.CompareTag("Floor") || hit.gameObject.CompareTag("MovingObject"))
        {
            isGrounded = true;
        }

        // �̵� ���� ���� ������ �̵��� ��ȭ
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
        // ���� �浹, ���� �߰�
        if (other.gameObject.CompareTag("Lever"))
        {
            checkLever = true;
            currentLever = other.gameObject;
        }

        // ����
        if (other.gameObject.CompareTag("Thorn"))
        {
            TrapScript thorn = other.gameObject.GetComponent<TrapScript>();
            StartCoroutine(HitDamage(thorn.damage));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // �ǰ�
        if (other.gameObject.CompareTag("Monster"))
        {
            MonsterController monster = other.gameObject.GetComponent<MonsterController>();
            StartCoroutine(HitDamage(monster.damage));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���� �浹 ����, ���� ����
        if (other.gameObject.CompareTag("Lever"))
        {
            checkLever = false;
            currentLever = null;
        }
    }

    public IEnumerator HitDamage(int damage) // �ǰ�
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
