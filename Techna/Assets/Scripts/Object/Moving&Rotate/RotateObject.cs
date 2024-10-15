using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public IRotateState currentState;
    public ActivateGem gem;

    [Header("ȸ�� ����")]
    public float rotateDuration; // ȸ�� �ð�
    public float rotateNum; // ȸ�� ����
    public bool x; // x�� ȸ�� ����
    public bool y; // y�� ȸ�� ����
    public bool z; // z�� ȸ�� ����

    [Header("���ð�")]
    public float waitTime; // ȸ�� �� ��� �ð�

    [Header("ȸ������")]
    public bool rotating; // ȸ�� �� ����
    public bool activate; // �Ϲ� ȸ���� ȸ�� ���� Ȯ��

    [Header("ȸ��Ÿ��")]
    public bool gamRotate; // ���� ����
    public bool objRotate; // ���� ȸ��
    public bool objRotateSetting; // ������ ��ġ�� ȸ��

    private void Start()
    {
        currentState = new IdleState(); // �ʱ� ���� ����
    }

    private void Update()
    {
        currentState.Update(this); // ���� ������ Update �޼ҵ� ȣ��
    }

    // ���� Ȱ��ȭ �̺�Ʈ
    private void OnEnable()
    {
        if (gem != null)
        {
            gem.activationChanged += HandleGemActivated;
        }
    }
    private void OnDisable()
    {
        if (gem != null)
        {
            gem.activationChanged -= HandleGemActivated;
        }
    }

    // ���¿� ���� ��� ����
    private void HandleGemActivated(bool activate)
    {
        if (activate)
        {
            if (objRotateSetting)
            {
                StartCoroutine(Rotate());
            }
            else if (objRotate)
            {
                StartCoroutine(RotateValueSetting());
            }
            else if (gamRotate && !rotating)
            {
                rotating = true;
                StartCoroutine(RotateAndWait());
            }
        }
    }

    public IEnumerator Rotate() // ���� ȸ��
    {
        while (activate)
        {
            if (x) // x�������� ȸ��
            {
                transform.Rotate(Vector3.right * rotateNum * Time.deltaTime);
            }
            if (y) // y�������� ȸ��
            {
                transform.Rotate(Vector3.up * rotateNum * Time.deltaTime);
            }
            if (z) // z�������� ȸ��
            {
                transform.Rotate(Vector3.forward * rotateNum * Time.deltaTime);
            }

            yield return null; // ���� �����ӱ��� ���
        }

        rotating = false;
    }

    public IEnumerator RotateAndWait() // ���� ����ŭ ȸ���� ����� �ٽ� ȸ��
    {
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            if (x) // x�������� ȸ��
            {
                transform.Rotate(Vector3.right * rotateNum * Time.deltaTime / rotateDuration);
            }
            if (y) // y�������� ȸ��
            {
                transform.Rotate(Vector3.up * rotateNum * Time.deltaTime / rotateDuration);
            }
            if (z) // z�������� ȸ��
            {
                transform.Rotate(Vector3.forward * rotateNum * Time.deltaTime / rotateDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȸ�� �� ��� �ð�
        yield return new WaitForSeconds(waitTime);

        rotating = false;
    }

    public IEnumerator RotateValueSetting()
    {
        // ��ǥ ȸ������ ���� ȸ������ ��������� ���ϰų� ���� ������� ����
        Vector3 targetRotation = transform.eulerAngles;

        if (x) // x�� ȸ���� ������ ������ ��������� ����
        {
            targetRotation.x = rotateNum;
        }
        if (y) // y�� ȸ���� ������ ������ ��������� ����
        {
            targetRotation.y = rotateNum;
        }
        if (z) // z�� ȸ���� ������ ������ ��������� ����
        {
            targetRotation.z = rotateNum;
        }

        // ���� ȸ������ ��ǥ ȸ���� ������ ���� ���
        Vector3 startRotation = transform.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            // ���� ������ ���� ������ ��ǥ ȸ�������� ȸ��
            transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, elapsedTime / rotateDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }

        // ��ǥ ȸ�������� ��Ȯ�� ����
        transform.eulerAngles = targetRotation;

        // ȸ���� �Ϸ�Ǿ����Ƿ� ȸ�� �� ���¸� ����
        rotating = false;
    }
}
