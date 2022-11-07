using UnityEngine;

public class MinotaurAnimation : EnemyAnimation
{
    private const string StartJumpAnimation = "StartJump";

    public void PlayJump()
    {
        Animator.Play(StartJumpAnimation);
    }
}
