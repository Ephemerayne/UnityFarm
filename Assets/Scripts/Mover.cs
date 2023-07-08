using UnityEngine;

public class Mover
{
    private Transform _transfrom;
    private Collider2D _collider;
    private string[] collisionWith;
    private float _speed;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;


    public Mover(Transform transform, Collider2D collider, string[] collisionWith, float speed)
    {
        this._collider = collider;
        this.collisionWith = collisionWith;
        this._speed = speed;
        this._transfrom = transform;
    }

    public void HandleMovement(Vector3 input)
    {

        Vector2 colliderSize;

        if (_collider is BoxCollider2D boxCollider)
        {
            colliderSize = boxCollider.size;
        }
        else if (_collider is CapsuleCollider2D capsuleCollider)
        {
            colliderSize = capsuleCollider.size;
        }
        else if (_collider is EdgeCollider2D edgeCollider)
        {
            colliderSize = edgeCollider.bounds.size;
        }
        else if (_collider is PolygonCollider2D polygonCollider)
        {
            colliderSize = polygonCollider.bounds.size;
        }
        else
        {
            colliderSize = Vector2.zero;
            // Other types of colliders
        }

        Vector2 directionNormalized = input.normalized;
        moveDelta = new Vector3(directionNormalized.x * _speed, directionNormalized.y * _speed, 0);

        // make sure we can move in this direction, by casting a box there first, if the box returns null, were free to move
        hit = Physics2D.BoxCast(_transfrom.position, colliderSize, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.fixedDeltaTime), LayerMask.GetMask(collisionWith));
        if (hit.collider == null)
        {
            // Make move by y coordinate
            _transfrom.Translate(0, moveDelta.y * Time.fixedDeltaTime, 0);

        }

        hit = Physics2D.BoxCast(_transfrom.position, colliderSize, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.fixedDeltaTime), LayerMask.GetMask(collisionWith));
        if (hit.collider == null)
        {
            // Make move by x coordinate
            _transfrom.Translate(moveDelta.x * Time.fixedDeltaTime, 0, 0);

        }
    }
}

