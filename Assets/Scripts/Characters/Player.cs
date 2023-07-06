using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4.0f;

    private CollisionAwareTransformCalculator mover;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private IMovementDirectionController _playerDirectionController;
    private string[] collisionWith = { "Cat", "Blocking" };
    private AnimationDirectionController _animationDirectionController;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        mover = new CollisionAwareTransformCalculator(speed, boxCollider, collisionWith);

        _playerDirectionController = new PlayerDirectionController();

        _animationDirectionController = new AnimationDirectionController(animator);
    }

    private void FixedUpdate()
    {
        Direction movementDirection = _playerDirectionController.GetMovementDirection();

        Vector2 _transform = mover.GetTransformFromDirection(movementDirection, transform.position);
        transform.Translate(_transform);

        _animationDirectionController.ChangeAnimationDirection(movementDirection);
    }


    /*private void FixedUpdate()
    {
        Direction movementDirection = _npcDirectionsController.GetMovementDirection(
            transform.position,
            _onAlmostReachedNextRoutePoint
        ) ?? _npcGeneratedDirectionController.GetMovementDirection();

        _animationDirectionController.ChangeAnimationDirection(movementDirection);

        Vector2 _transform = collisionAwareTransformCalculator.GetTransformFromDirection(movementDirection, transform.position);

        transform.Translate(_transform);
    }*/
}