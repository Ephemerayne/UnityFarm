
using System;
/// <summary>
/// Movement direction.
/// Values should be between -1 and 1;
/// </summary>
public class Direction
{
    private float _x;
    private float _y;

    private Direction(float x, float y)
    {
        _x = x;
        _y = y;
    }

    public float x => _x;
    public float y => _y;

    public static Direction Create(float x, float y)
    {
        float _x = x;
        float _y = y;

        if (_x > 1) _throw(_x);
        if (_x < -1) _throw(_x);
        if (_y > 1) _throw(_y);
        if (_y < -1) _throw(_y);

        return new Direction(_x, _y);
    }

    private static void _throw(float f)
    {
        throw new ArgumentOutOfRangeException(f + "should be in range -1 to 1");
    }
}
