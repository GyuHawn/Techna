using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector3 targetPosition;

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