using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    public PlayableDirector pd;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pd != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerMovement player = other.GetComponent<PlayerMovement>();
                player.pd = pd;

                player.pd.Play();
            }
        }

    }
}
