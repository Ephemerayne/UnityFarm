using UnityEngine;

interface IMoverController
{
    CollisionDirectionMover mover { get; }

    public abstract void Move();
}

class PlayerMoverController : IMoverController
{

    // Input inputHorizontal;
    // Input inputVertical;
    CollisionDirectionMover _mover;

    public PlayerMoverController(/*Input inputHorizontal, Input inputVertical,*/ CollisionDirectionMover mover)
    {
        // this.inputHorizontal = inputHorizontal;
        // this.inputVertical = inputVertical;
        _mover = mover;
    }

    public CollisionDirectionMover mover
    {
        get => _mover;
    }

    public void Move()
    {

        // -1 >= deltaX/deltaY >= 1
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        // Debug.Log("PlayerMoverController: " + new Vector2(deltaX, deltaY));
        Vector2 direction = new Vector2(deltaX, deltaY);
        mover.GetVectorFromDeltaCoordinate(direction, false);
    }
}

class NpcMoverController : IMoverController
{
    private CollisionDirectionMover _mover;
    private float lastTimeNpcMoved;
    private Vector2? _movementDirection;
    Vector2? _direction = null;

    public NpcMoverController(CollisionDirectionMover mover)
    {
        _mover = mover;
        lastTimeNpcMoved = Time.time;
    }

    public CollisionDirectionMover mover
    {
        get => _mover;
    }

    public Vector2? movementDirection
    {
        set => _movementDirection = value;
    }

    public void Move()
    {

        if (_movementDirection != null)
        {
            _direction = _movementDirection.Value;
            lastTimeNpcMoved = Time.time;
        }
        else
        {
            float diff = Time.time - lastTimeNpcMoved;
            if (diff > 3)
            {
                lastTimeNpcMoved = Time.time;
                _direction = _generateRandomMovementDirection();
            }
        }

        if (_direction != null)
        {
            mover.GetVectorFromDeltaCoordinate((Vector2)_direction, false);
        }
    }

    private Vector2 _generateRandomMovementDirection()
    {
        int xDirection = Random.Range(-1, 1);
        int yDirection = Random.Range(-1, 1);
        return new Vector2(xDirection, yDirection);
    }
}
