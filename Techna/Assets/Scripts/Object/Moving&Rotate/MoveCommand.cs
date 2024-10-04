using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector3 targetPosition; // 이동 관련

    public MoveCommand(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public void Execute(MovingObject movingObject) 
    {
        if (!movingObject.isMoving)
        {
            movingObject.isMoving = true;
            movingObject.StartCoroutine(movingObject.MovePosition(movingObject.gameObject, targetPosition));
        }
    }
}