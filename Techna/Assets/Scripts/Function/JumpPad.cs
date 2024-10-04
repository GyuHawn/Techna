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
                // 점프력을 적용하기 위한 Vector3를 생성합니다.
                Vector3 jump = jumpDirection * jumpPower;

                // PlayerMovement 스크립트가 있다면, 점프 힘을 전달해줄 수 있습니다.
                PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.ApplyJump(jump);
                }
            }
        }
    }
}
