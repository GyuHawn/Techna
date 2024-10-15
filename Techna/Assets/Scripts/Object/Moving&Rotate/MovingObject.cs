using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public IMovingState currentState;
    private ICommand command;
    public ActivateGem gem; // ���� Ȯ��

    [Header("�̵� ����")]
    public float moveNum; // �̵� �Ÿ�
    public bool x; // x�� �̵� ����
    public bool y; // y�� �̵� ����
    public bool z; // z�� �̵� ����
    public float moveDuration; // ���� �ð�

    [Header("�̵� �غ�")]
    public bool activated; // �׳� �̵� �غ�

    [Header("üũ�� ������Ʈ")]
    public GameObject checkObj; // üũ�� ������Ʈ

    [Header("Ȱ��ȭ üũ ������Ʈ")]
    public bool plateObj; // ���� ����
    public bool lightObj; // ����Ʈ ����
    public bool electrictyObj; // ���� ����
    public bool digitalLockObj; // ���� �����ġ ����
    public bool keyLockObj; // Ű �����ġ ����
    public bool playerObj; // �÷��̾� ����

    private Vector3 currentPosition; // �ʱ� ��ġ
    private Vector3 targetPosition; // ��ǥ ��ġ
    public bool isMoving = false; // �̵� �� ����

    [Header("Ư�� ������Ʈ �̵�")]
    public GameObject movingObject; // �̵���ų ��ü
    public Transform objectMovePos; // �̵���ų ��ġ

    [Header("�̵� ���")]
    public bool autoMoving; // ��� �̵� (�ݺ� �̵�)
    public bool controlMoving; // ���� �̵� (�ѹ��� �����̵���)
    public bool controlAutoMoving; // Ư����ġ ���� �ݺ� �̵�(Ư����ġ�� �ݺ��̵�)
    public bool objectMoving; // ��ü �̵� (�ٸ� ������Ʈ�� �̵���Ű����)
    public bool pointMoving; // Ư�� ��ġ�� �̵�

    private void Start()
    {
        currentState = new IdleState();
        currentPosition = transform.localPosition; // �ʱ� ��ġ ����
        targetPosition = CalculateTargetPosition(); // ��ǥ ��ġ ����
    }

    private void Update()
    {
        currentState.Update(this); // ���� ������ Update �޼ҵ� ȣ��
    }

    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    // ���� Ȱ��ȭ �̺�Ʈ
    private void OnEnable()
    {
        if(gem != null)
        {
            gem.activationChanged += HandleGemActivated;
        }
    }

    private void OnDisable()
    {
        if(gem != null)
        {
            gem.activationChanged -= HandleGemActivated;
        }
    }

    // ���¿� ���� ��� ����
    private void HandleGemActivated(bool activate)
    {
        if (activate)
        {
            if (controlMoving)
            {
                StartMoving();
            }
            else if (objectMoving)
            {
                if (CheckObject())
                {
                    StartMoving();
                }
            }
            else if (autoMoving)
            {
                StartRepeatingMove();
            }
            else if (controlAutoMoving && CheckObject())
            {
                StartRepeatingMoveAtPosition(objectMovePos.position);
            }
            else if (pointMoving)
            {
                StartMovingObject();
            }
        }
    }


    public void StartMoving()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject, targetPosition));
        }
        if (command != null)
        {
            command.Execute(this);
        }
    }

    // �ٸ� ��ü�� �̵�
    public void StartMovingObject()
    {
        if (!isMoving)
        {
            isMoving = true;

            // ���� ��ǥ�� ���� ����Ͽ� �θ��� ������ ���� �ʵ��� ��
            Vector3 localPosition = objectMovePos.localPosition;

            StartCoroutine(MovePosition(movingObject, localPosition));
        }
    }

    // �ݺ� �̵� ����
    public void StartRepeatingMove()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(RepeatMove(gameObject, targetPosition));
        }
    }

    // Ư�� ��ġ�� �ݺ� �̵� ����
    public void StartRepeatingMoveAtPosition(Vector3 position)
    {
        if (!isMoving)
        {
            isMoving = true;

            // ���� ��ǥ�� ���� ����Ͽ� �θ��� ������ ���� �ʵ��� ��
            Vector3 localPosition = objectMovePos.localPosition;

            if (movingObject == null)
            {
                movingObject = gameObject;
            }

            StartCoroutine(RepeatMove(movingObject, localPosition));
        }
    }

    // Ư�� ������Ʈ�� Ȱ��ȭ ���� Ȯ��
    public bool CheckObject()
    {
        if (plateObj)
        {
            PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
            return plateFunction != null && plateFunction.activate;
        }
        else if (lightObj)
        {
            LightningRod lightingFunction = checkObj.GetComponent<LightningRod>();
            return lightingFunction != null && lightingFunction.activate;
        }
        else if (electrictyObj)
        {
            CheckElectricity electricityFunction = checkObj.GetComponent<CheckElectricity>();
            return electricityFunction != null && electricityFunction.activate;
        }
        else if (digitalLockObj)
        {
            DigitalLock digitalLockFunction = checkObj.GetComponent<DigitalLock>();
            return digitalLockFunction != null && digitalLockFunction.activate;
        }
        else if (keyLockObj)
        {
            KeyLock keyLockFunction = checkObj.GetComponent<KeyLock>();
            return keyLockFunction != null && keyLockFunction.activate;
        }
        else if (playerObj)
        {
            PlayerCheck playerCheck = checkObj.GetComponent<PlayerCheck>();
            return playerCheck != null && playerCheck.activate;
        }
        return false;
    }



    public void MoveObject() // Ÿ��ũ��Ʈ ��� �̵��ڵ�
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject, targetPosition));
        }
    }

    private Vector3 CalculateTargetPosition() // ��ǥ ��ġ ���
    {
        return currentPosition + new Vector3(x ? moveNum : 0, y ? moveNum : 0, z ? moveNum : 0);
    }

    IEnumerator RepeatMove(GameObject obj, Vector3 targetPosition) // �ݺ��̵�
    {
        while (true)
        {
            yield return MovePosition(obj, targetPosition);
            yield return MovePosition(obj, currentPosition);
        }
    }

    public IEnumerator MovePosition(GameObject obj, Vector3 targetPos) // �̵�
    {
        Vector3 startPosition = obj.transform.localPosition; // ���� ��ġ ����
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration) // õõ�� �̵�
        {
            Vector3 currentPosition = Vector3.MoveTowards(startPosition, targetPos, (targetPos - startPosition).magnitude * (elapsedTime / moveDuration));

            obj.transform.localPosition = currentPosition; // ���� �������� �̵�


            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��ǥ ����
        obj.transform.localPosition = targetPos; // ���� �������� ��ǥ ��ġ ����

        isMoving = false;
    }
}
