using System.Collections.Generic;
using UnityEngine;

public class RouteController
{
    private Transform _npcTransform;
    private Queue<Vector2> _routePoints;

    public RouteController(Transform npcTransform)
    {
        _npcTransform = npcTransform;
        _routePoints = new Queue<Vector2>();
    }

    public void OnGetNewCoordinate(Vector2 coordinate)
    {
        _routePoints.Enqueue(coordinate);
    }

    public Vector2? CheckRoutePoints()
    {
        if (_routePoints.Count == 0) return null;

        Vector2 nextPoint = _routePoints.Peek();
        Vector2 currentNpcPosition = new Vector2(_npcTransform.position.x, _npcTransform.position.y);

        float targetPointNpcXDiff = Mathf.Abs(nextPoint.x - currentNpcPosition.x);
        float targetPointNpcYDiff = Mathf.Abs(nextPoint.y - currentNpcPosition.y);

        bool isAlmostReached = targetPointNpcXDiff < 0.01 && targetPointNpcYDiff < 0.01;

        if (isAlmostReached)
        {
            _npcTransform.position = nextPoint;
            _routePoints.Dequeue();
            return null;
        }
        else
        {
            float xDiff = nextPoint.x - currentNpcPosition.x;
            float yDiff = nextPoint.y - currentNpcPosition.y;

            float biggestDiff = Mathf.Max(Mathf.Abs(xDiff), Mathf.Abs(yDiff));

            Vector2 movementDirection = new Vector2(xDiff / biggestDiff, yDiff / biggestDiff);
            return movementDirection;
        }
    }
}
