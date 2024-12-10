using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Clear : MonoBehaviour
{
    public int currentSkullNum; // 현재 가지고 있는 해골수 
    public GameObject door; // 제거해야 할 벽
    public GameObject potal; // 이동용 포탈

    public GameObject[] emptySkulls; // 빈 해골
    public Transform[] skullPositions; // 빈 해골 위치

    private void OnTriggerEnter(Collider other)
    {
        // 해골 아이템 충돌시 빈 해골 위치로 이동
        // 5개의 해골을 모을시 문을 제거하고 포탈을 열도록
        if (other.gameObject.name == "Skull")
        {
            if (currentSkullNum < 5)
            {
                other.gameObject.name = "Check";

                Rigidbody skull = other.gameObject.GetComponent<Rigidbody>();
                skull.isKinematic = true;

                other.gameObject.transform.position = skullPositions[currentSkullNum].position;
                other.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);

                emptySkulls[currentSkullNum].SetActive(false);
                currentSkullNum++;

                if(currentSkullNum >= 5)
                {
                    door.SetActive(false);
                    potal.SetActive(true);
                }
            }
        }
    }
}
