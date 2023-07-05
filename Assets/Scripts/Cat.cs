using UnityEngine;

public class Cat : MonoBehaviour
{
    public float speed = 0.25f;

    private CollisionDirectionMover mover;
    private RouteController routeController;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private IMoverController moverController;
    private string[] collisionWith = { "Player", "Blocking" };
    private OnClickChecker onClickChecker;

    public Cat()
    {
        onClickChecker = new OnClickChecker();
    }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        mover = new CollisionDirectionMover(
            speed, 
            transform,
            boxCollider, 
            animator, 
            collisionWith
        );

        routeController = new RouteController(transform);

        moverController = new NpcMoverController(mover);
    }

    private void Update()
    {
        onClickChecker.checkClick(_onClick);
    }

    private void FixedUpdate()
    {
        moverController.Move();

        Vector2? movementDirection = routeController.CheckRoutePoints();

        ((NpcMoverController)moverController).movementDirection = movementDirection;
    }

    private void _onClick(Vector2 coordinate) {
        routeController.OnGetNewCoordinate(coordinate);
    }
}
