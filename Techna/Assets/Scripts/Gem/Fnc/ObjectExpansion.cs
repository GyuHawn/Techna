using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExpansion : MonoBehaviour
{
    public ExpansionConversion gun; // ���� ���� Ȯ��
    public float scaleChangeDuration; // ������ ��ȭ �ð�
    public float freezeDuration; // ������Ʈ ���� �ð�
    private bool isScaling = false; // ũ�� ��ȭ ������ ���θ� �����ϴ� ����

    private void Start()
    {
        gun = GameObject.Find("Gun").GetComponent<ExpansionConversion>();
        scaleChangeDuration = 2f;
        freezeDuration = 3f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ExpansionBullet")) // ���� �Ӽ� �Ѿ�
        {
            CheckObjectInfor cube = gameObject.GetComponent<CheckObjectInfor>();
            if (cube.expansion)
            {
                // ������Ʈ�� ũ�� ����
                HandleCollision();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ExpansionBullet")) // ���� �Ӽ� �Ѿ�
        {
            CheckObjectInfor cube = gameObject.GetComponent<CheckObjectInfor>();
            if (cube.expansion)
            {
                // ������Ʈ�� ũ�� ����
                HandleCollision();
            }
        }
    }

    void HandleCollision() // ũ�� ���� �� ������ �� ȸ�� ����, ������Ʈ �ִ� ���� �� Ȯ��
    {
        if (isScaling) return; // �̹� ũ�� ��ȭ ���̸� �Լ� ����

        gameObject.tag = "Untagged"; // �÷��̾ ������ ������ �±� ����

        // ��ġ, ȸ�� ����
        Vector3 originalPosition = gameObject.transform.position;
        Quaternion originalRotation = gameObject.transform.rotation;

        CheckObjectInfor check = gameObject.GetComponent<CheckObjectInfor>();

        if (gun.plus) // ũ�� ����
        {
            if (check.currentValue < check.expansValue)
            {
                check.currentValue++;
                isScaling = true; // ũ�� ��ȭ ����
                StartCoroutine(ScaleOverTime(gameObject, gameObject.transform.localScale * 2));
                check.weight = check.weight * 2;
            }
        }
        else // ũ�� ����
        {
            if (check.currentValue > check.reducedValue)
            {
                check.currentValue--;
                isScaling = true; // ũ�� ��ȭ ����
                StartCoroutine(ScaleOverTime(gameObject, gameObject.transform.localScale * 0.5f));
                check.weight = check.weight * 0.5f;
            }
        }

        StartCoroutine(FixedPostion()); // ������ ����

        // ����� ��ġ, ȸ������ ����
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
    }

    IEnumerator ScaleOverTime(GameObject obj, Vector3 targetScale)
    {
        Vector3 initialScale = obj.transform.localScale;  // ������Ʈ �ʱ� ������ ����
        float initialYPos = obj.transform.position.y;  // ������Ʈ �ʱ� Y ��ġ�� ����
        float elapsed = 0f;  // ��� �ð��� ����

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // ���� ��ȣ�ۿ��� ����
        }

        while (elapsed < scaleChangeDuration)
        {
            float progress = elapsed / scaleChangeDuration;  // ������ ��ȭ ���൵ ���

            // �������� �ʱ� �����Ͽ��� ��ǥ �����ϱ��� �����Ͽ� ���������� ��ȭ
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);

            // ���� �������� Y���� �����ͼ� ������ ��ȭ���� ���
            float currentScaleY = obj.transform.localScale.y;
            float scaleChangeY = currentScaleY - initialScale.y;

            // Y�� ��ġ�� ���� ������ ��ȭ�� ���缭 ����+1 ��ŭ �ø�
            obj.transform.position = new Vector3(obj.transform.position.x, initialYPos + ((scaleChangeY / 2) + 1), obj.transform.position.z);

            elapsed += Time.deltaTime;  // ��� �ð� ������Ʈ
            yield return null;
        }

        // ���� �������� ��ǥ �����Ϸ� ����
        obj.transform.localScale = targetScale;

        // ���� ������ ��ȭ���� ������� Y ��ġ�� ����
        float finalScaleChangeY = targetScale.y - initialScale.y;
        obj.transform.position = new Vector3(obj.transform.position.x, initialYPos + (finalScaleChangeY / 2), obj.transform.position.z);

        // ���� ��ȣ�ۿ��� ����
        if (rb != null)
        {
            rb.isKinematic = false;  // ���� ��ȣ�ۿ��� �ٽ� Ȱ��ȭ
        }

        gameObject.tag = "GrabObject"; // �±� ����
        isScaling = false;  // ������ ��ȭ�� �Ϸ� ǥ��
    }



    IEnumerator FixedPostion() // ������ ����
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        // �����ǰ� ȸ���� ����
        if (rb != null)
        {
            // ���� �ӵ�, ȸ���� ����
            Vector3 originalVelocity = rb.velocity;
            Vector3 originalAngularVelocity = rb.angularVelocity;

            // ������, ȸ�� ���� (�ӵ��� ȸ������ 0���� ����)
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // ���� �ð� ���� ������, ȸ���� ����
            yield return new WaitForSeconds(freezeDuration);

            // ���� �ӵ�, ȸ���� ����
            rb.velocity = originalVelocity;
            rb.angularVelocity = originalAngularVelocity;
        }
        else
        {
            // Kinematic ������ ���
            yield return new WaitForSeconds(freezeDuration);
        }
    }
}
