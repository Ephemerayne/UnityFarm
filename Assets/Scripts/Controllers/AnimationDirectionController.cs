using UnityEngine;

public class AnimationDirectionController
{
    Animator animator;

    public AnimationDirectionController(Animator animator)
    {
        this.animator = animator;
    }

    public void ChangeAnimationDirection(Direction direction)
    {
        bool isMoving = direction.x != 0 || direction.y != 0;

        animator.SetBool("IsWalking", isMoving);

        if (isMoving && Mathf.Abs(direction.x) > 0.2f)
        {
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", 0);
        }
        else
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", direction.y);
        }
    }
}
