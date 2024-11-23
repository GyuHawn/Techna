using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Clear : MonoBehaviour
{
    public int clearNum;
    public GameObject door;

    public GameObject[] emptySkulls;
    public Transform[] skullPositions;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Skull")
        {
            if (clearNum < 5)
            {
                other.gameObject.name = "Check";

                Rigidbody skull = other.gameObject.GetComponent<Rigidbody>();
                skull.isKinematic = true;

                other.gameObject.transform.position = skullPositions[clearNum].position;
                other.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);

                emptySkulls[clearNum].SetActive(false);
                clearNum++;

                if(clearNum >= 5)
                {
                    door.SetActive(false);
                }
            }
        }
    }
}
