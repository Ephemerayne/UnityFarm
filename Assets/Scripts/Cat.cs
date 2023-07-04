using UnityEngine;

public class Cat : MonoBehaviour
{
    public float speed = 0.25f;

    private CollisionDirectionMover mover;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private IMoverController moverController;
    private string[] collisionWith = { "Player", "Blocking" };

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
        moverController = new NpcMoverController(mover);
    }

    private void FixedUpdate()
    {
        moverController.Move();
    }
}
