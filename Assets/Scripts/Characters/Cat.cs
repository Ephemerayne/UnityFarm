using UnityEngine;

public class Cat : MonoBehaviour
{
    public float speed = 0.25f;

    private NpcDirectionsController _npcDirectionsController;
    private CapsuleCollider2D catCollider;
    private Animator animator;
    private Mover mover;
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
        catCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        mover = new Mover(transform, catCollider, collisionWith, speed);
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

        mover.HandleMovement(new Vector3(movementDirection.x, movementDirection.y, 0));
    }

    private void _onClick(Vector2 coordinate)
    {
        _npcDirectionsController.OnGetNewCoordinate(coordinate);
    }

    private void _onAlmostReachedNextRoutePoint(Vector2 nextPoint)
    {
        transform.position = nextPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
    }
}
