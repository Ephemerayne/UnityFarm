using UnityEngine;

interface IMovementDirectionController
{
    public abstract Direction GetMovementDirection();
}

class PlayerDirectionController : IMovementDirectionController
{
    public Direction GetMovementDirection()
    {
        // -1 >= deltaX/deltaY >= 1
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        return Direction.Create(deltaX, deltaY);
    }
}

class NpcGeneratedDirectionsController : IMovementDirectionController
{
    private float lastTimeNpcMoved;
    private Direction currrentGeneratedDirection;

    public NpcGeneratedDirectionsController()
    {
        lastTimeNpcMoved = Time.time;
        currrentGeneratedDirection = _generateRandomMovementDirection();
    }

    public Direction GetMovementDirection()
    {
        float now = Time.time;
        if (now - lastTimeNpcMoved > 3)
        {
            currrentGeneratedDirection = _generateRandomMovementDirection();
            lastTimeNpcMoved = now;
        }

        return currrentGeneratedDirection;
    }

    private Direction _generateRandomMovementDirection()
    {
        int xDirection = Random.Range(-1, 2);
        int yDirection = Random.Range(-1, 2);
        return Direction.Create(xDirection, yDirection);
    }
}
