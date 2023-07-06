using UnityEngine;

public class Cat : MonoBehaviour
{
    public float speed = 0.25f;

    private CollisionAwareTransformCalculator collisionAwareTransformCalculator;
    private NpcDirectionsController _npcDirectionsController;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private IMovementDirectionController _npcGeneratedDirectionController;
    private string[] collisionWith = { "Player", "Blocking" };
    private OnClickChecker onClickChecker;
    private AnimationDirectionController _animationDirectionController;

    public Cat()
    {
        onClickChecker = new OnClickChecker();
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        collisionAwareTransformCalculator = new CollisionAwareTransformCalculator(
            speed,
            boxCollider,
            collisionWith
        );

        _npcDirectionsController = new NpcDirectionsController();

        _npcGeneratedDirectionController = new NpcGeneratedDirectionsController();

        _animationDirectionController = new AnimationDirectionController(animator);
    }

    private void Update()
    {
        onClickChecker.checkClick(_onClick);
    }

    private void FixedUpdate()
    {
        Direction movementDirection = _npcDirectionsController.GetMovementDirection(
            transform.position,
            _onAlmostReachedNextRoutePoint
        ) ?? _npcGeneratedDirectionController.GetMovementDirection();

        _animationDirectionController.ChangeAnimationDirection(movementDirection);

        Vector2 _transform = collisionAwareTransformCalculator.GetTransformFromDirection(movementDirection, transform.position);

        transform.Translate(_transform);
    }

    private void _onClick(Vector2 coordinate)
    {
        _npcDirectionsController.OnGetNewCoordinate(coordinate);
    }

    private void _onAlmostReachedNextRoutePoint(Vector2 nextPoint)
    {
        transform.position = nextPoint;
    }
}
