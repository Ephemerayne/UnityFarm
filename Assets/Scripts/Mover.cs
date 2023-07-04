using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    const float X_SPEED = 12.0f;
    const float Y_SPEED = 12.0f;

    protected BoxCollider2D boxCollider;
    protected RaycastHit2D hitByY;
    protected RaycastHit2D hitByX;

    public Vector2 speed = new Vector2(X_SPEED, Y_SPEED);
    private Animator animator;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // -1 >= deltaX/deltaY >= 1
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        // normalized -> same diagonal movement speed
        Vector2 deltaCordinate = new Vector2(deltaX, deltaY).normalized * speed;
        Vector2 newCoordinate = deltaCordinate * Time.deltaTime;

        if (newCoordinate.x != 0 || newCoordinate.y != 0)
        {
            if (Mathf.Abs(newCoordinate.x) >= 0.025f)
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
            transform.position,
            boxCollider.size,
            0,
            new Vector2(0, newCoordinate.y),
            Mathf.Abs(newCoordinate.y),
            LayerMask.GetMask("Actor", "Blocking")
        );

        bool isHitByY = hitByY.collider != null;

        if (isHitByY)
        {
            moveY = 0;
        }

        float moveX = newCoordinate.x;
        hitByX = Physics2D.BoxCast(
            transform.position,
            boxCollider.size,
            0,
            new Vector2(newCoordinate.x, 0),
            Mathf.Abs(newCoordinate.x),
            LayerMask.GetMask("Actor", "Blocking")
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

        transform.Translate(moveX, moveY, 0);
    }

    protected virtual void OnCollide(Collider2D collider)
    {
        Debug.Log("OnCollide was not implemented in " + this.name);
    }
}
