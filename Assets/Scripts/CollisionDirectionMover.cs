using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDirectionMover
{
    public CollisionDirectionMover(float speed, Transform transform, BoxCollider2D boxCollider, Animator animator, string[] collisionWith)
    {
        this.speed = speed;
        this.boxCollider = boxCollider;
        this.animator = animator;
        _transform = transform;
        this.collisionWith = collisionWith;
    }

    private RaycastHit2D hitByY;
    private RaycastHit2D hitByX;

    protected BoxCollider2D boxCollider;
    private Animator animator;
    private Transform _transform;
    private string[] collisionWith;

    private float speed;

    private const float _xAxisSpeedLimit = 0.005f;

    public void GetVectorFromDeltaCoordinate(float deltaX, float deltaY, bool shouldPrint = false)
    {
        // normalized -> same diagonal movement speed
        Vector2 deltaCordinate = new Vector2(deltaX, deltaY).normalized * speed;
        Vector2 newCoordinate = deltaCordinate * Time.deltaTime;

        if (newCoordinate.x != 0 || newCoordinate.y != 0)
        {
            if (shouldPrint)
            {
                // Debug.Log("_xAxisSpeedLimit * speed: " + (_xAxisSpeedLimit));
                Debug.Log("Mathf.Abs(newCoordinate.x) / speed: " + Mathf.Abs(newCoordinate.x) / speed);
            }   
            
            if (Mathf.Abs(newCoordinate.x) / speed >= _xAxisSpeedLimit)
            {
                // Horizontal movement has higher magnitude or equal magnitude to vertical movement
                animator.SetFloat("X", newCoordinate.x);
                animator.SetFloat("Y", 0);
            }
            else
            {
                // Vertical movement has higher magnitude
                animator.SetFloat("X", 0);
                animator.SetFloat("Y", newCoordinate.y);
            }
        }

        float moveY = newCoordinate.y;
        hitByY = Physics2D.BoxCast(
            _transform.position,
            boxCollider.size,
            0,
            new Vector2(0, newCoordinate.y),
            Mathf.Abs(newCoordinate.y),
            LayerMask.GetMask(collisionWith)
        );

        bool isHitByY = hitByY.collider != null;

        if (isHitByY)
        {
            moveY = 0;
        }

        float moveX = newCoordinate.x;
        hitByX = Physics2D.BoxCast(
            _transform.position,
            boxCollider.size,
            0,
            new Vector2(newCoordinate.x, 0),
            Mathf.Abs(newCoordinate.x),
            LayerMask.GetMask(collisionWith)
        );

        bool isHitByX = hitByX.collider != null;

        if (isHitByX)
        {
            moveX = 0;
        }

        // Stop walking when collide
        if ((hitByX && moveY == 0) || (hitByY && moveX == 0) || (hitByX && hitByY) || (moveX == 0 && moveY == 0))
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
        }

        _transform.Translate(new Vector2(moveX, moveY));
    }
}
