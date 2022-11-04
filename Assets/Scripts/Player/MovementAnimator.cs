using UnityEngine;

public class MovementAnimator : PlayerAnimation
{
    public void AnimateByVelocityAndGrounded(Vector2 velocity, bool isGrounded)
    {
        if (isGrounded == false)
            PlayJump();
        else if (velocity.x == 0)
            PlayIdle();
        else
            PlayRun();

        TurnByVelocity(velocity);
    }

    private void TurnByVelocity(Vector2 velocity)
    {
        if (velocity.x != 0)
        {
            bool positiveDirection = velocity.x > 0;
            transform.LookForwardDirection(positiveDirection);
        }
    }
}
