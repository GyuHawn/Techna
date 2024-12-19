using System.Collections.Generic;
using UnityEngine;

public class KeyMaking : MonoBehaviour
{
    public Cauldronm cauldronm;

    [Header("필요 아이템 / 처음 위치")]
    public GameObject key;
    public Vector3 keyPosition;
    public GameObject flask;
    public Vector3 flaskPosition;
    public GameObject bookpuzzleItem;
    public Vector3 bookpuzzleItemPosition;
    public GameObject flaskpuzzleItem;
    public Vector3 flaskpuzzleItemPosition;

    public int makingNum;

    [Header("보상 아이템")]
    public GameObject goldenKey;
    public GameObject specialFlask;

    private List<GameObject> items;

    private void Awake()
    {
        cauldronm = GetComponent<Cauldronm>();
    }

    private void Start() // 아이템 리스트 설정 및 처음 위치 값 수정
    {
        items = new List<GameObject> { key, flask, bookpuzzleItem, flaskpuzzleItem };

        keyPosition = key.transform.position + Vector3.up * 0.1f;
        flaskPosition = flask.transform.position + Vector3.up * 0.1f;
        bookpuzzleItemPosition = bookpuzzleItem.transform.position + Vector3.up * 0.1f;
        flaskpuzzleItemPosition = flaskpuzzleItem.transform.position + Vector3.up * 0.1f;
    }

    // 아이템 충돌시
    private void OnTriggerEnter(Collider other)
    {
        // 가마솥에 불이 켜져있는지 확인
        if (cauldronm.active)
        {
            HandleKeyCollision(other);
        }
        else
        {
            ResetMakingState(); 
        }
    }

    // 알맞은 아이템인지 확인
    // 조합시 UI표시
    // 4개의 알맞은 레시피라면 보상 아이템 획득
    private void HandleKeyCollision(Collider other)
    {
        if (items.Contains(other.gameObject))
        {
            cauldronm.keyStatusUI.gameObject.SetActive(true);
            makingNum++;

            other.gameObject.SetActive(false);

            if (makingNum >= 4)
            {
                ResetMakingState();

                goldenKey.SetActive(true);
                specialFlask.SetActive(true);

                foreach (var item in items)
                {
                    Destroy(item);
                }
            }
        }
        else if (other.gameObject == cauldronm.fireWood) {} // 장작은 제외
        else
        {
            // 실패 처리
            cauldronm.MixtureFailed();
            ResetMakingState();
        }
    }

    // 실패시 상태 초기화
    private void ResetMakingState()
    {
        foreach (var item in items)
        {
            item.gameObject.SetActive(true);
        }

        cauldronm.keyStatusUI.gameObject.SetActive(false);

        makingNum = 0;
        KeyMove();
        FlaskMove();
        BookpuzzleItemMove();
        FlaskpuzzleItemMove();

    }

    // 아이템 사용시 위치 초기화
    void KeyMove()
    {
        Rigidbody keyObj = key.GetComponent<Rigidbody>();
        keyObj.velocity = Vector3.zero;
        keyObj.angularVelocity = Vector3.zero;
        key.transform.position = keyPosition;
    }

    void FlaskMove()
    {
        Rigidbody flaskObj = flask.GetComponent<Rigidbody>();
        flaskObj.velocity = Vector3.zero;
        flaskObj.angularVelocity = Vector3.zero;
        flask.transform.position = flaskPosition;
    }

    void BookpuzzleItemMove()
    {
        Rigidbody bookpuzzleItemObj = bookpuzzleItem.GetComponent<Rigidbody>();
        bookpuzzleItemObj.velocity = Vector3.zero;
        bookpuzzleItemObj.angularVelocity = Vector3.zero;
        bookpuzzleItem.transform.position = bookpuzzleItemPosition;
    }

    void FlaskpuzzleItemMove()
    {
        Rigidbody flaskpuzzleItemObj = flaskpuzzleItem.GetComponent<Rigidbody>();
        flaskpuzzleItemObj.velocity = Vector3.zero;
        flaskpuzzleItemObj.angularVelocity = Vector3.zero;
        flaskpuzzleItem.transform.position = flaskpuzzleItemPosition;
    }
}
