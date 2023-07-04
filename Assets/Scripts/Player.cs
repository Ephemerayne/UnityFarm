using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4.0f;

    private CollisionDirectionMover mover;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private IMoverController moverController;
    private string[] collisionWith = { "Cat", "Blocking" };


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        mover = new CollisionDirectionMover(speed, transform, boxCollider, animator, collisionWith);
        moverController = new PlayerMoverController(mover);
    }

    private void FixedUpdate()
    {
        moverController.Move();
    }
}