using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedBox : MonoBehaviour
{
    public GameObject lockObj; // �ڹ���
    public GameObject boxLid; // �ڽ� �Ѳ�

    [Header("���� ����")]
    public BoxType boxType; 

    public enum BoxType
    {
        Rusty, // �콼�ڽ�
        Silver, // �ǹ��ڽ�
        Golden // ���ڽ�
    }

    [Header("���� Ȯ��")]
    public bool openedBox; // ���� ���� Ȯ��

    private void OnCollisionEnter(Collision collision)
    {
        // ������ �ʰ� �˸��� �����϶�
        if (!openedBox && IsCorrectKey(collision.gameObject.name))
        {
            OpenedBox(); // �ڽ� ����
            Destroy(collision.gameObject);
        }
    }

    private bool IsCorrectKey(string keyName)
    {
        switch (boxType) // �ڽ� Ÿ�� Ȯ��
        {
            case BoxType.Rusty:
                return keyName == "RustyKey"; // ����Ȯ��
            case BoxType.Silver:
                return keyName == "SilverKey";
            case BoxType.Golden:
                return keyName == "GoldenKey";
            default:
                return false;
        }
    }

    private void OpenedBox()
    {
        openedBox = true;
        Destroy(lockObj);
        Destroy(boxLid);
    }
}
