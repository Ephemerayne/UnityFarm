using System;
using UnityEngine;

interface IMoverController
{
    CollisionDirectionMover mover { get; }

    public abstract void Move();
}

class PlayerMoverController: IMoverController
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

    public CollisionDirectionMover mover {
        get => _mover;
    }

    public void Move()
    {

        // -1 >= deltaX/deltaY >= 1
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");


        // Debug.Log("PlayerMoverController: " + new Vector2(deltaX, deltaY));
        mover.GetVectorFromDeltaCoordinate(deltaX, deltaY, false);
    }
}

class NpcMoverController : IMoverController
{
    private CollisionDirectionMover _mover;
    private float lastTimeNpcMoved;
    private float dx;
    private float dy;

    public NpcMoverController(CollisionDirectionMover mover)
    {
        _mover = mover;
        lastTimeNpcMoved = Time.time;
        dx = 0;
        dy = 0;
    }

    public CollisionDirectionMover mover
    {
        get => _mover;
    }


    public void Move()
    {
        float diff = Time.time - lastTimeNpcMoved;
        if (diff > 3)
        {
            lastTimeNpcMoved = Time.time;
            
            dx = UnityEngine.Random.Range(-1f, 1f);
            dy = UnityEngine.Random.Range(-1f, 1f);
        }
        else
        {
             // Debug.Log("NpcMoverController: " + new Vector2(dx, dy));
             mover.GetVectorFromDeltaCoordinate(dx, dy, false);
        }
    }
}
