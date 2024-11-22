using System.Collections.Generic;
using UnityEngine;

public class KeyMaking : MonoBehaviour
{
    public Cauldronm cauldronm;

    public GameObject key;
    public GameObject flask;
    public GameObject bookpuzzleItem;
    public GameObject flaskpuzzleItem;

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
    }
}
