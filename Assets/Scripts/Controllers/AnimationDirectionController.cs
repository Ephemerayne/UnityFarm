using UnityEngine;

public class AnimationDirectionController
{
    Animator animator;
    float lastXDirection;
    float lastYDirection;

    public AnimationDirectionController(Animator animator)
    {
        this.animator = animator;
    }

    public void ChangeAnimationDirection(Direction direction)
    {
        bool isMoving = direction.x != 0 || direction.y != 0;

        animator.SetBool("IsWalking", isMoving);

        if (isMoving)
        {
            lastXDirection = direction.x;
            lastYDirection = direction.y;


            if (direction.x == 0)
            {
                animator.SetFloat("X", 0);
            }
        }

        if (isMoving && Mathf.Abs(direction.x) > 0.2f)
        {
            lastXDirection = direction.x;
            lastYDirection = direction.y;

            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", 0);
        }
        else
        {
            animator.SetFloat("X", lastXDirection);
            animator.SetFloat("Y", lastYDirection);
        }
    }
}
