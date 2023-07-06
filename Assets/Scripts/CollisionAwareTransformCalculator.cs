using UnityEngine;

public class CollisionAwareTransformCalculator
{
    public CollisionAwareTransformCalculator(float speed, BoxCollider2D boxCollider, string[] collisionWith)
    {
        this.speed = speed;
        this.boxCollider = boxCollider;
        this.collisionWith = collisionWith;
    }

    private RaycastHit2D hitByY;
    private RaycastHit2D hitByX;

    protected BoxCollider2D boxCollider;
    private string[] collisionWith;

    private float speed;

    public Vector2 GetTransformFromDirection(Direction? direction, Vector2 position)
    {
        // normalized -> same diagonal movement speed
        Vector2 deltaCordinate = new Vector2(direction.x, direction.y).normalized * speed;
        Vector2 newCoordinate = deltaCordinate * Time.deltaTime;

        float moveY = newCoordinate.y;
        hitByY = Physics2D.BoxCast(
            position,
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
            position,
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

        return new Vector2(moveX, moveY);
    }
}
