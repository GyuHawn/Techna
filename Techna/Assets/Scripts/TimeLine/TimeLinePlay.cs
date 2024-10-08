using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLinePlay : MonoBehaviour
{
    public TimeLineManager timeLineManager;

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
                pd.Play();
            }
        }

    }
}
