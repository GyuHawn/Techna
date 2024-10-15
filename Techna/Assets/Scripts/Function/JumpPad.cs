using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPower;
    public Vector3 jumpDirection = Vector3.up;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                Vector3 jump = jumpDirection * jumpPower; // 점프력
                                                            
                PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.velocity = jump;  // 플레이어 점프
                }
            }
        }
    }
}
