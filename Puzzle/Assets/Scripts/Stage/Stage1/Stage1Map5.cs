using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Map5 : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject[] plates;

    public bool activate;
    public bool open;

    public GameObject player;

    void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Start()
    {
        foreach (GameObject plate in plates)
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                plateFunction.OnActivationChanged += CheckAllPlatesActivated;
            }
        }
    }

    void OnDestroy()
    {
        foreach (GameObject plate in plates)
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                plateFunction.OnActivationChanged -= CheckAllPlatesActivated;
            }
        }
    }

    void Update()
    {
        // Stage에 진입했을 때만 판 활성화 확인
        CheckAllPlatesActivated(false);
    }

    void CheckAllPlatesActivated(bool dummy)
    {
        bool allPlatesActivated = true;

        foreach (GameObject plate in plates)
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                if (!plateFunction.activate)
                {
                    allPlatesActivated = false;
                    break;
                }
            }
            else
            {
                allPlatesActivated = false;
                break;
            }
        }

        if (allPlatesActivated)
        {
            activate = true;
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        activate = false;
        if(!open)
        {
            open = true;
            StartCoroutine(MovingDoor());
        }
    }

    IEnumerator MovingDoor()
    {
        float elapsedTime = 0f;
        float duration = 3f;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(0, 16, 0);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }
}
