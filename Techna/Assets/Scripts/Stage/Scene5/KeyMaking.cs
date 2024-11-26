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

        keyPosition = new Vector3(key.transform.position.x, key.transform.position.y + 0.1f, key.transform.position.z);
        flaskPosition = new Vector3(flask.transform.position.x, flask.transform.position.y + 0.1f, flask.transform.position.z);
        bookpuzzleItemPosition = new Vector3(bookpuzzleItem.transform.position.x, bookpuzzleItem.transform.position.y + 0.1f, bookpuzzleItem.transform.position.z);
        flaskpuzzleItemPosition = new Vector3(flaskpuzzleItem.transform.position.x, flaskpuzzleItem.transform.position.y + 0.1f, flaskpuzzleItem.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cauldronm.active)
        {
            foreach (var item in items)
            {
                if (item != null && other.gameObject == item)
                {
                    makingNum++;
                    Destroy(other.gameObject);
                    break;
                }
            }

            if (makingNum >= 4)
            {
                goldenKey.SetActive(true);
                specialFlask.SetActive(true);
            }
        }
        else
        {
            cauldronm.Failed();
        }
    }

    void KeyMove()
    {
        Rigidbody keyObj = key.GetComponent<Rigidbody>();
        // 움직임 정지
        keyObj.velocity = Vector3.zero;
        keyObj.angularVelocity = Vector3.zero;

        key.transform.position = keyPosition;
    }
    void FlaskMove()
    {
        Rigidbody flaskObj = flask.GetComponent<Rigidbody>();
        // 움직임 정지
        flaskObj.velocity = Vector3.zero;
        flaskObj.angularVelocity = Vector3.zero;

        flask.transform.position = flaskPosition;
    }
    void BookpuzzleItemMove()
    {
        Rigidbody bookpuzzleItemObj = bookpuzzleItem.GetComponent<Rigidbody>();
        // 움직임 정지
        bookpuzzleItemObj.velocity = Vector3.zero;
        bookpuzzleItemObj.angularVelocity = Vector3.zero;

        bookpuzzleItem.transform.position = bookpuzzleItemPosition;
    }
    void FlaskpuzzleItemMove()
    {
        Rigidbody flaskpuzzleItemObj = flaskpuzzleItem.GetComponent<Rigidbody>();
        // 움직임 정지
        flaskpuzzleItemObj.velocity = Vector3.zero;
        flaskpuzzleItemObj.angularVelocity = Vector3.zero;

        flaskpuzzleItemObj.transform.position = flaskpuzzleItemPosition;
    }
}
