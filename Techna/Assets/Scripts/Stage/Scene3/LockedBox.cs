using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedBox : MonoBehaviour
{
    public GameObject lockObj; // 자물쇠
    public GameObject boxLid; // 박스 뚜껑

    [Header("상자 종류")]
    public BoxType boxType; 

    public enum BoxType
    {
        Rusty, // 녹슨박스
        Silver, // 실버박스
        Golden // 골드박스
    }

    [Header("상태 확인")]
    public bool openedBox; // 오픈 여부 확인

    private void OnCollisionEnter(Collision collision)
    {
        // 열리지 않고 알맞은 열쇠일때
        if (!openedBox && IsCorrectKey(collision.gameObject.name))
        {
            OpenedBox(); // 박스 열기
            Destroy(collision.gameObject);
        }
    }

    private bool IsCorrectKey(string keyName)
    {
        switch (boxType) // 박스 타입 확인
        {
            case BoxType.Rusty:
                return keyName == "RustyKey"; // 열쇠확인
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
