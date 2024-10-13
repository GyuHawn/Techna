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
    public float moveSpeed; // �̵��ӵ�
    public float mouseSensitivity; // ���콺 ����
    public float jumpPower; // ������

    // ����
    public bool moving; // �̵� ���� ����
    public bool isMove; // �̵��� ����
    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public Image healthBar; // ü�¹�
    public TMP_Text healthText; // ü�� �ؽ�Ʈ
    public int currentStage; // ���� ��������

    public int damage; // ������

    public bool hit; // �ǰ� ���� ����
    public GameObject dieUI;

    // Ű�Է�
    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded; // ���� ����
    private Vector3 moveDirection; // �̵� ����
    private Vector3 velocity; // �߷� �� �̵��ӵ� ����

    // ����ġ
    public Transform gunPos;
    public Vector3 gunOffset;

    public Transform movingPlatform; // �̵� ����
    private Vector3 lastPlatformPosition; // ���������� ��ϵ� ������ ��ġ

    // ���� �۵�
    private bool checkLever = false;
    private GameObject currentLever;

    private CharacterController controller;
    private float gravity = -9.81f; // �߷�

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
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
        if (moving)
        {
            GetInput(); // Ű�Է�
            Move(); // �̵�
            Rotate(); // ȸ��
            Jump(); // ���� �� �߷� ó��
        }

        Die(); // ���

        UpdateGunPosition(); // �� ��ġ ����

        FunctionLever(); // ���� �۵�

        HealthUpdate(); // ü���� ������Ʈ

        ApplyPlatformMovement(); // �̵� ������ �̵��� ����
    }

    public void ApplyJump(Vector3 jump) // ���� �е� ���� �ڵ�
    {
        velocity = jump;
    }

    void ApplyPlatformMovement() // �̵� �������� ���� �� �Բ� �̵�
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

        if (moveDirection.magnitude > 0) // �̵��� ����
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
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
        if (checkLever && Input.GetButtonDown("Function")) // ���� �۵�
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

    void HealthUpdate()  // ü�¹� ������Ʈ
    {
        healthText.text = "HP " + currentHealth.ToString() + " / " + maxHealth.ToString(); //

        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }

    void Die() // �����
    {
        if (currentHealth <= 0)
        {
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
            currentHealth -= thorn.damage;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // �ǰ�
        if (other.gameObject.CompareTag("Monster"))
        {
            MonsterController monster = other.gameObject.GetComponent<MonsterController>();
            HitDamage(other.gameObject, monster.damage);
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

    private void HitDamage(GameObject obj, int damage) // �ǰ�
    {
        if (!hit)
        {
            hit = true;
            currentHealth -= damage;

            StartCoroutine(HitInterval());
        }
    }

    IEnumerator HitInterval() // �ǰ� ����
    {
        yield return new WaitForSeconds(1f);
        hit = false;
    }
}