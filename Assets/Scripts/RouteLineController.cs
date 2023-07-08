using UnityEngine;

public class RouteLineController
{

    private LineRenderer _line;

    public RouteLineController(LineRenderer line)
    {
        this._line = line;
    }

    public void SetPositions(Vector3[] points)
    {
        _line.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            _line.SetPosition(i, points[i]);
        }
    }
}
