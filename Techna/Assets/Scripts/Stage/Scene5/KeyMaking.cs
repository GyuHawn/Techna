using System.Collections.Generic;
using UnityEngine;

public class KeyMaking : MonoBehaviour
{
    public Cauldronm cauldronm;

    public GameObject key;
    public Vector3 keyPosition;
    public GameObject flask;
    public Vector3 flaskPosition;
    public GameObject bookpuzzleItem;
    public Vector3 bookpuzzleItemPosition;
    public GameObject flaskpuzzleItem;
    public Vector3 flaskpuzzleItemPosition;

    public int makingNum;

    public GameObject goldenKey;
    public GameObject specialFlask;

    private List<GameObject> items;

    private void Awake()
    {
        cauldronm = GetComponent<Cauldronm>();
    }

    private void Start()
    {
        items = new List<GameObject> { key, flask, bookpuzzleItem, flaskpuzzleItem };

        keyPosition = key.transform.position + Vector3.up * 0.1f;
        flaskPosition = flask.transform.position + Vector3.up * 0.1f;
        bookpuzzleItemPosition = bookpuzzleItem.transform.position + Vector3.up * 0.1f;
        flaskpuzzleItemPosition = flaskpuzzleItem.transform.position + Vector3.up * 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cauldronm.active)
        {
            HandleKeyCollision(other);
        }
        else
        {
            ResetMakingState();
        }
    }

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
        else if (other.gameObject == cauldronm.fireWood) {}
        else
        {
            // 실패 처리
            cauldronm.MixtureFailed();
            ResetMakingState();
        }
    }

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
