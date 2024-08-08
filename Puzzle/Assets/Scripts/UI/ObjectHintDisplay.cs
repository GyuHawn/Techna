using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectHintDisplay : MonoBehaviour
{
    public GameObject player;
    public string text;
    public TMP_Text showText;
    public bool showUI;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        showText.gameObject.SetActive(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == player)
        {
            if (!showUI)
            {
                showUI = true;
                showText.gameObject.SetActive(true);
                showText.text = text;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            if (showUI)
            {
                showUI = false;
                showText.gameObject.SetActive(false);
                showText.text = "";
            }
        }
    }
}
