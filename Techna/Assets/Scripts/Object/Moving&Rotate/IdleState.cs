using System;

public class IdleState : IMovingState, IRotateState
{
    public void Update(MovingObject movingObject)
    {
        // 1. 제어 이동, 보석 활성화, 이동 중 아님
        if (movingObject.gem != null && movingObject.gem.activate && !movingObject.isMoving)
        {
            if (movingObject.controlMoving) // 제어 이동
            {
                movingObject.StartMoving();
            }
            else if (movingObject.objectMoving) // 물체 이동
            {
                movingObject.StartMovingObject();
            }
        }

        // 2. 물체 이동, 오브젝트 체크
        if (movingObject.objectMoving && !movingObject.isMoving)
        {
            if (movingObject.checkObj != null) // 체크할 오브젝트가 있을 경우
            {
                bool activate = movingObject.CheckObject();
                if (activate)
                {
                    movingObject.StartMoving();
                }
            }
            else if (movingObject.checkObj == null && movingObject.activated) // 체크할 오브젝트 없고 활성화 상태일 때
            {
                movingObject.StartMoving();
            }
        }

        // 3. 반복 이동
        if (movingObject.autoMoving && !movingObject.isMoving)
        {
            movingObject.StartRepeatingMove();
        }

        // 4. 특정 위치 제어 반복 이동
        if (movingObject.controlAutoMoving && movingObject.checkObj != null && !movingObject.isMoving)
        {
            bool activate = movingObject.CheckObject();
            if (activate)
            {
                movingObject.StartRepeatingMoveAtPosition(movingObject.objectMovePos.position);
            }
        }
    }

    public void Update(RotateObject rotateObject)
    {
        // 1. 보석 관련 회전
        if (rotateObject.gamRotate && rotateObject.gem != null && rotateObject.gem.activate && !rotateObject.rotating)
        {
            rotateObject.rotating = true;
            rotateObject.StartCoroutine(rotateObject.RotateAndWait());
        }

        // 2. 무한 회전
        if (rotateObject.objRotate && !rotateObject.rotating && rotateObject.activate)
        {
            rotateObject.rotating = true;
            rotateObject.StartCoroutine(rotateObject.Rotate());
        }

        // 3. 정해진 위치로 회전
        if (rotateObject.objRotateSetting && !rotateObject.rotating && rotateObject.activate)
        {
            rotateObject.rotating = true;
            rotateObject.StartCoroutine(rotateObject.RotateValueSetting());
        }
    }
}
