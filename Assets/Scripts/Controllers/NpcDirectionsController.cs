using System.Collections.Generic;
using UnityEngine;

public delegate void OnAlmostReached(Vector2 almostReachedPoint);

public class NpcDirectionsController
{
    private Queue<Vector2> _routePoints;

    public NpcDirectionsController()
    {
        _routePoints = new Queue<Vector2>();
    }

    public void OnGetNewCoordinate(Vector2 coordinate)
    {
        _routePoints.Enqueue(coordinate);
    }

    public Direction? GetMovementDirection(
        Vector2 currentNpcPosition,
        OnAlmostReached onAlmostReached
    )
    {
        if (_routePoints.Count == 0) return null;

        Vector2 nextPoint = _routePoints.Peek();

        float targetPointNpcXDiff = Mathf.Abs(nextPoint.x - currentNpcPosition.x);
        float targetPointNpcYDiff = Mathf.Abs(nextPoint.y - currentNpcPosition.y);

        bool isAlmostReached = targetPointNpcXDiff < 0.01 && targetPointNpcYDiff < 0.01;

        if (isAlmostReached)
        {
            onAlmostReached(nextPoint);
            _routePoints.Dequeue();
            return null;
        }
        else
        {
            float xDiff = nextPoint.x - currentNpcPosition.x;
            float yDiff = nextPoint.y - currentNpcPosition.y;

            float biggestDiff = Mathf.Max(Mathf.Abs(xDiff), Mathf.Abs(yDiff));

            Direction movementDirection = Direction.Create(xDiff / biggestDiff, yDiff / biggestDiff);
            return movementDirection;
        }
    }
}
