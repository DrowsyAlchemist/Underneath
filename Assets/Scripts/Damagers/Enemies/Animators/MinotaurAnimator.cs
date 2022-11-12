using UnityEngine;

public class MinotaurAnimator : EnemyAnimator
{
    private const string StartJumpAnimation = "StartJump";

    public void PlayJump()
    {
        Animator.Play(StartJumpAnimation);
    }
}
