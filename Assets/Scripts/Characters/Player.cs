using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float speed = 4.0f;

    private BoxCollider2D playerCollider;
    private Animator animator;
    private Mover mover;
    private IMovementDirectionController _playerDirectionController;
    private string[] collisionWith = { "Cat", "Blocking" };
    private AnimationDirectionController _animationDirectionController;
    protected RaycastHit2D hit;

    private void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        _playerDirectionController = new PlayerDirectionController();
        _animationDirectionController = new AnimationDirectionController(animator);

        mover = new Mover(transform, playerCollider, collisionWith, speed);
    }

    private void FixedUpdate()
    {
        Direction movementDirection = _playerDirectionController.GetMovementDirection();

        _animationDirectionController.ChangeAnimationDirection(movementDirection);
        mover.HandleMovement(new Vector3(movementDirection.x, movementDirection.y, 0));
    }
}
