using System;

public class IdleState : IMovingState, IRotateState
{
    public void Update(MovingObject movingObject)
    {
        // 1. ���� �̵�, ���� Ȱ��ȭ, �̵� �� �ƴ�
        if (movingObject.gem != null && movingObject.gem.activate && !movingObject.isMoving)
        {
            if (movingObject.controlMoving) // ���� �̵�
            {
                movingObject.StartMoving();
            }
            else if (movingObject.objectMoving) // ��ü �̵�
            {
                movingObject.StartMovingObject();
            }
        }

        // 2. ��ü �̵�, ������Ʈ üũ
        if (movingObject.objectMoving && !movingObject.isMoving)
        {
            if (movingObject.checkObj != null) // üũ�� ������Ʈ�� ���� ���
            {
                bool activate = movingObject.CheckObject();
                if (activate)
                {
                    movingObject.StartMoving();
                }
            }
            else if (movingObject.checkObj == null && movingObject.activated) // üũ�� ������Ʈ ���� Ȱ��ȭ ������ ��
            {
                movingObject.StartMoving();
            }
        }

        // 3. �ݺ� �̵�
        if (movingObject.autoMoving && !movingObject.isMoving)
        {
            movingObject.StartRepeatingMove();
        }

        // 4. Ư�� ��ġ ���� �ݺ� �̵�
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
        // 1. ���� ���� ȸ��
        if (rotateObject.gamRotate && rotateObject.gem != null && rotateObject.gem.activate && !rotateObject.rotating)
        {
            rotateObject.rotating = true;
            rotateObject.StartCoroutine(rotateObject.RotateAndWait());
        }

        // 2. ���� ȸ��
        if (rotateObject.objRotate && !rotateObject.rotating && rotateObject.activate)
        {
            rotateObject.rotating = true;
            rotateObject.StartCoroutine(rotateObject.Rotate());
        }

        // 3. ������ ��ġ�� ȸ��
        if (rotateObject.objRotateSetting && !rotateObject.rotating && rotateObject.activate)
        {
            rotateObject.rotating = true;
            rotateObject.StartCoroutine(rotateObject.RotateValueSetting());
        }
    }
}
